using System;

namespace TicTacToeProgram.ProgramIO
{
    public class ConsoleUserInput : IUserInput
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}