/** @type {HTMLElement} _container */
let _container;
/** @type {HTMLElement} _inputNamePrefix */
let _inputNamePrefix;
/** @type {HTMLElement} _sourceSearchInput */
let _sourceSearchInput;
/** @type {HTMLElement} _targetSearchInput */
let _targetSearchInput;
/** @type {HTMLElement} _sourceList */
let _sourceList;
/** @type {HTMLElement} _targetList */
let _targetList;

/**
 * Creates the multi-select list from a UL element.
 * Each li element MUST contain a [data-item-id] attribute.
 * The latter attribute would be the ID of the object as recorded in the datastore.
 * The attribute will be used to populate input elements for the target list.
 * @class
 */
export class MultiList {
    let 

    /**
     * 
     * @param {HTMLElement} container The UL element that contains the source items 
     * @param {string} inputNamePrefix The prefix used for the name attribute of the input elements 
     */
    constructor(container, inputNamePrefix) {
        _container = this.container = container;
        _inputNamePrefix = this.inputNamePrefix = inputNamePrefix;
        _sourceSearchInput = this.container.querySelectorAll('[type="search"]')[0];
        _targetSearchInput = this.container.querySelectorAll('[type="search"]')[1];
        _sourceList = this.container.querySelectorAll('ul')[0];
        _targetList = this.container.querySelectorAll('ul')[1];

        initializeLists();
        initButtons();
        initSearch();
    }
}

function createListItem(inputValue) {
    var input = document.createElement('input');
    input.name = _inputNamePrefix;
    input.value = inputValue;
    input.hidden = true;
    return input;
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 */
function moveToTargetList(selectedLi) {
    if (selectedLi) {
        const id = selectedLi.getAttribute('data-item-id');
        selectedLi.appendChild(createListItem(id))
        _targetList.appendChild(selectedLi);
    }
}

/**
 * 
 * @param {HTMLElement} selectedLi 
 */
function moveToSourceList(selectedLi) {
    if (selectedLi) {
        const targetInput = selectedLi.querySelector('input');
        if (targetInput) {
            selectedLi.removeChild(targetInput);
        }
        _sourceList.appendChild(selectedLi);
    }
}

function initializeLists() {
    _sourceList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToTargetList(selectedLi);
    });

    _targetList.addEventListener("click", (e) => {
        let selectedLi;
        for (let node of e.path) {
            if (node.tagName == "LI") {
                selectedLi = node;
                break;
            }
        }

        moveToSourceList(selectedLi);
    });
}

function initButtons() {
    const toTargetButton = _container.querySelector('.multi-btn-target');
    const toSourceButton = _container.querySelector('.multi-btn-source');
    const sourceList = _container.querySelectorAll('ul')[0];
    const targetList = _container.querySelectorAll('ul')[1];


    if (toTargetButton && toSourceButton) {
        toTargetButton.addEventListener('click', () => {
            for (let li of _sourceList.children) {
                const id = li.getAttribute('data-item-id');
                li.appendChild(createListItem(id));
            }
            _targetList.append(...sourceList.childNodes);
        })

        toSourceButton.addEventListener('click', () => {
            for (let li of _targetList.children) {
                const targetInput = li.querySelector('input');
                li.removeChild(targetInput);
            }
            _sourceList.append(...targetList.childNodes);
        })
    }
}

function initSearch() {
    configureSearch(container => container.querySelectorAll('.card')[0]);
    configureSearch(container => container.querySelectorAll('.card')[1]);
}

/**
 * @param {function(HTMLElement):HTMLElement} selectList
 * - Callback used to select the source or target list from multilist container.}
 * */
function configureSearch(selectList) {
    const list = selectList(_container).querySelector('ul');
    const searchInput = selectList(_container).querySelector('[type="search"]');
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