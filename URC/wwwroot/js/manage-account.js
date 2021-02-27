$(function () {
    $('#tag-input button[type=submit]').click(function () {
        const type = $('#tag-input select').val()
        const value = $('#tag-input input').val()
        $.post('/Students/AddTag/', { type, value })
            .then(function () {
                if ($(`#profile-${type}s button[data-value="${value}"]`)[0]) {
                    return
                }

                if ($(`#profile-${type}s li[data-empty]`)) {
                    $(`#profile-${type}s li[data-empty]`).remove()
                }

                $(`#profile-${type}s`).append(`
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

    $('#tags').on('click', 'button.remove-tag', function (e) {
        const type = $(e.target).data('type')
        const value = $(e.target).data('value')
        $.post('/Students/RemoveTag', { type, value })
            .then(function () {
                $(e.target).parent().remove()
                if ($(`#profile-${type}s`).children().length == 0) {
                    $(`#profile-${type}s`).append(`<li data-empty>You have no ${type}s listed yet.</li>`)
                }
            })
            .fail(function (err) {
                alert('an error has occurred')
                console.log(err)
            })
    })
})