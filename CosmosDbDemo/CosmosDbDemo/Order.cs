using Newtonsoft.Json;

namespace CosmosDbDemo
{
    public class Order
    {
        // v3 uses Newtonsoft, v4 uses System.Text.Json
        [JsonProperty("id")]
        public required string Id { get; set; }
        public DateTime Created { get; set; }
        public required string OrderType { get; set; }

    }
}
