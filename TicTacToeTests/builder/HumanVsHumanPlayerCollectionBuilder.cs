using Moq;
using TicTacToeProgram.builder;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using Xunit;

namespace TicTacToeTests.builder
{
    public class HumanVsHumanPlayerCollectionBuilderTests
    {
        [Fact]
        public void GetProduct_ConfirmCreatedHumanVsHumanPlayerCollection()
        {
            bool actual, expected = true;
            var stubInput = new Mock<IUserInput>();
            GameData gameState = GameData.Default;
            PlayerCollection actualCollection,
                expectedCollection = PlayerCollection.CreateHumanVsHumanCollection(stubInput.Object, gameState);
            HumanVsHumanPlayerCollectionBuilder sut = new HumanVsHumanPlayerCollectionBuilder();
            sut.SetGameData(gameState);
            sut.SetUserInputReader(stubInput.Object);

            actualCollection = sut.GetProduct();
            actual = expectedCollection.Equals(actualCollection);

            Assert.Equal(expected, actual);
        }
    }
}