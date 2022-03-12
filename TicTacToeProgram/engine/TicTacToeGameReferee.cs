using TicTacToeProgram.board;
using TicTacToeProgram.player;

namespace TicTacToeProgram.engine
{
    public class TicTacToeGameReferee : IGameReferee
    {
        public GameStatus GetEndOfTurnStatus(Position inPosition, IBoard inBoard)
        {
            const char XMarker = 'X', OMarker = 'O';
            GameStatus result;

            if (IsPlayerVictory(inBoard, inPosition, XMarker))
                result = GameStatus.PlayerXWin;
            else if (IsPlayerVictory(inBoard, inPosition, OMarker))
                result = GameStatus.PlayerOWin;
            else if (IsDrawnGame(inBoard))
                result = GameStatus.DrawnGame;
            else
                result = GameStatus.MarkerPlaced;
            return result;
        }

        private bool IsPlayerVictory(IBoard inBoard, Position inPosition, char inMarker)
        {
            return IsHorizontalThreeInARow(inBoard, inPosition, inMarker) || 
                   IsVerticalThreeInARow(inBoard, inPosition, inMarker) ||
                   IsDiagonalThreeInARow(inBoard, inPosition, inMarker);
        }
        
        private bool IsHorizontalThreeInARow(IBoard inBoard, Position inPosition, char inMarker)
        {
            int anchor = inPosition.Col / inBoard.MaxCol + 1;
            return inBoard[inPosition.Row, anchor - 1] == inMarker &&
                   inBoard[inPosition.Row, anchor] == inMarker &&
                   inBoard[inPosition.Row, anchor + 1] == inMarker;
        }
        
        private bool IsVerticalThreeInARow(IBoard inBoard, Position inPosition, char inMarker)
        {
            int anchor = inPosition.Row / inBoard.MaxRow + 1;
            return inBoard[anchor - 1, inPosition.Col] == inMarker &&
                   inBoard[anchor, inPosition.Col] == inMarker &&
                   inBoard[anchor + 1, inPosition.Col] == inMarker;
        }
        
        private bool IsDiagonalThreeInARow(IBoard inBoard, Position inPosition, char inMarker)
        {
            bool found = false;
            bool topRightToBottomLeft = inPosition.Row + inPosition.Col == (inBoard.MaxCol  - 1);
            bool topLeftToBottomRight = inPosition.Row == inPosition.Col;
            bool both = inPosition.Row == inBoard.MaxRow / 2 && inPosition.Col == inBoard.MaxCol / 2;
            bool spaceInADiagonal = topLeftToBottomRight || topRightToBottomLeft;
            
            if (!spaceInADiagonal)
                found = false;
            {
                if (topLeftToBottomRight)
                    found = IsDiagonalThreeInARowTopLeftToBottomRight(inBoard, inMarker);
                if (!found && (topRightToBottomLeft || both))
                    found = IsDiagonalThreeInARowTopRightToBottomLeft(inBoard, inMarker);
            }
            return found;
        }

        private bool IsDiagonalThreeInARowTopLeftToBottomRight(IBoard inBoard, char inMarker)
        {
            int i = 0;
            return inBoard[i, i++] == inMarker && inBoard[i, i++] == inMarker && inBoard[i, i++] == inMarker;
        }
        
        private bool IsDiagonalThreeInARowTopRightToBottomLeft(IBoard inBoard, char inMarker)
        {
            int i = 0, j = inBoard.MaxCol;
            return inBoard[i++, --j] == inMarker && inBoard[i++, --j] == inMarker && inBoard[i++, --j] == inMarker;
        }

        private bool IsDrawnGame(IBoard inBoard) => inBoard.AllSpacesPlayed();
    }
}