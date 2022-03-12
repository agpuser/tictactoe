using System;
using TicTacToeProgram.board;
using TicTacToeProgram.player;

namespace TicTacToeProgram.ProgramIO
{
    public class ConsoleDisplayOutput : IDisplayOutput
    {
        public void DisplayGameStartScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" Welcome to Tic Tac Toe! ");
            Console.WriteLine();
        }

        public void DisplayInitialBoardMessage()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Here's the current board:");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayPlayScreen(IBoard inBoard, IPlayer inPlayer)
        {
            Console.WriteLine();
            DisplayBoard(inBoard);
            DisplayPlayerPrompt(inPlayer);
        }

        private void DisplayPlayerPrompt(IPlayer inPlayer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Player: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{inPlayer.PlayerNumber}.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Environment.NewLine);
            Console.Write(" Enter a ");
            DisplayOption("row,col");
            Console.Write($") for your ");
            DisplayMarker(inPlayer.Marker);
            Console.Write(", ");
            DisplayOption("s");
            Console.Write(" to save the game or ");
            DisplayOption("q");
            Console.Write(" to quit: ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayMoveAcceptedMessage()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Move accepted, here's the current board:");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" MAIN MENU");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" 1.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" Human vs Human");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" 2. ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Human vs Computer");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" 3. ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Load save game");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" 4. ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Exit program");
            Console.WriteLine();
        }

        public void DisplayGameModePromptMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Please select option (");
            DisplayOption("1");
            Console.Write(", ");
            DisplayOption("2");
            Console.Write(", ");
            DisplayOption("3");
            Console.Write(" or ");
            DisplayOption("4");
            Console.Write("): ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void DisplayOption(string inOption)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("'");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(inOption);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("'");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void DisplayGetMarkerPromptMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Player 1 please select your marker (");
            DisplayOption("X");
            Console.Write(" or ");
            DisplayOption("O");
            Console.Write("): ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayInvalidPositionMessage(string inMsg)
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(inMsg);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayPlayerVictory(IBoard inBoard, char inMarker)
        {
            Console.WriteLine();
            DisplayBoard(inBoard);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" Player ");
            DisplayMarker(inMarker);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" wins!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayDrawnGame(IBoard inBoard)
        {
            Console.WriteLine();
            DisplayBoard(inBoard);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" No winner. Game drawn.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        public void DisplayReplayMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.Write(" Do you wish to play again (");
            DisplayOption("y");
            Console.Write(" or ");
            DisplayOption("n");
            Console.Write("): ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplaySaveMessage(bool inSaveOkay)
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            if (inSaveOkay)
                Console.Write(" Game successfully saved.");
            else
                Console.Write(" Game save failed. Please try again.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        private void DisplayBoard(IBoard inBoard)
        {
            string board = inBoard.Serialise();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("        1   2   3    COL");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("      +---+---+---+");
            for (int i = 0; i < inBoard.MaxRow; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"  {i+1}   ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("| ");
                for (int j = 0; j < inBoard.MaxCol; j++)
                {
                    DisplayMarker(board[(i * inBoard.MaxCol) + j]);
                    Console.Write(" | ");
                    Console.Write(j == inBoard.MaxRow - 1 ? Environment.NewLine : "");
                }
                Console.WriteLine("      +---+---+---+");
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" ROW");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(Environment.NewLine);
        }

        private void DisplayMarker(char inMarker)
        {
            Console.ForegroundColor = GetMarkerColour(inMarker);
            Console.Write(inMarker);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private ConsoleColor GetMarkerColour(char inMarker)
        {
            const char XMarker = 'X', OMarker = 'O';
            ConsoleColor result;
            switch (inMarker)
            {
                case XMarker:
                    result = ConsoleColor.Magenta;
                    break;
                case OMarker:
                    result = ConsoleColor.Yellow;
                    break;
                default:
                    result = ConsoleColor.DarkGray;
                    break;
            }
            return result;
        }
    }
}