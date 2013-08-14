(function() {
    $(document).ready(function() {
        var connection = $.hubConnection();
        var stockHubProxy = connection.createHubProxy('stockHub');
        stockHubProxy.on('updateStockPrice', function (price) {
            console.log('Price updated to ' + price);
        });
        connection.start().done(function() {
            // Wire up Send button to call NewContosoChatMessage on the server.
            $('#updatePrice').click(function() {
                stockHubProxy.invoke('notifyNewStock', $('#price').val());
                $('#price').val('').focus();
            });
        });
    });
}());
