import * as common from '../common.js';

export function index() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete template";
    const actionPath = "/Tasks/Delete/";
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
    const body = document.querySelector('body');
    const groupRadio = body.querySelector('#groupRadio');
    const sheepRadio = body.querySelector('#sheepRadio');
    const groupSelect = body.querySelector('#groupSelectContainer');
    const sheepSelect = body.querySelector('#sheepSelectContainer');

    groupRadio.addEventListener('change', e => {
        groupSelect.hidden = false;
        sheepSelect.hidden = true;
    });

    sheepRadio.addEventListener('change', e => {
        groupSelect.hidden = true;
        sheepSelect.hidden = false;
    });

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

export function edit() {
    const body = document.querySelector('body');
    const groupRadio = body.querySelector('#groupRadio');
    const sheepRadio = body.querySelector('#sheepRadio');
    const groupSelect = body.querySelector('#groupSelectContainer');
    const sheepSelect = body.querySelector('#sheepSelectContainer');

    if (groupSelect.querySelector('select').value != '') {
        groupSelect.hidden = false;
        sheepSelect.hidden = true;
        groupRadio.checked = true;
        sheepRadio.checked = false;
    }

    if (sheepSelect.querySelector('select').value != '') {
        groupSelect.hidden = true;
        sheepSelect.hidden = false;
        groupRadio.checked = false;
        sheepRadio.checked = true;
    }

    groupRadio.addEventListener('change', e => {
        groupSelect.hidden = false;
        sheepSelect.hidden = true;
    });

    sheepRadio.addEventListener('change', e => {
        groupSelect.hidden = true;
        sheepSelect.hidden = false;
    });

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