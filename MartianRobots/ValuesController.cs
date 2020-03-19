using SharedFiles;
using System.Web.Http;

namespace MartianRobots
{
    public class ValuesController : ApiController
    {
        // DELETE robot when program finished api/values
        public void Delete()
        {
            StaticClass.robotController.Death();
        }

        // GET map api/values/
        public string[,] Get()
        {
            return StaticClass.martianMap.GetMap();
        }

        // GET data on square x,y api/values/x/y
        public string Get(int idx, int idy)
        {
            return StaticClass.martianMap.GetMapPosition(idx, idy);
        }

        // POST command string to api/values
        public GetResponse Post([FromBody]string value)
        {
            if (value == null)
            {
                value = "";
            }

            var result = StaticClass.robotController.ProcessRobotInput(value, StaticClass.martianMap);
            return result;
        }
    }
}