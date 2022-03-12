using TicTacToeProgram.board;

namespace TicTacToeProgram.validation
{
    public interface IValidator
    {
        public Position GetPositionFromString(string inString);
        public string GetValidationPositionMessage(Position inPosition, IBoard inBoard);
        public bool IsValidValue(string inResponse, string[] inValidValues);
    }
}