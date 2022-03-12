using TicTacToeProgram.board;
using Xunit;

namespace TicTacToeTests.board
{
    public class TicTacToeBoardTests
    {
        [Theory]
        [InlineData("XOXOXOXOX", true)]
        [InlineData("XOXOX    ", false)]
        public void AllSpacesPlayed_ReturnsTrueOrFalseWhenBoardSpacesHaveAllBeenPlayed(
            string inSpaces, bool expected)
        {
            IBoard sut = new TicTacToeBoard(inSpaces);

            bool actual = sut.AllSpacesPlayed();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("X        ")]
        [InlineData("XOXOXO   ")]
        [InlineData("XOXO     ")]
        [InlineData("XOXOXOXO ")]
        [InlineData("   OXOXOX")]
        [InlineData("XO OX  OX")]
        public void AllSpacesPlayed_ReturnsFalseWhenBoardSpacesHaveNotAllBeenPlayed(
            string inSpaces)
        {
            bool actual, expected = false;
            IBoard sut = new TicTacToeBoard(inSpaces);

            actual = sut.AllSpacesPlayed();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 2)]
        public void IsValidCoordinate_ReturnsTrueWhereCoordinateValuesAreValid(
            int inRow,
            int inCol)
        {
            bool actual, expected = true;
            IBoard sut = new TicTacToeBoard();

            actual = sut.IsValidPosition(new Position(inRow, inCol));

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Serialise_ConfirmSerialisedVersionOfBoard()
        {
            string actual, expected = "....XOX.O";
            IBoard sut = new TicTacToeBoard();
            sut.AddMarker(new Position(1, 1), 'X');
            sut.AddMarker(new Position(2, 2), 'O');
            sut.AddMarker(new Position(2, 0), 'X');
            sut.AddMarker(new Position(1, 2), 'O');

            actual = sut.Serialise();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("....X....", 1, 1, 'X')]
        [InlineData("X........", 0, 0, 'X')]
        [InlineData("........X", 2, 2, 'X')]
        public void AddMarker_PlaysMarkerOnBoardAtSpecifiedCoordinateValues(
            string expected, int inRow, int inCol, char inMarker)
        {
            IBoard sut = new TicTacToeBoard();

            sut.AddMarker(new Position(inRow, inCol), inMarker);
            string actual = sut.Serialise();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("X........", "X........", 1, -1, 'X')]
        [InlineData("X........", "X........", 1, 4, 'X')]
        [InlineData("X........", "X........", -1, 2, 'X')]
        [InlineData("X........", "X........", 4, 2, 'X')]
        [InlineData("XOXOXOXOX", "XOXOXOXOX", 0, 1, 'X')]
        public void AddMarker_NoMarkerPlayedAsInvalidCoordinatesOrBoardFull(
            string inSpaces, string expected, int inRow, int inCol, char inMarker)
        {
            IBoard sut = new TicTacToeBoard(inSpaces);

            sut.AddMarker(new Position(inRow, inCol), inMarker);
            string actual = sut.Serialise();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("         ", 0, 0, true)]
        [InlineData("X        ", 0, 0, false)]
        [InlineData("         ", 0, 2, true)]
        [InlineData("  X      ", 0, 2, false)]
        [InlineData("         ", 1, 1, true)]
        [InlineData("    X    ", 1, 1, false)]
        [InlineData("         ", 2, 2, true)]
        [InlineData("        X", 2, 2, false)]
        public void IsSpaceEmpty_ReturnsTrueOrFalseToConfirmSpaceEmptyOrNot(
            string inSpaces, int inRow, int inCol, bool expected)
        {
            IBoard sut = new TicTacToeBoard(inSpaces);

            bool actual = sut.IsPositionEmpty(new Position(inRow, inCol));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Reset_ConfirmBoardIsEmptyAfterCall()
        {
            string actual, expected = ".........";
            string inSpaces = "..O.XOXO.";
            IBoard sut = new TicTacToeBoard(inSpaces);

            sut.Reset();
            actual = sut.Serialise();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(".........", true)]
        [InlineData(".X...O...", false)]
        public void IsEmpty_ReturnsTrueOrFalseWhenBoardIsEmptyOrNot(
            string inSpaces, bool expected)
        {
            IBoard sut = new TicTacToeBoard(inSpaces);

            bool actual = sut.IsEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Init_ConfirmBoardInitialisedState()
        {
            string spaces = "X.OO..X..";
            string actual, expected = "X.OO..X..";
            IBoard sut = new TicTacToeBoard();

            sut.Init(spaces);
            actual = sut.Serialise();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Remove_ConfirmMarkerRemovedFromSelectedPosition()
        {
            string actual, expected = "XX.O.O...";
            IBoard sut = new TicTacToeBoard("XX.OOO...");

            sut.Remove(new Position(1, 1));
            actual = sut.Serialise();
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Remove_ConfirmMarkerNotRemovedWhenInvalidPositionGiven()
        {
            string actual, expected = "XX.O.O...";
            IBoard sut = new TicTacToeBoard("XX.O.O...");

            sut.Remove(new Position(4, 1));
            actual = sut.Serialise();
            
            Assert.Equal(expected, actual);
        }
    }
}