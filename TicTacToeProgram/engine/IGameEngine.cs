namespace TicTacToeProgram.engine
{
    public interface IGameEngine
    {
        public void Run();
        public bool QuitProgramSignalled { get; }
    }
}