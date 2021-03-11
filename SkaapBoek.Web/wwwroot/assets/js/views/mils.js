import * as common from '../common.js';

export function index() {
    const body = document.querySelector('body');
    const deleteButtons = body.querySelectorAll('.fadable-buttons-container .fadable-button');
    const modalBody = body.querySelector('.modal-body');
    const modalForm = body.querySelector('.modal-footer form');

    deleteButtons.forEach((button, index) => {
        button.addEventListener('click', (e) => {
            const id = e.srcElement.parentElement.getAttribute('data-user-id');
            if (e.srcElement.innerHTML === "delete") {
                modalBody.innerHTML = `Are you sure you want to delete this phase?<br>
                                         <strong>NOTE:</strong> This will remove all related
                                         tasks.`;
                modalForm.action = 'Mils/DeletePhase/' + id;
                $('#deleteModal').modal('toggle');
            }
            e.stopPropagation();
        });
    });

    const accordion = body.querySelector('.accordion');
    if (accordion) {
        Sortable.create(accordion, {
            handle: '.drag-icon',
            animation: 150,
            onUpdate: function (event) {
                let data = {
                    "OldSequence": event.oldIndex + 1,
                    "NewSequence": event.newIndex + 1
                };
                fetch('/api/Mils/UpdatePhaseSequence/', {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data),
                })
                    .catch(error => console.log('Error: ', error))
            }
        });
    }

    const assignGroupForm = body.querySelector('#assignGroupForm');
    assignGroupForm.addEventListener('submit', e => {
        e.preventDefault();
        addGroupToPhase();
    })

    body.que

    $('#GroupId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select group"
    });

    $('#PenId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select pen"
    });
}

/**
 * Prepare task edit modal with relevant data
 * @param {number} groupId Group ID
 * @param {HTMLElement} element Passed in from onclick property
 */
export function unlinkGroup(groupId, element) {
    const body = document.querySelector('body');
    const unlinkModal = body.querySelector('#unlinkGroupModal');
    const groupName = element.closest("tr").firstElementChild.innerHTML;

    if (unlinkModal) {
        unlinkModal.querySelector('form').action = `/Mils/RemoveGroup/${groupId}`;
        let messageBody = `Are you sure you want to remove ${groupName} from this phase?`;
        unlinkModal.querySelector('.modal-body').innerHTML = messageBody;
    }
}

export function editPhase() {
    const body = document.querySelector('body');
    const deleteModal = body.querySelector('#deleteModal');
    const table = body.querySelector('#phaseTaskTable');
    const message = "Are you sure you want to delete task ";
    const actionPath = "/Mils/DeleteTask/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);

    const taskModel = body.querySelector('#addEditTaskModal');
    taskModel.addEventListener('submit', e => {
        
    })

    $('#GroupId').select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: "Select group"
    });
}

export function addGroupToPhase() {
    const body = document.querySelector('body');
    const groupForm = body.querySelector('#assignGroupForm');
    const phaseId = groupForm.querySelector('#MilsPhaseId').value;
    let formData = new FormData(groupForm);

    groupForm.addEventListener('submit', e => {
        e.preventDefault();
    });
    
    if ($('#assignGroupForm').valid()) {
        fetch('/Mils/AssignGroup', {
            method: 'post',
            body: formData
        })
            .then(() => {
                fetch(`/Mils/GetPhaseGroups/${phaseId}`, {
                    method: 'get',
                })
                    .then(result => result.text())
                    .then(content => {
                        document.querySelector(`#groupTable-${phaseId}`).innerHTML = content;
                        $('#assignGroupModal').modal('toggle');
                    })
                fetch('/Mils/RenderAssignGroupForm', {
                    method: 'get'
                })
                    .then(result => result.text())
                    .then(formContent => {
                        groupForm.parentElement.innerHTML = formContent;
                        initSelect2Item('#SelectedGroupId', 'Select group');
                    })
            })
            .catch((error) => {
                alert(error);
            });
    }
    
    return false;
}

/**
 * Enable select2 feature on select element
 * @param {string} selector CSS selector for the element
 * @param {string} placeholder Select placeholder value
 */
function initSelect2Item(selector, placeholder) {
    $(selector).val(null).empty().select2('destroy')
    $(selector).select2({
        theme: 'bootstrap4',
        allowClear: true,
        placeholder: placeholder
    });
}

/**
 * Prepare task edit modal with relevant data
 * @param {number} id Task ID
 * @param {HTMLElement} element Passed in from onclick property
 */
export function editPhaseTask(id, element) {
    const body = document.querySelector('body');
    const taskModal = body.querySelector('#addEditTaskModal');
    const modalForm = taskModal.querySelector('form');
    taskModal.querySelector('.modal-title').innerHTML = 'Edit phase task';
    const instructions = element.closest("tr").firstElementChild.innerHTML;

    if (taskModal) {
        modalForm.querySelector('#MilsTaskId').value = id;
        taskModal.querySelector('form').action = `/Mils/EditTask/${id}`;
        taskModal.querySelector('#Instructions').innerHTML = instructions;
    }

    taskModal.querySelector('#taskFormSubmitButton').innerHTML = 'Update';
}

export function addPhaseTask(id) {
    const body = document.querySelector('body');
    const taskModal = body.querySelector('#addEditTaskModal');
    taskModal.querySelector('.modal-title').innerHTML = 'Create phase task';

    if (taskModal) {
        taskModal.querySelector('form').action = `/Mils/CreateTask/${id}`;
        taskModal.querySelector('#Instructions').innerHTML = '';
    }
    taskModal.querySelector('#taskFormSubmitButton').innerHTML = 'Create';
}

export function assignGroup(groupId, penId) {
    document.querySelector('#assignGroupForm #MilsPhaseId').value = groupId;
    document.querySelector('#assignGroupForm #addGroupPenId').value = penId;
}