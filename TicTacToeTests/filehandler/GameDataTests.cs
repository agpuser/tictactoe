using Moq;
using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using Xunit;

namespace TicTacToeTests.filehandler
{
    public class GameDataTests
    {
        [Fact]
        public void CreateFromBoardAndPlayer_ConfirmThreeArgConstructorWorksCorrectly()
        {
            bool actual, expected = true;
            GameData expectedData = GameData.CreateNew(".X..O....", 
                'X', 'O', 1, GameModeType.Default);
            var stubPlayer = new Mock<IPlayer>();
            stubPlayer.Setup(p => p.Marker).Returns('X');
            stubPlayer.Setup(p => p.PlayerNumber).Returns(1);
            var stubBoard = new Mock<IBoard>();
            stubBoard.Setup(b => b.Serialise()).Returns(".X..O....");
            
            GameData sut = GameData.CreateUsingBoardPlayerAndMode(stubBoard.Object, stubPlayer.Object, GameModeType.Default);
            actual = sut.Equals(expectedData);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, ".........", "X", "O", "1", "0")]
        [InlineData(false, "...XX..O.", "X", "O", "1", "0")]
        public void Equals_ConfirmTwoGameDataObjectMatchOrNot(
            bool expected, string inBoard, string inPlayerOne, string inPlayerTwo, string inNext, string inMode)
        {
            GameData first = GameData.CreateWithStrings(inBoard, inPlayerOne, inPlayerTwo, inNext, inMode);
            GameData second = GameData.CreateWithStrings(".........", "X", "O", "1", "0");

            bool actual = first.Equals(second);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Serialise_ConfirmSerialisedGameDataObject()
        {
            string actual, expected = ".X...O...\nX\nO\n1\nHumanVsHuman";
            GameData sut = GameData.CreateWithStrings(".X...O...", "X", "O", "1", "0");

            actual = sut.Serialise();
            
            Assert.Equal(expected, actual);
        }
    }
}