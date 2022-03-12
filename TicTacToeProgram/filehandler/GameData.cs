using System;
using System.Text;
using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeProgram.player;

namespace TicTacToeProgram.filehandler
{
    public class GameData
    {
        public static GameData Default = new GameData(".........", 
            'X', 'O', 1, GameModeType.Default);
        
        public static GameData CreateNew(string inBoardState, char inPlayerOneMarker, char inPlayerTwoMarker,
            int inNextPlayerNum, GameModeType inGameMode) =>
                new GameData(inBoardState, inPlayerOneMarker, inPlayerTwoMarker, inNextPlayerNum, inGameMode);
        
        public static GameData CreateWithStrings(string inBoardState, string inPlayerOneMarker,
            string inPlayerTwoMarker, string inNextPlayerNum, string inGameMode) =>
            new GameData(inBoardState, inPlayerOneMarker, inPlayerTwoMarker, inNextPlayerNum, inGameMode);
        
        public static GameData CreateUsingBoardPlayerAndMode(IBoard inBoard, IPlayer inPlayer, GameModeType inMode) =>
            new GameData(inBoard, inPlayer, inMode);
        
        public static GameData CreateForNewGame(string inPlayerOneMarker, string inGameMode) =>
                new GameData(inPlayerOneMarker, inGameMode);

        public string BoardState { get; set; }
        public char PlayerOneMarker { get; set; }
        public char PlayerTwoMarker { get; set; }
        public int NextPlayerNumber { get; set; }
        public GameModeType GameMode { get; set; }

        private GameData(string inBoardState, string inPlayerOneMarker, string inPlayerTwoMarker,
            string inNextPlayerNum, string inGameMode)
        {
            BoardState = inBoardState;
            PlayerOneMarker = char.Parse(inPlayerOneMarker);
            PlayerTwoMarker = char.Parse(inPlayerTwoMarker);
            NextPlayerNumber = int.Parse(inNextPlayerNum);
            GameMode = GameModeType.ParseFromString(inGameMode);
        }

        private GameData(string inBoardState, char inPlayerOneMarker, char inPlayerTwoMarker,
            int inNextPlayerNum, GameModeType inGameMode)
        {
            BoardState = inBoardState;
            PlayerOneMarker = inPlayerOneMarker;
            PlayerTwoMarker = inPlayerTwoMarker;
            NextPlayerNumber = inNextPlayerNum;
            GameMode = inGameMode;
        }

        private GameData(string inPlayerOneMarker, string inGameMode)
        {
            BoardState = ".........";
            PlayerOneMarker = char.Parse(inPlayerOneMarker);
            PlayerTwoMarker = PlayerOneMarker == 'X' ? 'O' : 'X';
            NextPlayerNumber = 1;
            GameMode = GameModeType.ParseFromMenuOption(inGameMode);
        }

        private GameData(IBoard inBoard, IPlayer inPlayer, GameModeType inMode)
        {
            char[] markers;

            BoardState = inBoard.Serialise();
            markers = GetPlayerOneAndTwoMarkers(inPlayer.PlayerNumber, inPlayer.Marker);
            PlayerOneMarker = markers[0];
            PlayerTwoMarker = markers[1];
            NextPlayerNumber = inPlayer.PlayerNumber;
            GameMode = inMode;
        }

        private char[] GetPlayerOneAndTwoMarkers(int inNumber, char inMarker)
        {
            char playerOneMarker, playerTwoMarker;
            char[] markers = new char[2];
            
            if (inNumber == 1)
            {
                playerOneMarker = inMarker;
                playerTwoMarker = playerOneMarker == 'X' ? 'O' : 'X';
            }
            else
            {
                playerTwoMarker = inMarker;
                playerOneMarker = playerTwoMarker == 'X' ? 'O' : 'X';
            }
            markers[0] = playerOneMarker;
            markers[1] = playerTwoMarker;
            return markers;
        }

        public bool Equals(GameData other)
        {
            bool result =
                BoardState == other.BoardState &&
                PlayerOneMarker == other.PlayerOneMarker &&
                PlayerTwoMarker == other.PlayerTwoMarker &&
                NextPlayerNumber == other.NextPlayerNumber &&
                GameMode.Equals(other.GameMode);
            return result;
        }

        public string Serialise()
        {
            StringBuilder result = new(BoardState);
            result.Append(Environment.NewLine);
            result.Append(PlayerOneMarker.ToString());
            result.Append(Environment.NewLine);
            result.Append(PlayerTwoMarker.ToString());
            result.Append(Environment.NewLine);
            result.Append(NextPlayerNumber.ToString());
            result.Append(Environment.NewLine);
            result.Append(GameMode.ToString());
            return result.ToString();
        }
    }
}