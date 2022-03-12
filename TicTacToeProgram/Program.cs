using TicTacToeProgram.runner;

namespace TicTacToeProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            IRunner runner = new ConsoleRunner();
            runner.Run();
        }
    }
}