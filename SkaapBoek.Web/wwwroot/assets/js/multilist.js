/**
 * Creates the multi-select list from a UL element.
 * Each li element MUST contain a [data-item-id] attribute.
 * The latter attribute would be the ID of the object as recorded in the datastore.
 * The attribute will be used to populate input elements for the target list.
 * @class
 */
export class MultiList {
    /**
     * 
     * @param {HTMLElement} container The UL element that contains the source items 
     * @param {string} inputNamePrefix The prefix used for the name attribute of the input elements 
     */
    constructor(container, inputNamePrefix) {
        this.container = container;
        this.inputNamePrefix = inputNamePrefix;
        this.sourceSearchInput = container.querySelectorAll('[type="search"]')[0];
        this.targetSearchInput = container.querySelectorAll('[type="search"]')[1];
        this.sourceList = container.querySelectorAll('ul')[0];
        this.targetList = container.querySelectorAll('ul')[1];

        initializeLists(this);
        initButtons(this);
        initSearch(this);
    }
}

function createListItem(multilist, inputValue) {
    var input = document.createElement('input');
    input.name = multilist.inputNamePrefix;
    input.value = inputValue;
    input.hidden = true;
    return input;
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 */
function moveToTargetList(multilist, selectedLi) {
    if (selectedLi) {
        const id = selectedLi.getAttribute('data-item-id');
        selectedLi.appendChild(createListItem(multilist, id))
        multilist.targetList.appendChild(selectedLi);
    }
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 */
function moveToSourceList(multilist, selectedLi) {
    if (selectedLi) {
        const targetInput = selectedLi.querySelector('input');
        if (targetInput) {
            selectedLi.removeChild(targetInput);
        }
        multilist.sourceList.appendChild(selectedLi);
    }
}

function initializeLists(multilist) {
    multilist.sourceList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToTargetList(multilist, selectedLi);
    });

    multilist.targetList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToSourceList(multilist, selectedLi);
    });
}

function initButtons(multilist) {
    const toTargetButton = multilist.container.querySelector('.multi-btn-target');
    const toSourceButton = multilist.container.querySelector('.multi-btn-source');
    const sourceList = multilist.container.querySelectorAll('ul')[0];
    const targetList = multilist.container.querySelectorAll('ul')[1];


    if (toTargetButton && toSourceButton) {
        toTargetButton.addEventListener('click', () => {
            for (let li of multilist.sourceList.children) {
                const id = li.getAttribute('data-item-id');
                li.appendChild(createListItem(multilist, id));
            }
            multilist.targetList.append(...sourceList.childNodes);
        })

        toSourceButton.addEventListener('click', () => {
            for (let li of multilist.targetList.children) {
                const targetInput = li.querySelector('input');
                li.removeChild(targetInput);
            }
            multilist.sourceList.append(...targetList.childNodes);
        })
    }
}

function initSearch(multilist) {
    configureSearch(multilist, container => container.querySelectorAll('.card')[0]);
    configureSearch(multilist, container => container.querySelectorAll('.card')[1]);
}

/**
 * @param {function(HTMLElement):HTMLElement} selectList
 * - Callback used to select the source or target list from multilist container.}
 * */
function configureSearch(multilist, selectList) {
    const list = selectList(multilist.container).querySelector('ul');
    const searchInput = selectList(multilist.container).querySelector('[type="search"]');
    searchInput.addEventListener('keydown', (e) => {
        if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
            const sourceSpans = list.querySelectorAll('span');

            for (let item of sourceSpans) {
                if (item.innerText.toLowerCase().indexOf(searchInput.value.toLowerCase()) > -1) {
                    item.parentElement.style.display = "";
                }
                else {
                    item.parentElement.style.display = 'none';
                }
            }
            e.preventDefault();
        }
    });

    searchInput.addEventListener('search', (e) => {
        const sourceSpans = list.querySelectorAll('span');

        for (let item of sourceSpans) {
            item.parentElement.style.display = "";
        }
        e.preventDefault();
    })
}