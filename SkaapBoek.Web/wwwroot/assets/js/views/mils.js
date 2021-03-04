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

    $('#SelectedGroupId').select2({
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
    const deleteModal = document.querySelector('#deleteModal');
    const table = document.querySelector('#phaseTaskTable');
    const message = "Are you sure you want to delete task ";
    const actionPath = "/Mils/DeleteTask/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);
}

/**
 * Prepare task edit modal with relevant data
 * @param {number} id Task ID
 * @param {HTMLElement} element Passed in from onclick property
 */
export function editPhaseTask(id, element) {
    const body = document.querySelector('body');
    const editModal = body.querySelector('#editTaskModal');
    const instructions = element.closest("tr").firstElementChild.innerHTML;

    if (editModal) {
        editModal.querySelector('form').action = `/Mils/EditTask/${id}`;
        editModal.querySelector('#Instructions').innerHTML = instructions;
    }
}

export function assignGroup(groupId) {
    const phaseIdInput = document.querySelector('#assignGroupForm #MilsPhaseId');
    phaseIdInput.value = groupId;
}