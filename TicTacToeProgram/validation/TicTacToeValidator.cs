using System;
using System.Linq;
using TicTacToeProgram.board;

namespace TicTacToeProgram.validation
{
    public class TicTacToeValidator : IValidator
    {
        public Position GetPositionFromString(string inString)
        {
            Position result;
            
            try
            {
                result = Position.Parse(inString);
            }
            catch (Exception)
            {
                result = Position.InvalidPosition;
            }
            return result;
        }

        public string GetValidationPositionMessage(Position inPosition, IBoard inBoard)
        {
            string result;
            
            if (inPosition.Equals(Position.InvalidPosition))
                result = "Invalid input entered. Enter a 'row,col' or 'q' to quit.";
            else
                result = GetValidationResultMessage(inPosition, inBoard);
            return result;
        }

        private string GetValidationResultMessage(Position inPosition, IBoard inBoard)
        {
            string result;
            
            if (!inBoard.IsValidPosition(inPosition))
                result = "The entered co-ordinates are invalid, Please re-enter.";
            else if (!inBoard.IsPositionEmpty(inPosition))
                result = "Oh no, a piece is already at this place! Try again...";
            else
                result = String.Empty;
            return result;
        }

        public bool IsValidValue(string inResponse, string[] inValidValues) =>
            inValidValues.Contains(inResponse);
    }
}