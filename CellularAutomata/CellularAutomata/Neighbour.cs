namespace CellularAutomata
{
    public class Neighbour
    {
        public int Address { get; set; }
        public List<int> Neighbours { get; set; }
        public bool isAlive { get; set; }
    }
}
