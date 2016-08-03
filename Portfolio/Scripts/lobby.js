
class Lobby {

    serverMessage(message,name) {
        
    }

    connect(username) {
        this.hub.server.connect(username);
    }

    displayServerMessage(message) {
        if (this.serverStatusContainer != 'undefined') {

            var encodedMessage = $('<div />').text(message).html();

            this.serverStatusContainer.prepend('<li><strong>Server:'
            + '</strong>:&nbsp;&nbsp;' + encodedMessage + '</li>');
        }
    }

    setupCallbacks() {
        this.hub.client.serverStatusLog = function (message) {
            lobby.displayServerMessage(message);
        };
    }

    init() {

        this.hub = $.connection.colorWarsHub;

        lobby.setupCallbacks();
        
        $.connection.hub.start()
            .done(function () {

                
                lobby.displayServerMessage('Server connection initialized.');
                lobby.initialized = true;
            })
            .fail(function () {
                lobby.displayServerMessage('Server connection failed, retrying.');
                lobby.init();
            });

        
    }
}

var lobby = new Lobby;
