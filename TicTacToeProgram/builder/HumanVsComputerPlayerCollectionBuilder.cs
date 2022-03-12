using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.builder
{
    public class HumanVsComputerPlayerCollectionBuilder : ICollectionBuilder
    {
        private PlayerCollection _collection;
        private IUserInput _input;
        private GameData _data;
        private IGameReferee _referee;

        public void SetGameData(GameData inData)
        {
            _data = inData;
        }

        public void SetUserInputReader(IUserInput inInput)
        {
            _input = inInput;
        }

        public void SetGameReferee(IGameReferee inReferee)
        {
            _referee = inReferee;
        }

        public void Reset()
        {
            _collection = null;
            _data = null;
            _input = null;
            _referee = null;
        }

        public PlayerCollection GetProduct()
        {
            _collection = PlayerCollection.CreateHumanVsComputerCollection(_input, _data, _referee);
            return _collection;
        }
    }
}