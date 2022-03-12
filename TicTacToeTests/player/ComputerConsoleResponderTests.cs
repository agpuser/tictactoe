using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeProgram.player;
using TicTacToeTests.board;
using Xunit;

namespace TicTacToeTests.player
{
    public class ComputerConsoleResponderTests
    {
        [Theory]
        [InlineData("X.OO.O..X", 'X', "2,2")]
        [InlineData("XXOOXOOO.", 'X', "3,3")]
        [InlineData(".XOOXXOO.", 'O', "1,1")]
        [InlineData("XOX.O.O.X", 'X', "2,3")]
        [InlineData("OXO.X.X.O", 'O', "2,3")]
        [InlineData("..X.O.O..", 'X', "1,1")]
        [InlineData("OX..O....", 'X', "3,3")]
        public void GetResponse_ConfirmResponseIsExpectedMoveMadeByComputer(
            string inSpaces, char inMarker, string expected)
        {
            IBoard mockBoard = new MockTicTacToeBoard(inSpaces);
            IResponder sut = new ComputerConsoleResponder(new TicTacToeGameReferee());

            string actual = sut.GetResponse(mockBoard, inMarker);
            
            Assert.Equal(expected, actual);
        }
    }
}