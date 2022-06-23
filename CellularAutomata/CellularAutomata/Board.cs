namespace CellularAutomata
{
    public class Board
    {
        private int size;

        public Board(int size, int density, int rngSeed)
        {
            BoardState = new bool[size * size];
            this.size = size;

            Random rng = new Random(rngSeed);

            for (int i = 0; i < BoardState.Length; i++)
                if (rng.Next(0, density) == 1 ? true : false)  //Adjust for density
                    BoardState[i] = rng.Next(0, 2) == 0 ? false : true;
        }

        public bool[] BoardState { get; set; }

        #region Generate Bitmap

        public Bitmap GenerateImage(object loc)
        {
            lock (loc)
            {
                Bitmap boardImage = new Bitmap(size, size);

                int index = 0;

                for (int i = 0; i < boardImage.Width; i++)
                {
                    for (int j = 0; j < boardImage.Height; j++)
                    {
                        if (BoardState[index++])
                            boardImage.SetPixel(i, j, Color.Black);
                        else
                            boardImage.SetPixel(i, j, Color.White);
                    }
                }

                return boardImage;
            }
        }

        #endregion

        #region Count Neighbours

        public List<Neighbour> CountNeighbours()
        {
            //Conway's Game of Life uses the 8-cell Moore neighborhood, which includes the diagonal neighbors.
            //Not all CAs use this; others use the 4-cell Von Neumann neighborhood, which excludes diagonal neighbors.

            //8-cell Moore neighborhood
            List<Neighbour> n = new List<Neighbour>();
            int index = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Neighbour nb = new Neighbour();
                    nb.Address = index;
                    nb.Neighbours = new List<int>();
                    nb.isAlive = BoardState[index];

                    if (i != 0)  //Check previous row
                    {
                        if (j != 0)  //Check far left
                        {
                            if (BoardState[index - size - 1])
                                nb.Neighbours.Add(index - size - 1);
                        }

                        if (BoardState[index - size])
                            nb.Neighbours.Add(index - size);

                        if (j != size - 1)  //Check far right
                        {
                            if (BoardState[index - size + 1])
                                nb.Neighbours.Add(index - size + 1);
                        }
                    }

                    if (j != 0)  //Check left
                    {
                        if (BoardState[index - 1])
                            nb.Neighbours.Add(index - 1);
                    }

                    if (j != size - 1)  //Check right
                    {
                        if (BoardState[index + 1])
                            nb.Neighbours.Add(index + 1);
                    }

                    if (i != size - 1)  //Check next row
                    {
                        if (j != 0)  //Check far left
                        {
                            if (BoardState[index + size - 1])
                                nb.Neighbours.Add(index + size - 1);
                        }

                        if (BoardState[index + size])
                            nb.Neighbours.Add(index + size);

                        if (j != size - 1)  //Check far right
                        {
                            if (BoardState[index + size + 1])
                                nb.Neighbours.Add(index + size + 1);
                        }
                    }

                    n.Add(nb);
                    index++;
                }
            }

            return n;
        }

        #endregion

        #region Helpers

        public void NeighboursToBoardState(List<Neighbour> neighbours)
        {
            for (int i = 0; i < neighbours.Count; i++)
                BoardState[neighbours[i].Address] = neighbours[i].isAlive;
        }

        #endregion
    }
}
