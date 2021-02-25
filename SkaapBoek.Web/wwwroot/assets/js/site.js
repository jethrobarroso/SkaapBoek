import * as feedViews from './views/feed.js'
import * as sheepViews from './views/sheep.js'
import * as groupViews from './views/groups.js'
import * as taskViews from './views/tasks.js'
import * as penViews from './views/pen.js'
import * as milsViews from './views/mils.js'

document.addEventListener("DOMContentLoaded", () => {
    $('[data-toggle="tooltip"]').tooltip();
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    window.appns = {
        //account: {
        //    register: accountRegister
        //},
        //administration: {
        //    listusers: adminListUsers,
        //    listroles: adminListRoles
        //}
        feed: {
            index: feedViews.index,
            details: feedViews.details,
            create: feedViews.create
        },
        sheep: {
            index: sheepViews.index,
            create: sheepViews.create,
            details: sheepViews.details,
            edit: sheepViews.edit,
        },
        groups: {
            index: groupViews.index,
            details: groupViews.details,
            edit: groupViews.edit,
            create: groupViews.create,
        },
        pen: {
            index: penViews.index,
            details: penViews.details,
            edit: penViews.edit,
            create: penViews.create,
        },
        tasks: {
            index: taskViews.index,
            details: taskViews.details,
            create: taskViews.create,
            edit: taskViews.edit,
        },
        mils: {
            index: milsViews.index,
            editPhase: milsViews.editPhase,
            editPhaseTask: milsViews.editPhaseTask,
        }
    }
})