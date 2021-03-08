/**
 * Collection of helper functions for the project
 */
const helper = {
    /**
     * Evaluates the modal to ensure that the modal meets the following requirements:
     * 1. Has a modal body and a footer
     * 2. Contains a form using post method
     * @param {HTMLElement} modal The Bootstrap modal div element
     * @returns {boolean} True if requirements are met, else false
     */
    meetsModalRequirements: function (modal) {
        let result = false;

        if (modal && modal.classList.contains('modal')) {
            let modalBody = modal.querySelector('.modal-body');
            let modalFooter = modal.querySelector('.modal-footer');
            let modalForm = modal.querySelector('form');

            result = (modalBody && modalFooter && modalForm && modalForm.method === 'post') ? true : false;
        }

        return result;
    }
}

/**
 * Handles the way data is passed to modal when performing post delete actions.
 * @class
 * @classdesc Handles the way data is passed to modal when performing post delete actions.
 */
export class ModalDeletePrompt {
    /**
     * Initiates the components that will be used for setting up the bootstrap modal.
     * @param {HTMLElement} modal Generic bootstrap modal with containing form in footer 
     * @param {string} message Confirmation message upon delete
     * @param {string} id The id that the server will use to delete the respective item
     * @param {string} path The form action path 
     */
    constructor(modal, path, message, id) {
        this.modal = modal;
        this.message = message;
        this.id = id;
        this.path = path;
    }

    /**
     * Inject constructor requirements into modal when button is clicked from table.
     * This function assumes the table to have only one button which is used post and that 
     * the name will be in the first column of every row.
     * It will also search for an [data-user-id] attribute on the buttons containing the id, should 
     * it not be passed through the constructor.
     * @param {HTMLElement} table The table element 
     */
    fromTable(table) {
        if (!table) {
            return;
        }
        const buttons = table.querySelectorAll('button');

        if (buttons.length !== 0 && helper.meetsModalRequirements(this.modal)) {
            for (const btn of buttons) {
                let row = btn.closest('tr');
                let id = this.id ? this.id : btn.dataset.userId;
                if (id) {
                    let name = row.firstElementChild.textContent.trim();
                    btn.addEventListener('click', () => {
                        if (this.message) {
                            this.modal.querySelector('.modal-body').innerHTML = `${this.message} ${name}?`;
                        }
                        this.modal.querySelector('.modal-footer form').action = `${this.path}${id}`
                    });
                }
                else {
                    console.log("Id used in form action not specified");
                }
            }
        }
    }

    /**
     * @param {string} message Title message to set in header
     */
    set setTitleMessage(message) {
        this.modal.querySelector('.modal-title').innerHTML = message;
    }

    /**
     * @param {string} message Message to set in header
     */
    set setBodyMessage(message) {
        this.modal.querySelector('.modal-body').innerHTML = message;
    }
}

/**
 * Prevent user from double clicking on button for form submission.
 * @param {HTMLElement} form Form that will be submitted
 * @param {HTMLElement} input The input or button element with type submit
 */

export function preventDoubleSubmit(form, input) {
    if (input.getAttribute('type') === 'submit') {
        form.addEventListener('submit', () => {
            if ($('form').valid() !== false) {
                input.disabled = true;
            }
        });
    }
}

/**
 * 
 * @param {string} tableId
 */
export function initDt(tableId) {
    // Setup - add a text input to each footer cell
    let i = 0;
    let colCount = $(`#${tableId} thead tr th`).length;
    $(`#${tableId} thead th`).each(function () {
        if (i !== (colCount - 1)) {
            var title = $(this).children().first().text()
            $(this).append('<input type="text" class="form-control form-control-sm" placeholder="Search ' + title + '" />');
        }
        i++;
    });

    // DataTable
    var dt = $(`#${tableId}`).DataTable({
        columnDefs: [
            { targets: [colCount - 1], searchable: false, orderable: false }
        ],
        initComplete: function () {
            // Apply the search
            this.api().columns('.filterable').every(function () {
                var that = this;

                $('input', this.header()).on('click keyup change clear', function (e) {
                    e.stopPropagation();
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });
}

/**
 * @typedef buildTableOptions
 * @param {object} options
 * @param {string} options.itemId
 * @param {string} options.detailsPath
 * @param {string} options.editPath
 */
export function buildTableRowActions(options) {
    return `<div class="action-container d-flex align-content-center" data-item-id="${options.itemId}">
                <a class="icon-button mr-1 ml-1" data-toggle="tooltip" data-placement="top" title="Details"
                    href="${options.detailsPath}/${options.itemId}" data-original-title="View">
                    <i class="material-icons">search</i>
                </a>
                <a class="icon-button mr-1 ml-1" data-toggle="tooltip" data-placement="top" title="Edit"
                    href="${options.editPath}/${options.itemId}" data-original-title="Edit">
                    <i class="material-icons">edit</i>
                </a>
                <span data-toggle="tooltip" class="icon-button--delete mr-1 ml-1" data-placement="top" title="Delete" data-original-title="Delete">
                    <button class="icon-button--delete"
                        type="button" data-toggle="modal" data-target="#deleteModal">
                    <i class="material-icons">delete</i>
                    </button>
                </span>
            </div>`;
}

/**
 * Prepare delete modal.
 * @param {object} options
 * @param {HTMLElement} options.modal
 * @param {string} options.titleMessage
 * @param {string} options.bodyMessage
 * @param {string} options.deletePath
 * @param {string} options.actionId
 */
export function prepTableRowDeleteModal(options) {
    if (!options.modal
        && !options.modal.classList.contains('.modal')
        && !options.modal.classList.contains('.modal-title')
        && !options.modal.classList.contains('.modal-body')) {
        console.error('Not a valid bootstrap 4 modal');
    }

    options.modal.querySelector('.modal-title').innerHTML = options.titleMessage;
    options.modal.querySelector('.modal-body').innerHTML = options.bodyMessage;
    options.modal.querySelector('form').action = `${options.deletePath}/${options.actionId}`;
}