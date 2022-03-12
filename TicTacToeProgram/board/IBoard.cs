namespace TicTacToeProgram.board
{
    public interface IBoard
    {
        public int MaxRow { get; }
        public int MaxCol { get; }
        public char this[int inRow, int inCol] { get; }
        public void Init(string inSpaces);
        public bool AllSpacesPlayed();
        public bool IsPositionEmpty(Position inPosition);
        public bool IsValidPosition(Position inPosition);
        public void AddMarker(Position inPosition, char inMarker);
        public void Reset();
        public bool IsEmpty();
        public void Remove(Position inPosition);
        public string Serialise();
    }
}