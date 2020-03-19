using System;
using System.Configuration;
using MartianRobotsClient;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SharedFiles;

namespace AutoTests
{
    /// <summary>
    /// We do not test UI, because UI form is made with protection from
    /// invalid input in all fields
    /// </summary>
    public class UnitTest1
    {
        private RestClient client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            client = new RestClient("http://localhost:8080/");
            var getRequest = new RestRequest("/api/values", Method.GET);
            client.Execute(getRequest);
        }

        /// <summary>
        /// We do suicide on robot for each test, witch is sad.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var deleteRequest = new RestRequest("/api/values", Method.DELETE);
            client.Execute(deleteRequest);
        }

        [Test]
        public void Test_010_ShouldCreateInstance()
        {
            //Prepare

            //Act
            var restClient = new RestClient("http://localhost:8080/");

            //Assert
            Assert.NotNull(restClient);
        }

        [Test]
        public void Test_020_ShouldCreateMapAs2DArray()
        {
            //Prepare
            var getRequest = new RestRequest("/api/values", Method.GET);
            //Act
            var getResponse = client.Execute(getRequest);
            var getResponseDeserialized = JsonConvert.DeserializeObject<string[,]>(getResponse.Content);
            //Assert
            Assert.NotNull(getResponse);
            Assert.IsTrue(getResponse.Content.Contains("W"));
            Assert.IsInstanceOf<Array>(getResponseDeserialized);
        }

        [Test]
        public void Test_030_ShouldCreateMapPoint_ShouldReturnString()
        {
            //1,1 is a safe spot for robot spawn, so it`s always equals to "o"
            var x = 1;
            var y = 1;
            //Prepare
            var getResponse = GetMapCellStatus(x, y);
            //Assert
            Assert.AreNotEqual(string.Empty,getResponse);
            Assert.IsInstanceOf<string>(getResponse);
            Assert.AreEqual(getResponse, "o");
        }

        [Test]
        public void Test_040_DeathShouldResetPosition()
        {
            //Prepare
            var command = "F";
            var deleteRequest = new RestRequest("/api/values", Method.DELETE);
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);

            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //We moved 1 north
            Assert.AreEqual(2, postResponseDeserialized.Y);
            client.Execute(deleteRequest);

            var status = GetRobotStatus();

            //Assert
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(1, status.Y);
        }

        [Test]
        public void Test_050_EmptyPostCommandShouldReturnGetResponse()
        {
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody("");
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.NotNull(postResponse);
            Assert.IsInstanceOf<GetResponse>(postResponseDeserialized);
        }

        [Test]
        public void Test_060_StartPositionIsValid()
        {
            //Starting values from RobotController
            var x = 1;
            var y = 1;
            var scent = false;
            GetResponse.Directions dirs = GetResponse.Directions.North;

            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody("");
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.AreEqual(x, postResponseDeserialized.X);
            Assert.AreEqual(y, postResponseDeserialized.Y);
            Assert.AreEqual(scent, postResponseDeserialized.Scent);
            Assert.AreEqual(GetResponse.Directions.North, postResponseDeserialized.Direction);
        }

        [Test]
        public void Test_070_LCommandShouldTurnLeft()
        {
            var command = "L";
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.NotNull(postResponse);
            Assert.AreEqual(GetResponse.Directions.West, postResponseDeserialized.Direction);
        }

        [Test]
        public void Test_080_RTurnShouldTurnRight()
        {
            var command = "R";
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.NotNull(postResponse);
            Assert.AreEqual(GetResponse.Directions.East, postResponseDeserialized.Direction);
        }

        [Test]
        public void Test_090_FShouldMoveForwardWithoutTurns()
        {
            var command = "F";
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.NotNull(postResponse);
            Assert.AreEqual(2, postResponseDeserialized.Y);
            Assert.AreEqual(GetResponse.Directions.North, postResponseDeserialized.Direction);
        }

        [Test]
        public void Test_100_ShouldDieOnDangerCellWithoutScent()
        {
            //After first death cell is marked with scent and no longer suicidable
            if (string.Equals(GetMapCellStatus(1, 2).ToLowerInvariant(), "s")) {return;}

            var command = "FF";
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            var status = GetRobotStatus();

            //Assert
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(1, status.Y);
            Assert.AreEqual(GetResponse.Directions.North, postResponseDeserialized.Direction);
        }

        [Test]
        public void Test_101_ShouldDieOnDangerCellWithoutScent()
        {
            //1,2 is a scent spot after death on 1,3, marked with "s"
            var x = 1;
            var y = 2;
            //Prepare
            var getRequest = new RestRequest($"/api/values/{x}/{y}", Method.GET);
            //Act
            var getResponse = client.Execute(getRequest);
            var getResponseDeserialized = JsonConvert.DeserializeObject<string>(getResponse.Content);
            //Assert
            Assert.NotNull(getResponse);
            Assert.IsInstanceOf<string>(getResponseDeserialized);
            Assert.AreEqual(getResponseDeserialized, "s");

            //Cleanup
            var scentCleanupRequest = new RestRequest($"/api/values/{x}/{y}", Method.POST);
            client.Execute(getRequest);
        }

        [Test]
        public void Test_110_ShouldNotDieTwiceOnScentCell()
        {
            var command = "FF";
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(command);
            //Act
            client.Execute(postRequest);
            var status = GetRobotStatus();

            //Assert
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(2, status.Y);
            Assert.AreEqual(true, status.Scent);
            Assert.AreEqual(GetResponse.Directions.North, status.Direction);
        }

        [Test]
        [TestCase("aAa")]
        [TestCase("111")]
        [TestCase(null)]
        public void Test_120_Negative_TrashInWorksOnInvalidInput(string input)
        {
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(input);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.AreEqual(1, postResponseDeserialized.X);
            Assert.AreEqual(1, postResponseDeserialized.Y);
            Assert.AreEqual(false, postResponseDeserialized.Scent);
            Assert.AreEqual(GetResponse.Directions.North, postResponseDeserialized.Direction);
        }

        [Test]
        [TestCase("aFZ")]
        [TestCase("1F@")]
        public void Test_130_Negative_FiltersTrashInput(string input)
        {
            //Prepare
            var postRequest = new RestRequest("/api/values", Method.POST);
            postRequest.AddJsonBody(input);
            //Act
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            //Assert
            Assert.AreEqual(1, postResponseDeserialized.X);
            Assert.AreEqual(2, postResponseDeserialized.Y);
            Assert.AreEqual(GetResponse.Directions.North, postResponseDeserialized.Direction);
        }

        private string GetMapCellStatus(int x, int y)
        {
            var getRequest = new RestRequest($"/api/values/{x}/{y}", Method.GET);
            //Act
            var getResponse = client.Execute(getRequest);
            var getResponseDeserialized = JsonConvert.DeserializeObject<string>(getResponse.Content);
            return getResponseDeserialized;
        }

        /// <summary>
        /// Sends empty POST request to get current status from robot
        /// </summary>
        /// <returns>GetResponse</returns>
        private GetResponse GetRobotStatus()
        {
            var postRequest = new RestRequest("/api/values", Method.POST);
            var postResponse = client.Execute(postRequest);
            var postResponseDeserialized = JsonConvert.DeserializeObject<GetResponse>(postResponse.Content);
            return postResponseDeserialized;
        }
    }
}
