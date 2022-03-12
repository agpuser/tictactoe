using Moq;
using TicTacToeProgram.board;

namespace TicTacToeTests.factory
{
    public class MoqBoardFactory
    {
        public Mock<IBoard> GetMoqBoardForDrawnEndGame()
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b.IsValidPosition(new Position(0, 0))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(0, 0))).Returns(true);
            result.Setup(b => b.AddMarker(new Position(0, 0), 'X'));
            result.Setup(b => b.AllSpacesPlayed()).Returns(true);
            result.Setup(b => b.ToString()).Returns("X O X \nX O O \nO X X ");
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForNoWinNoDrawnGame()
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b.IsValidPosition(new Position(1, 1))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(1, 1))).Returns(true);
            result.Setup(b => b.AddMarker(new Position(1, 1), 'X'));
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);
            result.Setup(b => b.ToString()).Returns(". . X \n. O . \n. . . ");
            result.Setup(b => b.Serialise()).Returns("..X.O....");
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForNonEmptySpace()
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b.IsValidPosition(new Position(0, 0))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(0, 0))).Returns(false);
            result.Setup(b => b.AddMarker(new Position(1, 1), 'X'));
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);
            result.Setup(b => b.ToString()).Returns("X O X \nX X O \n. O . ");
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForDrawnGameBasedOnFlag(bool inIsDrawGame)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b.IsValidPosition(new Position(0, 0))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(0, 0))).Returns(false);
            result.Setup(b => b.IsValidPosition(new Position(0, 0))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(0, 0))).Returns(true);
            result.Setup(b => b.AddMarker(new Position(0, 0), 'X'));
            result.Setup(b => b.AllSpacesPlayed()).Returns(inIsDrawGame);
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForPlayerWins(char inMarker)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b.IsValidPosition(new Position(0, 0))).Returns(true);
            result.Setup(b => b.IsPositionEmpty(new Position(0, 0))).Returns(true);
            result.Setup(b => b.AddMarker(new Position(0, 0), inMarker));
            result.Setup(b => b[0, 0]).Returns(inMarker);
            result.Setup(b => b[0, 1]).Returns(inMarker);
            result.Setup(b => b[0, 2]).Returns(inMarker);
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);
            result.Setup(b => b.ToString()).Returns("X O O \nX X X \n. O . ");
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForPlayerWinsHorizontalThreeInARow(char inMarker)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b[0, 0]).Returns(inMarker);
            result.Setup(b => b[0, 1]).Returns(inMarker);
            result.Setup(b => b[0, 2]).Returns(inMarker);
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);
            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForPlayerWinsVerticalThreeInARow(char inMarker)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b[0, 0]).Returns(inMarker);
            result.Setup(b => b[1, 0]).Returns(inMarker);
            result.Setup(b => b[2, 0]).Returns(inMarker);
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);

            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForPlayerWinsDiagonalTopLeftToBottomRightThreeInARow(char inMarker)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b[0, 0]).Returns(inMarker);
            result.Setup(b => b[1, 1]).Returns(inMarker);
            result.Setup(b => b[2, 2]).Returns(inMarker);
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);

            return result;
        }
        
        public Mock<IBoard> GetMoqBoardForPlayerWinsDiagonalBottomLeftToTopRightThreeInARow(char inMarker)
        {
            var result = new Mock<IBoard>();
            result.Setup(b => b.MaxRow).Returns(3);
            result.Setup(b => b.MaxCol).Returns(3);
            result.Setup(b => b[0, 2]).Returns(inMarker);
            result.Setup(b => b[1, 1]).Returns(inMarker);
            result.Setup(b => b[2, 0]).Returns(inMarker);
            result.Setup(b => b.AllSpacesPlayed()).Returns(false);
            return result;
        }
    }
}