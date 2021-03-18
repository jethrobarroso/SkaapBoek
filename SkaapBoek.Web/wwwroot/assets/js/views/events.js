import * as common from '../common.js'

/**
 * Request to mark task as complete.
 * @param {number} id Task ID 
 * @param {HTMLElement} element Containing card
 */
export function overview() {
    $('.task-info-popover').popover();
    $('.mils-info-popover').popover({
        title: 'Additional Info',
        placement: 'bottom',
        html: true,
        content: '<div class="event-body">Loading...</div>'
    })

    document.querySelectorAll('mils-info-popover').forEach(item => {
        $(item).on('shown.bs.popover')
    })
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
        $(element.closest('.card')).fadeTo(200, 0).slideUp(200, function () {
            $(this).remove();
        });

        const mainCard = element.closest('.task-card-main');
        if (mainCard.querySelectorAll('.card').length == 1) {
            mainCard.querySelector('.alert').hidden = false;
        }
    }, 0);
}