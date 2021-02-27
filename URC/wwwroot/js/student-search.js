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
 * JavaScript to enable student search by keyword
 */
$(function () {
    const coverImages = [
        'https://www.cs.utah.edu/wp-content/uploads/2014/12/rajeev_balasubramoniansm.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2015/01/david_johnson.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2016/10/dkopta.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2019/07/PSadayappan.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2015/07/ryan-stutsman.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2015/01/jim_destgermain.jpg',
        'https://www.cs.utah.edu/wp-content/uploads/2015/01/jeff_phillips.jpg'
    ]

    function getRandomProfileImage() {
        return coverImages[Math.floor(Math.random() * coverImages.length)]
    }

    // Implemented search-as-you-type based on previous experience
    // https://github.com/ktucalvin?tab=repositories

    let lastSearch = ''
    function search() {
        const keywords = $('#student-search-bar')[0].value
        if (!keywords) {
            $('#student-search-results').empty()
        }
        if (keywords === lastSearch) return
        lastSearch = keywords

        $('#student-search-error').text('')
        // Thanks https://api.jquery.com/jQuery.ajax/
        $.ajax({
            method: 'POST',
            url: '/Students/Find',
            headers: {
                // Thanks https://stackoverflow.com/questions/57727595/send-requestverificationtoken-with-fetch-api-and-recieve-with-an-validateanti
                'X-CSRF-TOKEN-URC': $('#RequestVerificationToken')[0].value
            },
            data: { keywords }
        })
            .then(res => {
                $('#student-search-results').empty()
                for (const student of res) {
                    const desc = student.summary.length < 100 ? student.summary : student.summary.slice(0, 97) + '...'
                    let skills = student.skills.join(', ')
                    skills = skills.length < 100 ? skills : skills.slice(0, 97) + '...'

                    const card = $('#student-card-template').clone()
                    card.find('.card-img-top').attr('src', getRandomProfileImage())
                    card.find('.card-title').text(student.name)
                    card.find('.student-search-summary').text(desc)
                    card.find('.student-search-uid').text(student.uId)
                    card.find('.student-search-gpa').text(student.gpa)
                    card.find('.student-search-skills').text(skills)
                    card.find('a').attr('href', `/Students/Details/${student.applicationId}`)

                    card.removeClass('hide invisible')
                    $('#student-search-results').append(card)
                    card.show()
                }
            })
            .fail(err => {
                $('#student-search-error').text('An error has occurred while searching')
                console.error(err)
            })
    }


    $('#student-card-template').hide()

    let searchTimeout = 0

    $('#student-search-bar').keyup(function () {
        clearTimeout(searchTimeout)
        searchTimeout = setTimeout(() => search(), 150)
    })

    // Thanks https://stackoverflow.com/questions/2977023/how-do-you-detect-the-clearing-of-a-search-html5-input
    $('#student-search-bar').on('search', function () {
        const keywords = $('#student-search-bar')[0].value
        if (!keywords) {
            $('#student-search-results').empty()
        }
    })

    $('#submit-student-search').click(() => search())
})