namespace CellularAutomata
{
    public class Conway
    {
        #region Conway's Game of Life Rules

        public List<Neighbour> UpdateGeneration(List<Neighbour> neighbours)
        {
            for (int i = 0; i < neighbours.Count; i++)
            {
                //Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                if (neighbours[i].isAlive && neighbours[i].Neighbours.Count < 2)
                {
                    neighbours[i].isAlive = false;
                    continue;
                }

                //Any live cell with two or three live neighbours lives on to the next generation.
                if (neighbours[i].isAlive && (neighbours[i].Neighbours.Count == 2 || neighbours[i].Neighbours.Count == 3))
                {
                    neighbours[i].isAlive = true;
                    continue;
                }

                //Any live cell with more than three live neighbours dies, as if by overpopulation.
                if (neighbours[i].isAlive && neighbours[i].Neighbours.Count > 3)
                {
                    neighbours[i].isAlive = false;
                    continue;
                }

                //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                if (!neighbours[i].isAlive && neighbours[i].Neighbours.Count == 3)
                {
                    neighbours[i].isAlive = true;
                    continue;
                }
            }

            return neighbours;
        }

        #endregion
    }
}
