using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.builder
{
    public interface ICollectionBuilder
    {
        public void SetUserInputReader(IUserInput inInput);
        public void SetGameData(GameData inData);
        public void SetGameReferee(IGameReferee inReferee);
        public void Reset();
    }
}