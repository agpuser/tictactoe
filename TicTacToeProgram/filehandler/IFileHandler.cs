namespace TicTacToeProgram.filehandler
{
    public interface IFileHandler<T>
    {
        public T Load();
        public bool Save(T inData);
    }
}




