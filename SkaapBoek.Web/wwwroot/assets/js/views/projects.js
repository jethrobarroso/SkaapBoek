import * as common from '../common.js';
import { MultiList } from '../multilist.js';

export function list() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete project";
    const actionPath = "/Projects/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);

    common.initDt();
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Projects/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function edit() {
    const multiLists = document.querySelectorAll('.multilist-container');
    const multiSheep = new MultiList(multiLists[0]);
    const multiGroup = new MultiList(multiLists[1]);

    multiSheep.init();
    multiGroup.init();
}