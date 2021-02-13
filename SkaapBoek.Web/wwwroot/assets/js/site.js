import * as feedViews from './views/feed.js'
import * as sheepViews from './views/sheep.js'
import * as templateViews from './views/templates.js'
import * as projectViews from './views/projects.js'
import * as groupViews from './views/groups.js'
import * as taskViews from './views/tasks.js'

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
            list: feedViews.list,
            details: feedViews.details,
            create: feedViews.create
        },
        sheep: {
            list: sheepViews.list,
            create: sheepViews.create,
            details: sheepViews.details,
            editChild: sheepViews.editChild,
            childDetails: sheepViews.childDetails,
            edit: sheepViews.edit,
        },
        templates: {
            list: templateViews.list,
            details: templateViews.details,
        },
        projects: {
            list: projectViews.list,
            details: projectViews.details,
            edit: projectViews.edit,
        },
        groups: {
            list: groupViews.list,
            details: groupViews.details,
            edit: groupViews.edit,
        },
        tasks: {
            list: taskViews.list,
            details: taskViews.details,
        }
    }
})