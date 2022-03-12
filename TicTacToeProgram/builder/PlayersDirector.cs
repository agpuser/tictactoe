using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.builder
{
    public class PlayersDirector
    {
        private ICollectionBuilder _builder;

        public PlayersDirector(ICollectionBuilder inBuilder)
        {
            _builder = inBuilder;
        }

        public void ConstructHumanVsHumanCollection(GameData inData, IUserInput inInput)
        {
            _builder.Reset();
            _builder.SetGameData(inData);
            _builder.SetUserInputReader(inInput);
        }
        
        public void ConstructHumanVsComputerCollection(GameData inData, IUserInput inInput, IGameReferee inReferee)
        {
            _builder.Reset();
            _builder.SetGameData(inData);
            _builder.SetUserInputReader(inInput);
            _builder.SetGameReferee(inReferee);
        }
    }
}