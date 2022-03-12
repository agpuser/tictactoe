using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeTests.board;
using TicTacToeTests.factory;
using Xunit;

namespace TicTacToeTests.engine
{
    public class TicTacToeGameRefereeTests
    {
        [Theory]
        [InlineData('X', GameStatus.PlayerXWin)]
        [InlineData('O', GameStatus.PlayerOWin)]
        public void GetEndOfTurnStatus_ReturnsPlayerWinsHorizontalThreeInARow(
            char inMarker, GameStatus expected)
        {
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForPlayerWins(inMarker);
            IGameReferee sut = new TicTacToeGameReferee();    
            
            GameStatus actual = sut.GetEndOfTurnStatus(new Position(0,0), mockBoard.Object);

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(true, GameStatus.DrawnGame)]
        [InlineData(false, GameStatus.MarkerPlaced)]
        public void IsDrawnGame_ReturnsTrueOrFalseWhenDrawnGameOrNot(
            bool isDrawnGame, GameStatus expected)
        {
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForDrawnGameBasedOnFlag(isDrawnGame);
            IGameReferee sut = new TicTacToeGameReferee();
                
            GameStatus actual = sut.GetEndOfTurnStatus(new Position(0,0), mockBoard.Object);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetEndOfTurnStatus_ReturnsPlayerXWinsWhenThreeHorizontalInARow()
        {
            GameStatus actual, expected = GameStatus.PlayerXWin;
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForPlayerWinsHorizontalThreeInARow('X');
            IGameReferee sut = new TicTacToeGameReferee();
                
            actual = sut.GetEndOfTurnStatus(new Position(0,0), mockBoard.Object);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetEndOfTurnStatus_ReturnsPlayerXWinsWhenThreeVerticalInARow()
        {
            GameStatus actual, expected = GameStatus.PlayerXWin;
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForPlayerWinsVerticalThreeInARow('X');
            IGameReferee sut = new TicTacToeGameReferee();
                
            actual = sut.GetEndOfTurnStatus(new Position(0,0), mockBoard.Object);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetEndOfTurnStatus_ReturnsPlayerXWinsWhenThreeDiagonalTopLeftToBottomRightInARow()
        {
            GameStatus actual, expected = GameStatus.PlayerXWin;
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForPlayerWinsDiagonalTopLeftToBottomRightThreeInARow('X');
            IGameReferee sut = new TicTacToeGameReferee();
                
            actual = sut.GetEndOfTurnStatus(new Position(0,0), mockBoard.Object);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetEndOfTurnStatus_ReturnsPlayerXWinsWhenThreeDiagonalBottomLeftToTopRightInARow()
        {
            GameStatus actual, expected = GameStatus.PlayerXWin;
            MoqBoardFactory factory = new();
            var mockBoard = factory.GetMoqBoardForPlayerWinsDiagonalBottomLeftToTopRightThreeInARow('X');
            IGameReferee sut = new TicTacToeGameReferee();
                
            actual = sut.GetEndOfTurnStatus(new Position(1,1), mockBoard.Object);

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(0, 1, "XXXO.O.O.", GameStatus.PlayerXWin)]
        [InlineData(1, 1, "XX.OOO.O.", GameStatus.PlayerOWin)]
        [InlineData(0, 0, "OOXXXOOXO", GameStatus.DrawnGame)]
        [InlineData(2, 2, "XX.O.O.OX", GameStatus.MarkerPlaced)]
        public void GetEndOfTurnStatus_ReturnsPlayerWins(
            int inRow, int inCol, string inSpaces, GameStatus expected)
        {
            IBoard mockBoard = new MockTicTacToeBoard(inSpaces);
            IGameReferee sut = new TicTacToeGameReferee();
                
            GameStatus actual = sut.GetEndOfTurnStatus(new Position(inRow,inCol), mockBoard);

            Assert.Equal(expected, actual);
        }
    }
}