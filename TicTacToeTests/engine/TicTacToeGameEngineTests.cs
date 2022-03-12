using TicTacToeProgram.ProgramIO;
using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using Xunit;
using Moq;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeTests.factory;
using TicTacToeTests.ProgramIO;

namespace TicTacToeTests.engine
{
    public class TicTacToeGameEngineTests
    {
        [Fact]
        public void Run_ConfirmInitialGameMessageWrittenToOutputDevice()
        {
            string actual, expected = "Welcome to Tic Tac Toe!";
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForDrawnEndGame();
            ITestOutputWriter fakeOutput = new FakeDisplayOutputDevice();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, fakeOutput, 
                new Mock<IGameReferee>().Object, new Mock<IFileHandler<GameData>>().Object);
            
            sut.Run();
            actual = fakeOutput.GetDisplayLineNUmber(1);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Run_ConfirmGracefulExitWhenInvalidReplayResponseInitiallyEntered()
        {
            string actual, expected = "Do you wish to play again ('y' or 'n'): ";
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("q")
                .Returns(" ")
                .Returns("n");
            var dummyBoard = new Mock<IBoard>();
            ITestOutputWriter fakeOutput = new FakeDisplayOutputDevice();
            IGameEngine sut = new TicTacToeGameEngine(
                dummyBoard.Object, stubInput.Object, fakeOutput, 
                new Mock<IGameReferee>().Object, new Mock<IFileHandler<GameData>>().Object);
            
            sut.Run();
            actual = fakeOutput.GetLastDisplayLine();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Run_ConfirmOnBoardValidMoveAccepted()
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("2,2")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(
                It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.MarkerPlaced);
            var dummyDisplay = new Mock<IDisplayOutput>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, new Mock<IFileHandler<GameData>>().Object);

            sut.Run();
            
            dummyDisplay.Verify(d => d.DisplayMoveAcceptedMessage(), Times.Once());
        }
        
        [Fact]
        public void Run_ConfirmInvalidInputEnteredByPlayer()
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns(" ")
                .Returns("2,2")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(
                It.IsAny<Position>(), It.IsAny<IBoard>()))
                .Returns(GameStatus.MarkerPlaced);
            var dummyDisplay = new Mock<IDisplayOutput>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, new Mock<IFileHandler<GameData>>().Object);

            sut.Run();
            
            dummyDisplay.Verify(d => d.DisplayInvalidPositionMessage(It.IsAny<string>()), Times.Once());
        }
        
        [Theory]
        [InlineData('X', GameStatus.PlayerXWin)]
        [InlineData('O', GameStatus.PlayerOWin)]
        public void Run_ConfirmPlayerWinResultObtained(
            char inMarker, GameStatus inStatus)
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("1,1")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForPlayerWins(inMarker);
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(inStatus);
            var dummyDisplay = new Mock<IDisplayOutput>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, new Mock<IFileHandler<GameData>>().Object);

            sut.Run();
            
            dummyDisplay.Verify(d => d.DisplayPlayerVictory(mockBoard.Object, inMarker), Times.Once());
        }
        
        [Fact]
        public void Run_ConfirmDrawnGameStatusObtained()
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("1,1")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForDrawnEndGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            var dummyDisplay = new Mock<IDisplayOutput>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, new Mock<IFileHandler<GameData>>().Object);

            sut.Run();
            
            dummyDisplay.Verify(d => d.DisplayDrawnGame(mockBoard.Object), Times.Once());
        }
        
        [Theory] 
        [InlineData("Invalid input entered. Enter a 'row,col' or 'q' to quit.", " ")]
        [InlineData("The entered co-ordinates are invalid, Please re-enter.", "4,4")]
        [InlineData("Oh no, a piece is already at this place! Try again...", "1,1")]
        public void Run_ConfirmInvalidMessageObtained(string expected, string input)
        {
            
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns(input)
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNonEmptySpace();
            ITestOutputWriter fakeDisplay = new FakeDisplayOutputDevice();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                fakeDisplay, stubGameReferee.Object, new Mock<IFileHandler<GameData>>().Object);

            sut.Run();
            string actual = fakeDisplay.InvalidInputMessage;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Run_ConfirmGameLoadedByCheckingBoardState()
        {
            string actual, expected = "..X.O....";
            GameData testData = GameData.CreateWithStrings("..X.O....","X", 
                "O", "2", "0");
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("3")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            var dummyDisplay = new Mock<IDisplayOutput>();
            var stubFileHandler = new Mock<IFileHandler<GameData>>();
            stubFileHandler.Setup(f => f.Load()).Returns(testData);
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, stubFileHandler.Object);

            sut.Run();
            actual = mockBoard.Object.Serialise();

            Assert.Equal(expected, actual);
            dummyDisplay.Verify(d => d.DisplayPlayScreen(It.IsAny<IBoard>(),It.IsAny<IPlayer>()), Times.Once());
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Run_ConfirmGameSavedByCheckingCallToDisplaySaveMessage(
            bool inOkay)
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("1")
                .Returns("X")
                .Returns("1,1")
                .Returns("s")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => 
                r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            var dummyDisplay = new Mock<IDisplayOutput>();
            var stubFileHandler = new Mock<IFileHandler<GameData>>();
            stubFileHandler.Setup(f => 
                f.Save(It.IsAny<GameData>())).Returns(inOkay);
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, stubFileHandler.Object);

            sut.Run();
            
            dummyDisplay.Verify(d => d.DisplaySaveMessage(inOkay), Times.Once());
        }
        
        [Fact]
        public void Run_ConfirmHumanVsComputerPlayerModeSelected()
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("2")
                .Returns("X")
                .Returns("q")
                .Returns("n");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            var dummyDisplay = new Mock<IDisplayOutput>();
            var stubFileHandler = new Mock<IFileHandler<GameData>>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, stubFileHandler.Object);

            sut.Run();
        }
        
        [Fact]
        public void Run_ConfirmGameIsResetAfterAPlaythrough()
        {
            var stubInput = new Mock<IUserInput>();
            stubInput.SetupSequence(i => i.ReadInput())
                .Returns("2")
                .Returns("X")
                .Returns("q")
                .Returns("y")
                .Returns("4");
            MoqBoardFactory factory = new();
            Mock<IBoard> mockBoard = factory.GetMoqBoardForNoWinNoDrawnGame();
            var stubGameReferee = new Mock<IGameReferee>();
            stubGameReferee.Setup(r => r.GetEndOfTurnStatus(It.IsAny<Position>(), It.IsAny<IBoard>())).Returns(GameStatus.DrawnGame);
            var dummyDisplay = new Mock<IDisplayOutput>();
            var stubFileHandler = new Mock<IFileHandler<GameData>>();
            IGameEngine sut = new TicTacToeGameEngine(mockBoard.Object, stubInput.Object, 
                dummyDisplay.Object, stubGameReferee.Object, stubFileHandler.Object);

            sut.Run();
        }
    }
}