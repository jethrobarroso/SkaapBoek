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
}



export function editPhase() {
    const deleteModal = document.querySelector('#deleteModal');
    const table = document.querySelector('#phaseTaskTable');
    const message = "Are you sure you want to delete task ";
    const actionPath = "/Mils/DeleteTask/";
    const obj = new common.ModalDeletePrompt(deleteModal, actionPath, message);

    obj.fromTable(table);


}