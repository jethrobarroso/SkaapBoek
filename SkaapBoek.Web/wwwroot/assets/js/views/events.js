/**
 * Request to mark task as complete.
 * @param {number} id Task ID 
 * @param {HTMLElement} element Containing card
 */
export function overview() {
    $('.task-info-popover').popover();
    $('.mils-info-popover').each(function (index) {
        let groupId = $(this).closest('[data-group-id]').attr('data-group-id');
        $(this).popover({
            title: 'Additional Info',
            placement: 'bottom',
            html: true,
            content: `<div class="popover-wrapper group-id-${groupId}">Loading...</div>`
        });
    })
    

    $('.mils-info-popover').on('shown.bs.popover', function () {
        let groupId = $(this).closest('[data-group-id]').attr('data-group-id');
        $.ajax({
            url: `/Events/RenderMilsEventInfo/${groupId}`,
            type: "GET",
            success: function (result) {
                $(`.group-id-${groupId}`).html(result);
            },
            error: function (error) {
                $(`.group-id-${groupId}`).html('An error has occured fetching the data');
            }
        });
    });

    const body = document.querySelector('body');
    const moveToPhaseForm = body.querySelector('#moveToPhaseModal form');
    const groupIdInput = moveToPhaseForm.querySelector('#GroupId');
    const moveGroupSubmitBtn = body.querySelector('#moveGroupBtn');
    const removeGroupSubmitBtn = body.querySelector('#removeGroupBtn');

    body.querySelectorAll('[data-target="#moveToPhaseModal"]').forEach(submitBtn => {
        submitBtn.addEventListener('click', () => {
            groupIdInput.value = submitBtn.closest('[data-group-id]').dataset.groupId;
        });
    });

    moveGroupSubmitBtn.addEventListener('click', (e) => {
        let moveGroupFromData = new FormData(moveToPhaseForm);
        fetch(`/Mils/MoveGroupToPhase`, {
            method: 'PUT',
            body: moveGroupFromData,
        })
            .then(response => {
                let groupId = moveGroupFromData.get('GroupId');
                let cardToRemove = body.querySelector(`[data-group-id="${groupId}"]`);
                $('#moveToPhaseModal').modal('toggle');
                removeEventCard(cardToRemove)
            })
            .catch(error => {
            console.error('Error: ', error);
        })
    });
}

export function completeTask(id, element) {
    fetch(`/api/Tasks/${id}/Complete`, {
        method: 'put',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(result => {
        if (result.status == 204) {
            removeEventCard(element);
        }
    }).catch(error => alert(error));
}

export function markAsInProgress(id, element) {
    fetch(`/api/Tasks/${id}/MarkAsInProgress`, {
        method: 'put',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(result => {
        if (result.status == 204) {
            removeEventCard(element);
        }
    }).catch(error => alert(error));
}

function removeEventCard(element) {
    window.setTimeout(function () {
        let card = element.classList.contains('card') ? $(element) : $(element.closest('.card'));
        card.fadeTo(200, 0).slideUp(200, function () {
            $(this).remove();
        });

        const mainCard = element.closest('.event-card-main');
        if (mainCard.querySelectorAll('.card').length == 1) {
            mainCard.querySelector('.alert').hidden = false;
        }
    }, 0);
}