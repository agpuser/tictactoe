using Moq;
using TicTacToeProgram.builder;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using Xunit;

namespace TicTacToeTests.builder
{
    public class PlayersDirectorTests
    {
        [Fact]
        public void ConstructCollection_ConfirmHumanVsHumanCollection()
        {
            bool actual, expected = true;
            GameModeType expectedGameMode = GameModeType.HumanVsHuman;
            GameData gameState = GameData.Default;
            var stubInput = new Mock<IUserInput>();
            HumanVsHumanPlayerCollectionBuilder builder = new HumanVsHumanPlayerCollectionBuilder();
            PlayersDirector sut = new PlayersDirector(builder);
            
            sut.ConstructHumanVsHumanCollection(gameState, stubInput.Object);
            PlayerCollection result = builder.GetProduct();
            actual = result.GameMode.Equals(expectedGameMode);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ConstructCollection_ConfirmHumanVsComputerCollection()
        {
            bool actual, expected = true;
            GameModeType expectedGameMode = GameModeType.HumanVsComputer;
            GameData gameState = GameData.Default;
            gameState.GameMode = GameModeType.HumanVsComputer;
            var dummyGameReferee = new Mock<IGameReferee>();
            var stubInput = new Mock<IUserInput>();
            HumanVsComputerPlayerCollectionBuilder builder = new HumanVsComputerPlayerCollectionBuilder();
            PlayersDirector sut = new PlayersDirector(builder);
            
            sut.ConstructHumanVsComputerCollection(gameState, stubInput.Object, dummyGameReferee.Object);
            PlayerCollection result = builder.GetProduct();
            actual = result.GameMode.Equals(expectedGameMode);
            
            Assert.Equal(expected, actual);
        }
    }
}