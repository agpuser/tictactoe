using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.builder
{
    public class HumanVsHumanPlayerCollectionBuilder : ICollectionBuilder
    {
        private PlayerCollection _collection;
        private IUserInput _input;
        private GameData _data;

        public void SetGameData(GameData inData)
        {
            _data = inData;
        }

        public void SetUserInputReader(IUserInput inInput)
        {
            _input = inInput;
        }

        public void SetGameReferee(IGameReferee inReferee) { }
        
        public void Reset()
        {
            _collection = null;
            _data = null;
            _input = null;
        }

        public PlayerCollection GetProduct()
        {
            _collection = PlayerCollection.CreateHumanVsHumanCollection(_input, _data);
            return _collection;
        }
    }
}