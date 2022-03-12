using TicTacToeProgram.filehandler;
using Xunit;

namespace TicTacToeTests.filehandler
{
    public class GameFileHandlerTests
    {
        [Fact]
        public void Load_ConfirmLoadedDataMatchesExpected_ReturnsGameData()
        {
            bool actual, expected = true;
            string filename = "/../../../testfiles/testloadgame.txt";
            GameData expectedData = GameData.Default;
            IFileHandler<GameData> sut = new GameFileHandler(filename);

            GameData result = sut.Load();
            actual = result.Equals(expectedData);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Load_ConfirmLoadFailedBecauseFIleNotFound_ReturnsDefaultGameData()
        {
            bool actual, expected = true;
            string filename = "badfilename.txt";
            GameData expectedData = GameData.Default;
            IFileHandler<GameData> sut = new GameFileHandler(filename);

            GameData result = sut.Load();
            actual = result.Equals(expectedData);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("/../../../testfiles/testsavegame.txt", true)]
        [InlineData("/badsavefilename", false)]
        public void Save_ReturnsBooleanWhetherDataIsSuccessfullySavedToFileOrNot(
            string inFilename, bool expected)
        {
            IFileHandler<GameData> sut = new GameFileHandler(inFilename);
            GameData saveData = GameData.Default;

            bool actual = sut.Save(saveData);

            Assert.Equal(expected, actual);
        }
    }
}