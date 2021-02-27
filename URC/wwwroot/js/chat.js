"use strict";
$(function () {
    // Code example from ASP Core Signal R Tutorial
    let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    const isViewerStudent = $('#is-viewer-student').val()
    let user = isViewerStudent ? $('#viewer-id').val() : $('#profile-id').val()
    let professor = isViewerStudent ? $('#profile-id').val() : $('#viewer-id').val() 

    console.log(`Is user student? ${isViewerStudent}`)
    console.log(`Joining room: ${user}${professor}`)

    // Disable send button until connection is established
    $("#sendButton").hide();

    // Escape code from Mustache library:
    // https://github.com/janl/mustache.js/blob/master/mustache.js#L73
    var entityMap = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;',
        '/': '&#x2F;',
        '`': '&#x60;',
        '=': '&#x3D;'
    };

    function escapeHtml(string) {
        return String(string).replace(/[&<>"'`=\/]/g,
            function fromEntityMap(s) { return entityMap[s] });
    }
    ///////////// end escape code from Mustache library: //////////////

    $("#exitButton").click(
        function (event) {
            connection.invoke("LeaveRoom", user, professor)
                .catch(function (err) {
                    return console.error(err.toString());
                });
            $("#sendButton").prop("disabled", true)
        }
    )

    /**
     * What to do when a message comes from the Server
     */
    connection.on("ReceiveMessage", function (time, user, message, msgId) {
        console.log('raw message: ' + message)
        let encoded_message = escapeHtml(message);
        console.log('decoded message: ' + encoded_message)

        $("#chatbox").append(`<p id="id${msgId}"><b>${user}</b> <span>${time}</span> ${encoded_message} </p>`);
        $(`#id${msgId}`).hide();
        $(`#id${msgId}`).fadeIn("slow");

        setTimeout(function () { $(`#id${msgId}`).toggleClass("highlight", 1000) }, 5000);

        $('#chatbox').scrollTop($('#chatbox')[0].scrollHeight);
    });

    /**
     *  Main initiation of web socket connection (via signalR)
     */
    connection
        .start()
        .then(
            function () {
                connection.invoke("JoinRoom", user, professor)
                    .catch(function (err) {
                        return console.error(err.toString());
                    });
                connection.invoke("LoadMessage", user, professor)
                    .catch(function (err) {
                        return console.error(err.toString());
                    });

                $("#sendButton").show();
                $("#chatbox").append('<p><i>Joined room successfully</i></p>')
            })
        .catch(
            function (err) {
                return console.error(err.toString());
            });

    //Thanks to https://www.w3schools.com/howto/howto_js_trigger_button_enter.asp
    //Enable enter button for message
    var input = document.getElementById("messageInput");
    input.addEventListener("keypress", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("sendButton").click();
        }
    });

    //event listerner for Send button
    $("#sendButton").click(
        function (event) {
            console.log(event)

            //getting value from user input
            let message = $("#messageInput").val();

            connection.invoke("SendMessage", user, message, professor, !!isViewerStudent)
                .catch(function (err) {
                    return console.error(err.toString());
                });
            // reset message input box for the next messages...
            message = $("#messageInput").val('').focus();
            // thanks to https://stackoverflow.com/questions/34348260/how-to-auto-scroll-down-to-list-while-messages-are-coming
            // enable auto scroll
            $('#chatbox').scrollTop($('#chatbox')[0].scrollHeight);

        });
})