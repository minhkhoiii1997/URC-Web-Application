/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      October 2nd, 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did
 * not copy it in part or whole from another source.  Any references used
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * JavaScript to make admin index page interactive
 */

$(document).ready(function () {
    let table = $('#users-roles-table').DataTable()

    // Sometimes table renders incorrectly, so force it to redraw
    table.draw()

    $('#users-roles-table').on('click', 'input', function (e) {

        let removeRole = !$(e.target).is(':checked')

        // Thanks to https://sweetalert.js.org/guides/
        $.post('Admin/ChangeRole', {
            removeRole,
            userId: $(e.target).closest('tr').data('user-id'),
            role: $(e.target).parent().data('role')
        })
            .done(result => {
                swal(`Successfully ${removeRole ? 'removed' : 'added'} role!`, result.message, 'success')
            })
            .fail(result => {
                // do something result
                swal(`Failed to ${removeRole ? 'remove' : 'add'} role.`, result.responseJSON.message, 'error')
                $(e.target).prop('checked', removeRole)
            })
    })
});
