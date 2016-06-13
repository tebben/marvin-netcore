var serversocket = new WebSocket("ws://localhost:5000/actions");

serversocket.onopen = function() {

}

// Write message on receive
serversocket.onmessage = function(e) {
    console.log("socket message: " + e.data);
};

function fireAction(actionMessage) {
    console.log("sending: " + actionMessage);
    serversocket.send(actionMessage);
}