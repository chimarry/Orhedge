"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var templateString = '<li class="in">     <div class="chat-img">   <div class="avatar-circle"><span class="initials">MN</span></div>  </div>  <div class="chat-body">   <div class="chat-message">  <h5>' + user + '</h5>  <p>' + msg + '</p> </div> </div> </li>'
    $('#messagesList').append(templateString);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function page(pageNumber) {
    var url = $("#PageRedirect").val() + "?pageNumber=" + pageNumber;
    location.href = url;
}

function deleteMessage(chatMessageId) {
    var url = $("#MessageDeleteRedirect").val() + "?chatMessageId=" + chatMessageId;
    location.href = url;
}