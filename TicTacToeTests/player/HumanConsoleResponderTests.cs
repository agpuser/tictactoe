using TicTacToeProgram.player;
using Moq;
using TicTacToeProgram.board;
using TicTacToeProgram.ProgramIO;
using Xunit;

namespace TicTacToeTests.player
{
    public class HumanConsoleResponderTests
    {
        [Fact]
        public void GetResponse_ReturnsExpectedStringOfCoordinateValues()
        {
            string actual, expected = "1,1";
            var mockConsole = new Mock<IUserInput>();
            mockConsole.Setup(c => c.ReadInput()).Returns("1,1");
            IResponder sut = new HumanConsoleResponder(mockConsole.Object);

            actual = sut.GetResponse(new Mock<IBoard>().Object, 'X');
            
            Assert.Equal(expected, actual);
        }
    }
}