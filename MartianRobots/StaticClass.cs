using MartianRobots;

namespace SharedFiles
{
    /// <summary>
    /// This is a hotfix, we have no external DB, and no external execution modules.
    /// But still need to be able to access RobotController and MartianMap from ValuesController
    /// And this class provides new instances for both.
    /// </summary>
    public static class StaticClass
    {
        public static RobotController robotController = new RobotController();
        public static MartianMap martianMap = new MartianMap();
    }
}