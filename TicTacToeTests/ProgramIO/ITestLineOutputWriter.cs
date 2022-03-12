using TicTacToeProgram.ProgramIO;

namespace TicTacToeTests.ProgramIO
{
    public interface ITestOutputWriter : IDisplayOutput
    {
        public string InvalidInputMessage { get; }
        public string GetDisplay();
        public string GetDisplayLineNUmber(int inLineNumber);
        public string GetLastDisplayLine();
        public void DisplayDummyText();
    }
}