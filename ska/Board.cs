using System;

namespace ska
{
    public class Board
    {
        public Piece[][] pieces;

        public Board()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    pieces[i][j] = null;
                }
            }
        }
    }
}

