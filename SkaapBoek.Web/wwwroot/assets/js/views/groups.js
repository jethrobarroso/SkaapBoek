import * as common from '../common.js';
import { MultiList } from '../multilist.js';

export function index() {
    const body = document.querySelector('body');
    const deleteModal = body.querySelector('.modal');
    const deleteForm = deleteModal.querySelector('form');
    const table = body.querySelector('table');
    const message = "Are you sure you want to delete project";
    const actionPath = "/Groups/Delete/";

    body.querySelectorAll('[data-user-id]').forEach(btn => {
        btn.addEventListener('click', e => {
            let groupId = btn.dataset.userId;
            if (!groupId) {
                console.error("Error: No group ID specified for delete action.");
                return;
            }
            let groupName = btn.closest('tr').firstElementChild.innerHTML;
            deleteModal.querySelector('.modal-body').innerHTML = `Are you sure 
                you want to delete ${groupName.trim()}?`;
            deleteForm.action = actionPath + groupId;
        })
    })

    $('table').DataTable({
        searchDelay: 1000,
        columnDefs: [{
            targets: -1,
            searchable: false,
            orderable: false
        }]
    });
}

export function edit() {
    const multiList = document.querySelector('.multilist-container');
    new MultiList(multiList, "SelectedSheepIds");

    $('#PenId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select pen"
    });
}

export function create() {
    $('select').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select pen"
    });
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