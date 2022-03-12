using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.runner
{
    public class ConsoleRunner : IRunner
    {
        private IGameEngine _engine;

        public ConsoleRunner()
        { 
           _engine = new TicTacToeGameEngine(
                new TicTacToeBoard(), 
                new ConsoleUserInput(), 
                new ConsoleDisplayOutput(), 
                new TicTacToeGameReferee(),
                new GameFileHandler("/savefile/savedgame.txt"));
        }
        
        public void Run()
        {
            _engine.Run();
        }
    }
}