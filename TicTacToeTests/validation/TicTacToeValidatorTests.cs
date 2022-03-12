using Moq;
using TicTacToeProgram.board;
using TicTacToeProgram.validation;
using Xunit;

namespace TicTacToeTests.validation
{
    public class TicTacToeValidatorTests
    {
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(2, 1, false)]
        public void Equals_ReturnsTrueOrFalseWhenPositionValuesMatchOrDoNot(
            int inRow, int inCol, bool expected)
        {
            Position left = new(1, 1);
            
            bool actual = left.Equals(new Position(inRow, inCol));
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("2,2", true)]
        [InlineData("2,", false)]
        public void GetPositionFromString_ReturnsValidPosition(
            string input, bool expected)
        {
            Position actualPos, expectedPos = new Position(1, 1);
            IValidator sut = new TicTacToeValidator();

            actualPos = sut.GetPositionFromString(input);
            bool actual = actualPos.Equals(expectedPos);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, -1, false, false, "Invalid input entered. Enter a 'row,col' or 'q' to quit.")]
        [InlineData(1, 1, false, false, "The entered co-ordinates are invalid, Please re-enter.")]
        [InlineData(1, 1, true, false, "Oh no, a piece is already at this place! Try again...")]
        [InlineData(1, 1, true, true, "")]
        public void GetValidationPositionMessage_ReturnsAppropriateInvalidMessageBased(
            int inRow, int inCol, bool validPos, bool emptyPos, string expected)
        {
            Position pos = new Position(inRow, inCol);
            var stubBoard = new Mock<IBoard>();
            stubBoard.Setup(b => b.IsValidPosition(pos)).Returns(validPos);
            stubBoard.Setup(b => b.IsPositionEmpty(pos)).Returns(emptyPos);
            IValidator sut = new TicTacToeValidator();
            
            string actual = sut.GetValidationPositionMessage(pos, stubBoard.Object);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("y", true)]
        [InlineData("n", true)]
        [InlineData("   ", false)]
        public void IsValidReplayResponse_ReturnTrueIfYOrNpassedFalseOtherwise(
            string inResponse, bool expected)
        {
            IValidator sut = new TicTacToeValidator();

            bool actual = sut.IsValidValue(inResponse, new string[] { "y", "n" });
            
            Assert.Equal(expected, actual);
        }
    }
}