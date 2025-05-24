"use strict";

const projectId = window.location.pathname.split("/").pop();
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chathub?projectId=${projectId}`)
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    const div = document.createElement("div");
    div.className = "chat-message" + (user === document.getElementById("userInput").value ? " me" : "");
    div.innerHTML = `<strong>${user}</strong>: ${message}`;
    const list = document.getElementById("messagesList");
    list.appendChild(div);
    list.scrollTop = list.scrollHeight;
});

connection.start().then(function () {
    console.log("✅ Connected to SignalR hub:", projectId);
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    console.error("❌ SignalR connection failed:", err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    if (!user || !message) return;

    connection.invoke("SendMessage", projectId, user, message).catch(function (err) {
        console.error("❌ Send failed:", err.toString());
    });

    document.getElementById("messageInput").value = "";
    event.preventDefault();
});
