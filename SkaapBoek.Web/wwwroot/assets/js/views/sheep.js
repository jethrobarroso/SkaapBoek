import * as common from '../common.js';
import { MultiList } from '../multilist.js'

export function index() {
    const deleteModal = document.querySelector('.modal');
    const tables = document.querySelectorAll('table');
    const message = "Are you sure you want to delete sheep with tag ";
    const actionPath = "/Sheep/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.setBodyMessage = "Are you sure you want to delete this sheep?"

    for (const table of tables) {
        obj.fromTable(table);
    }

    common.initDt("herdTable");
    common.initDt("childTable");
}

export function create() {
    $('[data-selector="multi"]').each(function () {
        $(this).select2({
            theme: 'bootstrap4',
            width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
            placeholder: $(this).data('placeholder'),
            allowClear: Boolean($(this).data('allow-clear')),
            closeOnSelect: !$(this).attr('multiple'),
        });
    });

    $('#FatherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select father"
    });

    $('#MotherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select mother"
    });
}

export function edit() {
    $('#FeedId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select feed"
    });

    $('#FatherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select father"
    });

    $('#MotherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select mother"
    });
}

export function details() {
    const body = document.body;
    const deleteModal = body.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Sheep/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');
    const tag = document.querySelector('.card-header').getAttribute('data-sb-tag');

    deleteModal.querySelector('.modal-title').innerHTML = `Delete sheep with tag ${tag}`;
    deleteModal.querySelector('.modal-body').innerHTML = "Are you sure you want to delete this sheep?"

    if (body.querySelector('#childrenTable')) {
        common.initDt("childrenTable");
    }

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function editChild() {
    $('#FatherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select father"
    });

    $('#MotherId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select mother"
    });
}