using System;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using Moq;
using TicTacToeProgram.board;
using Xunit;

namespace TicTacToeTests.player
{
    public class GamePlayerTests
    {
        [Fact]
        public void GetPlayerResponse_ReturnsStringOfCoordinatesValuesFromHumanPlayer()
        {
            string actual, expected = "1,1";
            var mockConsole = new Mock<IUserInput>();
            mockConsole.Setup(c => c.ReadInput()).Returns("1,1");
            IPlayer sut = new GamePlayer(new HumanConsoleResponder(mockConsole.Object),
                'X', 1);

            actual = sut.GetResponse(new Mock<IBoard>().Object);
            
            Assert.Equal(expected, actual);
        }
    }
}