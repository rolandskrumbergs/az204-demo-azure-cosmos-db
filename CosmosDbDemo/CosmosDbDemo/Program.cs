using CosmosDbDemo;
using Microsoft.Azure.Cosmos;

var cosmosDbConnectionString = "";
var database = "demo";
var container = "orders";

var cosmosClient = new CosmosClient(cosmosDbConnectionString, new CosmosClientOptions
{
    // Try to change this
    ConsistencyLevel = ConsistencyLevel.Session,
    MaxRetryAttemptsOnRateLimitedRequests = 10,
    MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(10)
});


var containerClient = cosmosClient.GetContainer(database, container);

var createResponse = await containerClient.CreateItemAsync(new Order
{
    Id = Guid.NewGuid().ToString(),
    Created = DateTime.Now,
    OrderType = "Online"
});

Console.WriteLine($"Create order request took: {createResponse.RequestCharge}");

createResponse = await containerClient.CreateItemAsync(new Order
{
    Id = Guid.NewGuid().ToString(),
    Created = DateTime.Now,
    OrderType = "Online"
});

Console.WriteLine($"Create order request took: {createResponse.RequestCharge}");

createResponse = await containerClient.CreateItemAsync(new Order
{
    Id = Guid.NewGuid().ToString(),
    Created = DateTime.Now,
    OrderType = "Manual"
});

Console.WriteLine($"Create order request took: {createResponse.RequestCharge}");

var queryDefinition = new QueryDefinition("SELECT * FROM orders");

using FeedIterator<Order> feed = containerClient.GetItemQueryIterator<Order>(queryDefinition);

while (feed.HasMoreResults)
{
    FeedResponse<Order> response = await feed.ReadNextAsync();
    Console.WriteLine($"Request took: {response.RequestCharge}");
    foreach (Order item in response)
    {
        Console.WriteLine($"Found item with Id = {item.Id}.");
    }
}

