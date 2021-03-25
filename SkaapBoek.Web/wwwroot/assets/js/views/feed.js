import * as common from '../common.js'

export function create() {
}

export function details() {
    const body = document.querySelector('body');
    const deleteModal = body.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Feed/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = body.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function edit() {
}

export function index() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete item";
    const actionPath = "/Feed/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);

    $('table').DataTable({
        searchDelay: 1000,
        columnDefs: [{
            targets: -1,
            searchable: false,
            orderable: false
        }]
    });
}