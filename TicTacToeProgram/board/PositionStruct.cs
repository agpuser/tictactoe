using System;

namespace TicTacToeProgram.board
{
    public struct Position
    {
        public int Row;
        public int Col;

        public static Position InvalidPosition = new Position(-1, -1);
        
        public static Position Parse(string inString)
        {
            string[] tokens = inString.Split(',');
            int row = (Int32.Parse(tokens[0])) - 1;
            int col = (Int32.Parse(tokens[1])) - 1;
            Position result = new Position(row, col);
            return result;
        }
        
        public Position(int inRow, int inCol)
        {
            Row = inRow;
            Col = inCol;
        }

        public bool Equals(Position other) => Row == other.Row && Col == other.Col;
    }
}