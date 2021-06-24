//WebSocket = undefined;
//EventSource = undefined;





setupConnection = (hubProxy) => {

    hubProxy.on("receiveOrderUpdate", function (updateObject) {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Order: ${updateObject.OrderId}: ${updateObject.Update}`;
    });

    hubProxy.on("newOrder", function (order) {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Somebody ordered an ${order.Product}`;
    });
};

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

