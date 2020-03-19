using Newtonsoft.Json;
using RestSharp;
using SharedFiles;

namespace MartianRobotsClient
{
    public class RestApiClient
    {
        private RestClient client;

        public bool CreateRestClient(string url = "http://localhost:8080/")
        {
            client = new RestClient(url);
            return client != null;
        }

        public string[,] GetMapRequest()
        {
            var getRequest = new RestRequest("/api/values", Method.GET);
            var getResponse = client.Execute(getRequest);
            var getResponseDeserialized = JsonConvert.DeserializeObject<string[,]>(getResponse.Content);
            return getResponseDeserialized;
        }

        public string GetMapRequest(int x, int y)
        {
            var getRequest = new RestRequest($"/api/values/{x}/{y}", Method.GET);
            var getResponse = client.Execute(getRequest);
            var getResponseDeserialized = JsonConvert.DeserializeObject<string>(getResponse.Content);
            return getResponseDeserialized;
        }

        public GetResponse PostRequest(string command)
        {
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            var getResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(getResponse.Content);
            return postResponseDeserialized;
        }
    }
}