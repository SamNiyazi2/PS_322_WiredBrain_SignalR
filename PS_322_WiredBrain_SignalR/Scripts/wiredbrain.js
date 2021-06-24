
function cl(msg) {
     
    console.log('%c ' + msg, 'color:white;font-size:10px;');
}


setupConnection = (hubProxy) => {

    hubProxy.client.receiveOrderUpdate = (updateObject) => {

        cl('client.receiveOrderUpdate - 1101');

        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Order: ${updateObject.OrderId}: ${updateObject.Update}`;
    };

    hubProxy.client.newOrder = (order) => {

        cl('client.newOrder - 1102');

        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Somebody ordered an ${order.Product}`;
    };

    hubProxy.client.finished = (order) => {

        cl('client.finished - 1103');

        //stop? $.connection.hub.stop();
        console.log(`Finished coffee order ${order}`);
    };
};



$(document).ready(() => {

    cl('document.ready - 1201');

    var hubProxy = $.connection.coffeeHub;
    setupConnection(hubProxy);
    $.connection.hub.start();

    document.getElementById("submit").addEventListener("click",
        e => {

            e.preventDefault();

            cl('submit - 1201');

            var statusDiv = document.getElementById("status");
            statusDiv.innerHTML = "Submitting order..";

            const product = document.getElementById("product").value;
            const size = document.getElementById("size").value;

            fetch("api/Coffee",
                {
                    method: "POST",
                    body: JSON.stringify({ product, size }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .then(response => {

                    cl('response:');
                    cl(response);

                    return response.text();

                } )
                .then(id => {

                    cl('response id:');
                    cl(id);

                    return hubProxy.server
                        .getUpdateForOrder({ id, product, size })
                        .fail(error => console.log(error))
                });

        });
});