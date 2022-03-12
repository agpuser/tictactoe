namespace TicTacToeProgram.board
{
    public class TicTacToeBoard : IBoard
    {
        private const char UnplayedSpace = '.';
        
        private char[,] _board;

        public int MaxRow { get; }
        public int MaxCol { get; }
        public char this[int inRow, int inCol] => _board[inRow, inCol];

        public TicTacToeBoard()
        {
            MaxRow = 3;
            MaxCol = 3;
            Reset();
        }

        public TicTacToeBoard(string inSpaces) : this()
        {
            Init(inSpaces);
        }

        public void Init(string inSpaces)
        {
            for (int index = 0; index < inSpaces.Length; index++)
                _board[index / MaxRow, index % MaxCol] = inSpaces[index] == ' ' ? '.' : inSpaces[index];
        }

        public bool IsEmpty()
        {
            bool empty = true;
            
            for (int index = 0; index < MaxRow * MaxCol; index++)
            {
                if (_board[index / MaxRow, index % MaxCol] != '.')
                {
                    empty = false;
                    break;
                }
            }
            return empty;
        }

        public bool IsPositionEmpty(Position inPosition) =>
            _board[inPosition.Row, inPosition.Col] == UnplayedSpace;

        public void AddMarker(Position inPosition, char inMarker)
        {
            if (IsValidPosition(new Position(inPosition.Row, inPosition.Col)) && !AllSpacesPlayed())
                _board[inPosition.Row, inPosition.Col] = inMarker;
        }

        public bool IsValidPosition(Position inPosition)
        {
            return (inPosition.Row >= 0 && inPosition.Row <= MaxRow - 1) &&
                   (inPosition.Col >= 0 && inPosition.Col <= MaxCol - 1);
        }

        public void Reset()
        {
            _board = new char[MaxRow, MaxCol];
            for (int index = 0; index < MaxRow * MaxCol; index++)
                _board[index / MaxRow, index % MaxCol] = '.';
        }

        public bool AllSpacesPlayed()
        {
            bool emptySpaceFound = false;

            for (int index = 0; index < MaxRow * MaxCol; index++)
            {
                emptySpaceFound = BoardSpaceEmpty(index);
                if (emptySpaceFound)
                    break;
            }
            return !emptySpaceFound;
        }

        private bool BoardSpaceEmpty(int inIndex) =>
            _board[inIndex / MaxRow, inIndex % MaxCol] == UnplayedSpace;

        public void Remove(Position inPosition)
        {
            if ((inPosition.Row <= MaxRow && inPosition.Row >= 0) &&
                (inPosition.Col <= MaxCol && inPosition.Col >= 0))
                _board[inPosition.Row, inPosition.Col] = '.';
        }
        
        public string Serialise()
        {
            string result = "";

            for (int index = 0; index < MaxRow * MaxCol; index++)
                result += _board[index / MaxRow, index % MaxCol];
            return result;
        }
    }
}