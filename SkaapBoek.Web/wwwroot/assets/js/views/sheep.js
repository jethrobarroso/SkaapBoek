import * as common from '../common.js';

export function index() {
    $(document).ready(function () {

    });
}

export function list() {
    const deleteModal = document.querySelector('.modal');
    const table = document.querySelector('table');
    const message = "Are you sure you want to delete sheep with tag ";
    const actionPath = "/Sheep/Delete/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.setBodyMessage = "Are you sure you want to delete this sheep?"

    obj.fromTable(table);

    common.initDt("herdTable");
    common.initDt("feedlotTable");
}

export function create() {
    const form = document.querySelector('form');
    const input = form.querySelector('input[type="submit"]');
    common.preventDoubleSubmit(form, input);

    $('[data-selector="multi"]').each(function () {
        $(this).select2({
            theme: 'bootstrap4',
            width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
            placeholder: $(this).data('placeholder'),
            allowClear: Boolean($(this).data('allow-clear')),
            closeOnSelect: !$(this).attr('multiple'),
        });
    });
}

export function details() {
    const deleteModal = document.querySelector('.modal');
    const urlPath = window.location.pathname;
    const deletePath = "/Sheep/Delete/";
    const id = urlPath.substring(urlPath.lastIndexOf('/') + 1);
    const deleteButton = document.querySelector('.card-footer button');
    const form = deleteModal.querySelector('form');
    const tag = document.querySelector('.card-header').getAttribute('data-sb-tag');

    deleteModal.querySelector('.modal-title').innerHTML = `Delete sheep with tag ${tag}`;
    deleteModal.querySelector('.modal-body').innerHTML = "Are you sure you want to delete this sheep?"

    deleteButton.addEventListener('click', () => {
        form.action = deletePath + id;
    });
}

export function childDetails() {
    
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