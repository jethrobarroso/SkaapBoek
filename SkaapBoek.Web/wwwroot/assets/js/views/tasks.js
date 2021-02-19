import * as common from '../common.js';

export function index() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete template";
    const actionPath = "/Tasks/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);

    common.initDt("taskTable");
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Tasks/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function create() {
    $('#GroupId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select group"
    });

    $('#SheepId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select sheep"
    });
}