using System;
using System.Collections.Generic;
using TicTacToeProgram.board;
using TicTacToeProgram.player;

namespace TicTacToeTests.ProgramIO
{
    public class FakeDisplayOutputDevice : ITestOutputWriter
    {
        public string InvalidInputMessage { get; private set; }

        private List<string> _buffer = new();

        public void DisplayGameStartScreen()
        {
            _buffer.Add("Welcome to Tic Tac Toe!");
            _buffer.Add("");
        }

        public void DisplayMainMenu()
        {
            _buffer.Add("Main Menu");
        }

        public void DisplayGameModePromptMessage()
        {
            _buffer.Add("Please enter game mode:");
        }

        public void DisplayGetMarkerPromptMessage()
        {
            _buffer.Add("Player 1 please select your marker ('X' or 'O'): ");
        }

        public void DisplayPlayScreen(IBoard inBoard, IPlayer inPlayer)
        {
            _buffer.Add("Board State");
        }

        public void DisplayInitialBoardMessage()
        {
            _buffer.Add("Here's the current board:");
        }

        public void DisplayMoveAcceptedMessage()
        {
            _buffer.Add("Move accepted, here's the current board:");
        }

        public void DisplayPlayerVictory(IBoard inBoard, char inMarker)
        {
            _buffer.Add($"Player {inMarker} wins!");
        }

        public void DisplayDrawnGame(IBoard inBoard)
        {
            _buffer.Add("Drawn game.");
        }

        public void DisplayInvalidPositionMessage(string inMsg)
        {
            InvalidInputMessage = inMsg;
        }

        public void DisplayReplayMessage()
        {
            _buffer.Add("Do you wish to play again ('y' or 'n'): ");
        }

        public void Write(string inData)
        {
            _buffer.Add(inData);
        }
        
        public void WriteLine(string inLine)
        {
            Write(inLine + Environment.NewLine);
        }

        public void WriteLine()
        {
            WriteLine("");
        }

        public string GetDisplay()
        {
            string result = "";

            _buffer.ForEach(s => result += s);
            return result;
        }

        public string GetDisplayLineNUmber(int inLineNumber) => _buffer[inLineNumber - 1];

        public string GetLastDisplayLine() => _buffer[^1];

        public void DisplayDummyText()
        {
            Console.WriteLine("Dummy prompt text");
        }

        public void DisplaySaveMessage(bool inSaveOkay)
        {
            _buffer.Add("Game saved.");
        }
    }
}