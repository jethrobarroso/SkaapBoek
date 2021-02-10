import * as common from '../common.js'

export function create() {
    const button = document.querySelector('#btnCreate');
    const form = button.closest('form');
    button.disabled = false;

    common.preventDoubleSubmit(form, button);
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Feed/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function edit() {
    const button = document.querySelector('#btnCreate');
    const form = button.closest('form');
    button.disabled = false;

    common.preventDoubleSubmit(form, button);
}

export function list() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete item";
    const actionPath = "/Feed/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);
}