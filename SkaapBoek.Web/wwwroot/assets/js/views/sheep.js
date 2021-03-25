import * as common from '../common.js';

export function index() {
    
    $("#herdTable").DataTable({
        "processing": true,
        "language": {
            processing: `<div class="spinner-border text-light" role="status">
                          <span class="sr-only">Loading...</span>
                        </div>`
        },
        "serverSide": true,
        "searchDelay": 1000,
        "filter": true,
        "ajax": {
            "url": "/api/sheep",
            "type": "POST",
            "datatype": "json",
            "data": { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() }
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }, {
            "targets": [9],
            "searchable": false,
            "orderable": false,

        }],
        "drawCallback": function (settings) {
            const body = document.querySelector('body');
            const buttonArray = [...this[0].querySelectorAll('tbody button')];
            buttonArray.forEach(item => {
                item.addEventListener('click', (item) => {
                    const id = item.srcElement.closest('.action-container')
                        .dataset.itemId;
                    const name = item.srcElement.closest('tr').firstElementChild.innerText;
                    common.prepTableRowDeleteModal({
                        modal : body.querySelector('#deleteModal'),
                        deletePath : '/Sheep/Delete',
                        actionId : id,
                        bodyMessage : `Are you sure you want to delete sheep ${name}?`,
                        titleMessage : "Delete Sheep"
                    });
                });
            });
            
        },
        "columns": [
            { "data": "id", "name": "id", "autoWidth": true },
            { "data": "tagNumber", "name": "tagNumber", "autoWidth": true },
            { "data": "color.name", "defaultContent": "<i>None</i>", "name": "color.name", "autoWidth": true },
            { "data": "sheepStatus.name", "defaultContent": "<i>None</i>", "name": "sheepStatus.name", "autoWidth": true },
            { "data": "gender.type", "defaultContent": "<i>None</i>", "name": "gender.type", "autoWidth": true },
            { "data": "weight", "name": "weight", "autoWidth": true },
            { "data": "birthDate", "name": "birthDate", "autoWidth": true },
            { "data": "mother.tagNumber", "defaultContent": "<i>None</i>", "name": "mother.tagNumber", "autoWidth": true },
            { "data": "father.tagNumber", "defaultContent": "<i>None</i>", "name": "father.tagNumber", "autoWidth": true },
            {
                "data" : "id",
                "render": function (data, type, row, meta) {
                    return common.buildTableRowActions({
                        detailsPath : "/Sheep/Details",
                        editPath : "/Sheep/Edit",
                        itemId : row.id
                    });
                }
            },
        ]
    });

    $('#deleteModal').on('show.bs.modal', function (event) {
        console.log(event);
        let sheepId = $(event.relatedTarget).closest('[data-item-id]').data('itemId');
        let tagName = event.relatedTarget.closest('tr').firstElementChild.innerText;

        this.querySelector('.modal-body').innerHTML = `Are you sure you want to delete ${tagName.trim()}?`;
        this.querySelector('form').action = `/Sheep/Delete/${sheepId}`;
    })
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

    $('table').DataTable({
        searchDelay: 1000,
        columnDefs: [{
            targets: -1,
            searchable: false,
            orderable: false
        }]
    });

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