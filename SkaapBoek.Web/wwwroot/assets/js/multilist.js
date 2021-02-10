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
    }

    init() {
        InitializeLists(this.container, this.inputNamePrefix);
        initButtons(this.container, this.inputNamePrefix);
        initSearch(this.sourceSearchInput, this.sourceList, this.targetSearchInput, this.targetList);
    }
}

function createListItem(inputNamePrefix, inputValue) {
    var input = document.createElement('input');
    input.name = inputNamePrefix;
    input.value = inputValue;
    input.hidden = true;
    return input;
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 * @param {string} inputNamePrefix 
 * @param {HTMLElement} targetList 
 */
function moveToTargetList(selectedLi, targetList, inputNamePrefix) {
    if (selectedLi) {
        const id = selectedLi.getAttribute('data-item-id');
        selectedLi.appendChild(createListItem(inputNamePrefix, id))
        targetList.appendChild(selectedLi);
    }
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 * @param {HTMLElement} sourceList 
 */
function moveToSourceList(selectedLi, sourceList) {
    if (selectedLi) {
        const targetInput = selectedLi.querySelector('input');
        if (targetInput) {
            selectedLi.removeChild(targetInput);
        }
        sourceList.appendChild(selectedLi);
    }
}

/**
 * 
 * @param {HTMLElement} container Wrapper containing the source and target UL's 
 * @param {string} inputNamePrefix The prefix used for the name attribute of the input elements
 */
function InitializeLists(container, inputNamePrefix) {
    const sourceList = container.querySelectorAll('ul')[0];
    const targetList = container.querySelectorAll('ul')[1];
    sourceList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToTargetList(selectedLi, targetList, inputNamePrefix);
    });

    targetList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToSourceList(selectedLi, sourceList);
    });
}

/**
 * 
 * @param {HTMLElement} toTargetButton 
 * @param {HTMLElement} toSourceButton 
 */
function initButtons(container, inputNamePrefix) {
    const toTargetButton = container.querySelector('.multi-btn-target');
    const toSourceButton = container.querySelector('.multi-btn-source');
    const sourceList = container.querySelectorAll('ul')[0];
    const targetList = container.querySelectorAll('ul')[1];


    if (toTargetButton && toSourceButton) {
        toTargetButton.addEventListener('click', () => {
            for (let li of sourceList.children) {
                const id = li.getAttribute('data-item-id');
                li.appendChild(createListItem(inputNamePrefix, id));
            }
            targetList.append(...sourceList.childNodes);
        })

        toSourceButton.addEventListener('click', () => {
            for (let li of targetList.children) {
                const targetInput = li.querySelector('input');
                li.removeChild(targetInput);
            }
            sourceList.append(...targetList.childNodes);
        })
    }
}

/**
 * 
 * @param {HTMLElement} sourceSearch 
 * @param {HTMLElement} sourceList 
 * @param {HTMLElement} targetSearch 
 * @param {HTMLElement} targetList 
 */
function initSearch(sourceSearch, sourceList, targetSearch, targetList) {
    sourceSearch.addEventListener('keydown', (e) => {
        if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
            const sourceSpans = sourceList.querySelectorAll('span');

            for (let item of sourceSpans) {
                if (item.innerText.indexOf(sourceSearch.value) > -1) {
                    item.parentElement.style.display = "";
                }
                else {
                    item.parentElement.style.display = 'none';
                }
            }
            e.preventDefault();
        }
    });

    targetSearch.addEventListener('keydown', (e) => {
        if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
            const targetSpans = targetList.querySelectorAll('span');
            console.log('triggered');
            for (let item of targetSpans) {
                if (item.innerText.indexOf(targetSearch.value) > -1) {
                    item.parentElement.style.display = "";
                }
                else {
                    item.parentElement.style.display = 'none';
                }
            }
            e.preventDefault();
        }
    })
}

