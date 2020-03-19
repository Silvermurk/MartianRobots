using SharedFiles;
using System;
using System.Windows.Forms;

namespace MartianRobotsClient
{
    public partial class RobotsUI : Form
    {
        private RestApiClient client;
        private bool connected = false;
        private GetResponse.Directions currentDirection;
        private int currentX;
        private int currentY;
        private string[,] mapArray;
        public RobotsUI()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void AddToLogs(string value)
        {
            LogBox.Items.Add(value);
            var visibleItems = LogBox.ClientSize.Height / LogBox.ItemHeight;
            LogBox.TopIndex = Math.Max(LogBox.Items.Count - visibleItems + 1, 0);
        }

        private void ChangeDirection(GetResponse.Directions desiredDirection)
        {
            while (currentDirection != desiredDirection)
            {
                GetPostResponse("L");
            }
        }

        private void CommandBox_TextChanged(object sender, EventArgs e)
        {
            CommandBox.Text = CommandBox.Text.ToUpperInvariant();
            //Better to use Regex
            foreach (var c in CommandBox.Text)
            {
                if (c != nameof(GetResponse.Turns.F)[0] &&
                    c != nameof(GetResponse.Turns.R)[0] &&
                    c != nameof(GetResponse.Turns.L)[0])
                {
                    CommandBox.Text = CommandBox.Text.Substring(0, CommandBox.Text.Length - 1);
                    AddToLogs("Only allowed by robot manual chars are accepted in commands");
                }
            }
            CommandBox.SelectionStart = CommandBox.Text.Length;
            CommandBox.SelectionLength = 0;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                var clientConnected = client.CreateRestClient();
                {
                    AddToLogs(clientConnected ? "Connected to Robot" : "Failed to connect to Robot");
                    UpdateMap();
                    AddToLogs("Map updated");
                    GetPostResponse();
                    AddToLogs("Robot position found");
                    CommandBox.Enabled = true;
                    AddToLogs("Command input enabled");
                    connected = true;
                }
            }
            else
            {
                AddToLogs("Already connected");
            }
        }

        private void DrawMap(int x = 1, int y = 1, string directionSymbol = "0")
        {
            UpdateMap();
            MapBox.Text = string.Empty;
            if (BlindModeCheckBox.Checked) { return; }
            mapArray[x, y] = directionSymbol;

            int rowLength = mapArray.GetLength(0);
            int colLength = mapArray.GetLength(1);

            //Turn matrix counterclockwise because [,] is facing right as North\Y++
            for (var col = mapArray.GetLength(1) - 1; col >= 0; col--)
            {
                for (var row = 0; row < mapArray.GetLength(0); row++)
                {
                    MapBox.Text += (string.Format("{0} ", mapArray[row, col])).ToUpperInvariant();
                }

                MapBox.Text += Environment.NewLine + Environment.NewLine;
            }
        }

        private void EastButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }

            ChangeDirection(GetResponse.Directions.East);
            GoForward();
        }

        private void ExecuteMoveButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }
            
            foreach (var c in CommandBox.Text)
            {
                if (!CheckNextCell()) continue;
                GetPostResponse(c.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new RestApiClient();
        }
        private string GetDirectionSymbol(GetResponse.Directions dir)
        {
            switch (dir)
            {
                case GetResponse.Directions.North:
                    return "^";

                case GetResponse.Directions.East:
                    return ">";

                case GetResponse.Directions.West:
                    return "<";

                case GetResponse.Directions.South:
                    return "∨";
            }
            throw new ArgumentException("Direction is not recognized");
        }

        private void GetMapPositionButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }

            if (NumericX.Value < 0 ||
                NumericX.Value > mapArray.GetLength(0) - 1 ||
                NumericY.Value < 0 ||
                NumericY.Value > mapArray.GetLength(1) - 1)
            {
                AddToLogs($"X value must be between 0 and {mapArray.GetLength(0)} ");
                AddToLogs($"Y value must be between 0 and {mapArray.GetLength(1)} ");
            }
            else
            {
                var result = client.GetMapRequest((int)NumericX.Value, (int)NumericY.Value);
                AddToLogs(result);
            }
        }
        private void GetPostResponse(string commands = "")
        {
            var response = client.PostRequest(commands);
            currentDirection = response.Direction;
            currentX = response.X;
            currentY = response.Y;
            var directionSymbol = GetDirectionSymbol(currentDirection);
            DrawMap(response.X, response.Y, directionSymbol);
            AddToLogs(response.ToString());

            if (client.GetMapRequest(currentX, currentY) == "W")
            {
                AddToLogs("You found a treasure! You win!");
            }
        }

        private void GoForward()
        {
            if (!CheckNextCell()) return;
            GetPostResponse("F");
        }

        private bool CheckNextCell()
        {
            switch (currentDirection)
            {
                case GetResponse.Directions.North:
                    if (currentY + 1 <= mapArray.GetLength(1))
                    {
                        return true;
                    }
                    break;
                case GetResponse.Directions.East:
                    if (currentX + 1 <= mapArray.GetLength(0))
                    {
                        return true;
                    }
                    break;
                case GetResponse.Directions.South:
                    if (currentY - 1 >= 0)
                    {
                        return true;
                    }
                    break;
                case GetResponse.Directions.West:
                    if (currentX - 1 >= 0)
                    {
                        return true;
                    }
                    break;
            }
            AddToLogs($"Movement to {currentDirection} aborted, map limit reached");
            return false;
        }

        private void NorthButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }

            ChangeDirection(GetResponse.Directions.North);
            GoForward();
        }

        private void SouthButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }

            ChangeDirection(GetResponse.Directions.South);
            GoForward();
        }

        private void UpdateMap()
        {
            mapArray = client.GetMapRequest();
        }
        private void WestButton_Click(object sender, EventArgs e)
        {
            if (!IsConnected()) { return; }

            ChangeDirection(GetResponse.Directions.West);
            GoForward();
        }

        private bool IsConnected()
        {
            if (connected) return true;
            AddToLogs("Connect first, than apply commands");
            return false;

        }
    }
}