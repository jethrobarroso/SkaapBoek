import * as common from '../common.js';
import { MultiList } from '../multilist.js';

export function list() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete project";
    const actionPath = "/Groups/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);

    common.initDt();
}

export function edit() {
    const multiList = document.querySelector('.multilist-container');
    const multi = new MultiList(multiList, "selectedSheepIds");
    multi.init();
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Groups/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}