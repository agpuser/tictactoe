using TicTacToeProgram.board;
using TicTacToeProgram.player;

namespace TicTacToeProgram.ProgramIO
{
    public interface IDisplayOutput
    {
        public void DisplayGameStartScreen();
        public void DisplayMainMenu();
        public void DisplayGameModePromptMessage();
        public void DisplayGetMarkerPromptMessage();
        public void DisplayInitialBoardMessage();
        public void DisplayPlayScreen(IBoard inBoard, IPlayer inPlayer);
        public void DisplayMoveAcceptedMessage();
        public void DisplayInvalidPositionMessage(string inMsg);
        public void DisplayPlayerVictory(IBoard inBoard, char inMarker);
        public void DisplayDrawnGame(IBoard inBoard);
        public void DisplayReplayMessage();
        public void DisplaySaveMessage(bool inSaveOkay);
    }
}