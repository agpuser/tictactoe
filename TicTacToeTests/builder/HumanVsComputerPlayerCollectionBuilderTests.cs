using Moq;
using TicTacToeProgram.builder;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using Xunit;

namespace TicTacToeTests.builder
{
    public class HumanVsComputerPlayerCollectionBuilderTests
    {
        [Fact]
        public void GetProduct_ConfirmCreatedHumanVsComputerPlayerCollection()
        {
            bool actual, expected = true;
            GameData gameState = GameData.Default;
            var stubInput = new Mock<IUserInput>();
            var stubGameReferee = new Mock<IGameReferee>();
            PlayerCollection actualCollection, expectedCollection = 
                PlayerCollection.CreateHumanVsComputerCollection(stubInput.Object, gameState, stubGameReferee.Object);
            HumanVsComputerPlayerCollectionBuilder sut = new HumanVsComputerPlayerCollectionBuilder();
            sut.SetGameData(gameState);
            sut.SetUserInputReader(stubInput.Object);
            sut.SetGameReferee(stubGameReferee.Object);

            actualCollection = sut.GetProduct();
            actual = expectedCollection.Equals(actualCollection);

            Assert.Equal(expected, actual);
        }
    }
}