import * as common from '../common.js';
import { MultiList } from '../multilist.js';

export function index() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete pen";
    const actionPath = "/Pen/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);
    common.initDt('penTable');
}

export function edit() {
    const multiLists = document.querySelectorAll('.multilist-container');
    new MultiList(multiLists[0], "GroupIds");
    new MultiList(multiLists[1], "SheepIds");

    $('#FeedId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select pen"
    });
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Pen/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}