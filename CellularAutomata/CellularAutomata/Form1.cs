namespace CellularAutomata
{
    public partial class Form1 : Form
    {
        bool isRunning;
        System.Timers.Timer timer;
        Board board;
        Conway conway;
        McCarron mccarron;
        object loc;

        public Form1()
        {
            InitializeComponent();
            loc = new object();
            comboBox1.SelectedItem = "Conway";
            comboBox1.Refresh();
            comboBox2.SelectedItem = "8";
            comboBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                timer.Enabled = false;
                timer.Stop();
                isRunning = false;
                button1.Text = "Start";
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
            }
            else
            {
                button1.Text = "Stop";
                int density = int.Parse(comboBox2.GetItemText(comboBox2.SelectedItem));
                board = new Board(256, density);
                string selected = comboBox1.GetItemText(comboBox1.SelectedItem);

                if (selected.Contains("Conway"))
                {
                    conway = new Conway();
                    mccarron = null;
                }
                else
                {
                    conway = null;
                    mccarron = new McCarron();
                }

                timer = new System.Timers.Timer();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Ticked);
                timer.Interval = 35;
                timer.Enabled = true;
                timer.Start();
                isRunning = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void Ticked(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (loc)
            {
                //Update
                List<Neighbour> neighbours = board.CountNeighbours();

                if (mccarron != null)
                    neighbours = mccarron.UpdateGeneration(neighbours);
                else
                    neighbours = conway.UpdateGeneration(neighbours);

                board.NeighboursToBoardState(neighbours);


                //Push to screen
                pictureBox1.Invalidate();
                pictureBox1.BackgroundImage = board.GenerateImage(loc);
            }
        }
    }
}