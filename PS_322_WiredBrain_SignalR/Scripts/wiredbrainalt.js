//WebSocket = undefined;
//EventSource = undefined;





setupConnection = (hubProxy) => {

    hubProxy.on("receiveOrderUpdate", function (updateObject) {

        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Order: ${updateObject.OrderId}: ${updateObject.Update}`;

        updateOrderDiv("receiveOrderUpdate", updateObject);

    });

    hubProxy.on("newOrder", function (order) {

        updateOrderDiv("New Order:", order);

        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Somebody ordered an ${order.Product}`;

    });
};

// 09/26/2022 06:55 pm - SSN - Added
function updateOrderDiv(message, order_or_update) {

    let div = document.getElementById("ordersDiv");

    let p2 = null;
    let p3 = null;

    console.log(message);
    console.log(order_or_update);

    let orderId = 0;

    if (order_or_update.Product) {
        orderId = order_or_update.Id;
        p2 = document.createElement("p");
        p2.innerHTML = "Order No: " + orderId + " |  Product: " + order_or_update.Product;
    }

    if (order_or_update.Update) {
        p3 = document.createElement("p");
        p3.innerHTML = order_or_update.Update;
        orderId = order_or_update.OrderId;
    }



    let orderDivId = "div" + orderId;

    let orderDiv = document.getElementById(orderDivId);

    if (orderDiv == null) {
        orderDiv = document.createElement("div");
        orderDiv.id = orderDivId;
        orderDiv.className = "cssOrderDiv";
        div.insertAdjacentElement('afterbegin', orderDiv);
    }


    if (p2) orderDiv.appendChild(p2);

    if (p3) orderDiv.appendChild(p3);


}



$(document).ready(() => {




    function defineConnection() {



        // 06/24/2021 08:01 am - SSN - [20210624-0756] - [002] - M04-04-Transport-negotiation

        var connection = null;

        var hubProxy = null;


        let connectionOption = 1;

        switch (connectionOption) {

            case 1: {

                console.log('Testing dynamic connection setting (ALT script (1)...');

                connection = $.hubConnection();
                hubProxy = connection.createHubProxy('coffeeHub');

                break;
            }

            case 2: {

                console.log('Testing dynamic connection setting (ALT script (2))...');

                connection = new HubConnection("https://localhost:44315/");
                hubProxy = connection.CreateHubProxy("coffeeHub");

                console.log('Done creating connection.');

                break;
            }

            case 3: {

                console.log('Testing dynamic connection setting (ALT script (3))...');

                connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:44315/hubs/coffeeHub').build();
                hubProxy = connection.CreateHubProxy("coffeeHub");

                console.log('Done creating connection.');

                break;
            }


            case 4: {

                console.log('Testing dynamic connection setting (ALT script (4-v3))...');

                $.connection.hub.url = 'https://localhost:44315/';

                connection = $.hubConnection();

                console.log('connection:');
                console.log(connection);

                hubProxy = connection.createHubProxy('coffeeHub');

                console.log('Done creating connection.');

                break;
            }


            default: {
                throw new Error(`No case for connection option [{$}]`);
            }
        }







        setupConnection(hubProxy);



        connection.start();

        //connection.start({ transport: 'longPolling' });




        document.getElementById("submit").addEventListener("click",
            e => {
                e.preventDefault();



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
                    .then(response => response.text())
                    .then(id => hubProxy.invoke('getUpdateForOrder', { id, product, size })
                        .fail(error => console.log(error))
                    );
            });

    }



    setTimeout(() => defineConnection(), 1000);

});

