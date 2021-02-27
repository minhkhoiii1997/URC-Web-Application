/**
 * Author:    Kevin Tran
 * Partner:   Calvin Tu
 * Date:      September, 2020
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.
 *
 * We, Kevin Tran, Calvin Tu certify that we wrote this code from scratch and did
 * not copy it in part or whole from another source.  Any references used
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 * File Contents
 * JavaScript to enable cover image upload for a research opportunity
 */

async function AJAXSubmit(oFormElement) {
    $('#resume-spinner').removeClass('hide invisible')
    const formData = new FormData(oFormElement)
    formData.set('id', oFormElement.dataset.opportunityId);
    try {
        const response = await fetch(oFormElement.action, {
            method: 'POST',
            body: formData
        })
        const data = await response.json()
        if (response.ok) {
            $('#response').html('Image uploaded successfully!')
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
    const id = $('#opp-id-div').data('opportunity-id')
    $('#tag-input button[type=submit]').click(function () {
        const value = $('#tag-input input').val()
        $.post('/Opportunities/AddTag/', { type: 'searchtag', value, id })
            .then(function () {
                if ($('#opportunity-searchtags li[data-empty]')) {
                    $('#opportunity-searchtags li[data-empty]').remove()
                }

                $('#opportunity-searchtags').append(`
                    <li>
                        <button class="remove-tag" data-type="searchtag" data-value="${value}">X</button>
                        <span>${value}</span>
                    </li>`
                )
            })
            .fail(function (err) {
                alert('an error has occurred')
                console.log(err)
            })
    })

    $('#skill-input button[type=submit]').click(function () {
        const type = $('#skill-input select').val()
        const value = $('#skill-input input').val()
        $.post('/Opportunities/AddTag/', { type, value, id })
            .then(function () {
                if ($(`#opportunity-${type}s li[data-empty]`)) {
                    $(`#opportunity-${type}s li[data-empty]`).remove()
                }

                $(`#opportunity-${type}s`).append(`
                    <li>
                        <button class="remove-tag" data-type="${type}" data-value="${value}">X</button>
                        <span>${value}</span>
                    </li>`
                )
            })
            .fail(function (err) {
                alert('an error has occurred')
                console.log(err)
            })
    })

    $('#tags-container').on('click', 'button.remove-tag', function (e) {
        const type = $(e.target).data('type')
        const value = $(e.target).data('value')
        $.post('/Opportunities/RemoveTag', { type, value, id })
            .then(function () {
                $(e.target).parent().remove()
                if ($(`#opportunity-${type}s`).children().length == 0) {
                    let typestr
                    if (type == 'searchtag') {
                        typestr = 'search tags'
                    } else if (type == 'requiredskill') {
                        typestr = 'required skills'
                    } else {
                        typestr = 'preferred skills'
                    }
                    $(`#opportunity-${type}s`).append(`<li data-empty>You have no ${typestr} listed yet.</li>`)
                }
            })
            .fail(function (err) {
                alert('an error has occurred')
                console.log(err)
            })
    })
})
