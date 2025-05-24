"use strict";

const projectId = window.location.pathname.split("/").pop();
const messagesList = document.getElementById("messagesList");
const currentUser = () => document.getElementById("userInput").value;

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chathub?projectId=${projectId}`)
    .build();

document.getElementById("sendButton").disabled = true;

// ⏳ Step 1: Load chat history from the API
fetch(`/api/chatapi/${projectId}`)
    .then(response => response.json())
    .then(data => {
        data.forEach(msg => {
            const div = document.createElement("div");
            div.className = "chat-message";
            div.innerHTML = `<strong>${msg.user}</strong> <small class="text-muted">${msg.timestamp}</small><br>${msg.message}`;
            messagesList.appendChild(div);
        });

        messagesList.scrollTop = messagesList.scrollHeight;
    })
    .catch(error => {
        console.error("❌ Error fetching history:", error);
    });

// 💬 Real-time messages
connection.on("ReceiveMessage", function (user, message) {
    const div = document.createElement("div");
    div.className = "chat-message" + (user === currentUser() ? " me" : "");
    div.innerHTML = `<strong>${user}</strong>: ${message}`;
    messagesList.appendChild(div);
    messagesList.scrollTop = messagesList.scrollHeight;
});

// 🔌 Start connection
connection.start().then(function () {
    console.log("✅ Connected to SignalR hub:", projectId);
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    console.error("❌ SignalR connection failed:", err.toString());
});

// 📤 Send message
document.getElementById("sendButton").addEventListener("click", function (event) {
    const user = currentUser();
    const message = document.getElementById("messageInput").value;
    if (!user || !message) return;

    connection.invoke("SendMessage", projectId, user, message).catch(function (err) {
        console.error("❌ Send failed:", err.toString());
    });

    document.getElementById("messageInput").value = "";
    event.preventDefault();
});
