using TicTacToeProgram.board;

namespace TicTacToeProgram.player
{
    public interface IResponder
    {
        public string GetResponse(IBoard inBoard, char inMarker);
    }
}