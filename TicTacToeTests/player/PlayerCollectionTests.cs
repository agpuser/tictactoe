using Moq;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using Xunit;

namespace TicTacToeTests.player
{
    public class PlayerCollectionTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Next_ConfirmReturnsExpectedPlayer(
            int inNextPlayerNumber)
        {
            int actual, expected = inNextPlayerNumber == 1 ? 2 : 1;
            var stubPlayerX = new Mock<IPlayer>();
            stubPlayerX.Setup(p => p.PlayerNumber).Returns(1);
            var stubPlayerO = new Mock<IPlayer>();
            stubPlayerO.Setup(p => p.PlayerNumber).Returns(2);
            PlayerCollection sut = new PlayerCollection(
                stubPlayerX.Object, stubPlayerO.Object, inNextPlayerNumber);
            
            IPlayer nextPlayer = sut.Next();
            nextPlayer = sut.Next();
            actual = nextPlayer.PlayerNumber;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Equals_ChecksEqualityOfTwoCollections_ReturnTrue()
        {
            bool actual, expected = true;
            GameData gameState = GameData.Default;
            var stubInput = new Mock<IUserInput>();
            var stubGameReferee = new Mock<IGameReferee>();
            PlayerCollection right = PlayerCollection.CreateHumanVsComputerCollection(
                    stubInput.Object, gameState, stubGameReferee.Object);
            PlayerCollection left = PlayerCollection.CreateHumanVsComputerCollection(
                stubInput.Object, gameState, stubGameReferee.Object);
            
            actual = left.Equals(right);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Equals_ChecksEqualityOfTwoCollections_ReturnFalse()
        {
            bool actual, expected = false;
            GameData leftGameState = GameData.CreateNew("", 'X', 'O', 1, GameModeType.HumanVsHuman),
                rightGameState = GameData.CreateNew("", 'X', 'O', 1, GameModeType.HumanVsComputer);
            var stubInput = new Mock<IUserInput>();
            var stubGameReferee = new Mock<IGameReferee>();
            PlayerCollection left = PlayerCollection.CreateHumanVsHumanCollection(
                stubInput.Object, leftGameState);
            PlayerCollection right = PlayerCollection.CreateHumanVsComputerCollection(
                stubInput.Object, rightGameState, stubGameReferee.Object);

            actual = left.Equals(right);
            
            Assert.Equal(expected, actual);
        }
    }
}