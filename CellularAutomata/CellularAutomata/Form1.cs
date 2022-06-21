namespace CellularAutomata
{
    public partial class Form1 : Form
    {
        bool isRunning;
        System.Timers.Timer timer;
        Board board;
        Conway conway;
        object loc;

        public Form1()
        {
            InitializeComponent();
            loc = new object(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                timer.Enabled = false;
                timer.Stop();
                isRunning = false;
                button1.Text = "Start";
            }
            else
            {
                button1.Text = "Stop";
                board = new Board(256);
                conway = new Conway();

                timer = new System.Timers.Timer();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Ticked);
                timer.Interval = 35;
                timer.Enabled = true;
                timer.Start();
                isRunning = true;
            }
        }

        private void Ticked(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (loc)
            {
                //Update
                List<Neighbour> neighbours = board.CountNeighbours();
                neighbours = conway.UpdateGeneration(neighbours);
                board.NeighboursToBoardState(neighbours);


                //Push to screen
                pictureBox1.Invalidate();
                pictureBox1.BackgroundImage = board.GenerateImage(loc);
            }
        }
    }
}