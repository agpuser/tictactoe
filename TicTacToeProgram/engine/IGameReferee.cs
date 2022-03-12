using TicTacToeProgram.board;
using TicTacToeProgram.player;

namespace TicTacToeProgram.engine
{
    public interface IGameReferee
    {
        public GameStatus GetEndOfTurnStatus(Position inPosition, IBoard inBoard);
    }
}