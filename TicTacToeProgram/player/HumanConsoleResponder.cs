using TicTacToeProgram.board;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.player
{
    public class HumanConsoleResponder : IResponder
    {
        public static IResponder CreateStandard(IUserInput inInput) => new HumanConsoleResponder(inInput);

        private readonly IUserInput _inputReader;

        public HumanConsoleResponder(IUserInput inInputReader)
        {
            _inputReader = inInputReader;
        }
    
        public string GetResponse(IBoard inBoard, char inMarker)
        {
            return _inputReader.ReadInput();
        }
    }
}