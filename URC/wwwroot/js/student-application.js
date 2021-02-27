/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      10/21/2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did
 * not copy it in part or whole from another source.  Any references used
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * JavaScript to enable student resume upload
 */

async function AJAXSubmit(oFormElement) {
    $('#resume-spinner').removeClass('hide invisible')
    const formData = new FormData(oFormElement)
    formData.set('id', oFormElement.dataset.applicationId);
    try {
        const response = await fetch(oFormElement.action, {
            method: 'POST',
            body: formData
        })
        const data = await response.json()
        if (response.ok) {
            $('#response').html('Resume uploaded successfully!')
        } else {
            $('#response').html('An error has occurred: ' + data.message)
        }
    } catch (error) {
        $('#reponse').html('Error: ' + error)
        console.log('Error:', error)
    } finally {
        $('#resume-spinner').addClass('hide invisible')
    }
}

$(function () {
    $('#toggle-active-application').click(async function () {
        const isActive = $(this).data('is-student-active').toLowerCase() === 'true' // because C# send string instead of a JS boolean

        $.post('/Students/Apply', {
            id: $(this).data('student-id'),
            state: !isActive ? 'apply' : 'remove'
        })
            .then(() => {
                $(this).data('is-student-active', (!isActive + ''))
                $(this).removeClass('btn-primary btn-secondary').addClass(!isActive ? 'btn-primary' : 'btn-secondary')
                $(this).text(!isActive ? 'Looking for position' : 'Not looking for position')
            })
            .fail(err => {
                console.log('An error has occurred')
                console.log(err)
            })
    })
})
