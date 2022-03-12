using Moq;
using TicTacToeProgram.board;
using TicTacToeProgram.player;
using Xunit;

namespace TicTacToeTests.ProgramIO
{
    public class FakeDisplayOutputDeviceTests
    {
        [Fact]
        public void DisplayGameStartScreen_ConfirmOutput()
        {
            string actual, expected = "Welcome to Tic Tac Toe!";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayGameStartScreen();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayMainMenu_ConfirmOutput()
        {
            string actual, expected = "Main Menu";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayMainMenu();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayGameModePromptMessage_ConfirmOutput()
        {
            string actual, expected = "Please enter game mode:";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayGameModePromptMessage();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayGetMarkerPromptMessage_ConfirmOutput()
        {
            string actual, expected = "Player 1 please select your marker ('X' or 'O'): ";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayGetMarkerPromptMessage();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayInitialBoardMessage_ConfirmOutput()
        {
            string actual, expected = "Here's the current board:";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
           
            sut.DisplayInitialBoardMessage();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayPlayScreen_ConfirmOutput()
        {
            string actual, expected = "Board State";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayPlayScreen(new Mock<IBoard>().Object, new Mock<IPlayer>().Object);
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayMoveAcceptedMessage_ConfirmOutput()
        {
            string actual, expected = "Move accepted, here's the current board:";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayMoveAcceptedMessage();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayInvalidPositionMessage_ConfirmOutput()
        {
            string actual, expected = "Test Invalid Message";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayInvalidPositionMessage("Test Invalid Message");
            actual = sut.InvalidInputMessage;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayPlayerVictory_ConfirmOutput()
        {
            string actual, expected = "Player X wins!";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayPlayerVictory(new Mock<IBoard>().Object, 'X');
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayDrawnGame_ConfirmOutput()
        {
            string actual, expected = "Drawn game.";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayDrawnGame(new Mock<IBoard>().Object);
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplayReplayMessage_ConfirmOutput()
        {
            string actual, expected = "Do you wish to play again ('y' or 'n'): ";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplayReplayMessage();
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DisplaySaveMessage_ConfirmOutput()
        {
            string actual, expected = "Game saved.";
            ITestOutputWriter sut = new FakeDisplayOutputDevice();
            
            sut.DisplaySaveMessage(true);
            actual = sut.GetDisplay();

            Assert.Equal(expected, actual);
        }
    }
}