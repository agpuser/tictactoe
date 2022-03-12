using System.Collections.Generic;
using TicTacToeProgram.engine;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.player
{
    public class PlayerCollection
    {
        public static PlayerCollection CreateHumanVsHumanCollection(IUserInput inInput, GameData inState) => 
                new PlayerCollection(inInput, inState);
        
        public static PlayerCollection CreateHumanVsComputerCollection(
            IUserInput inInput, GameData inState, IGameReferee inReferee) => 
                new PlayerCollection(inInput, inState, inReferee);
        
        private List<IPlayer> _players;
        private int _index;
        
        public GameModeType GameMode { get; private set;  }

        public PlayerCollection(IPlayer inPlayerX, IPlayer inPlayerO, int inNextPlayerNumber)
        {
            AddPlayers(inPlayerX, inPlayerO);
            SetupPlayerIndex(inNextPlayerNumber);
        }

        private PlayerCollection(IUserInput inInput, GameData inState)
        {
            InitialiseCollectionAndGameMode(inState.GameMode);
            _players.Add(CreateHumanPlayer(inInput, inState.PlayerOneMarker, 1));
            _players.Add(CreateHumanPlayer(inInput, inState.PlayerTwoMarker, 2));
            SetupPlayerIndex(inState.NextPlayerNumber);
        }

        private PlayerCollection(IUserInput inInput, GameData inState, IGameReferee inReferee)
        {
            InitialiseCollectionAndGameMode(inState.GameMode);
            _players.Add(CreateHumanPlayer(inInput, inState.PlayerOneMarker, 1));
            _players.Add(CreateComputerPlayer(inReferee, inState.PlayerTwoMarker, 2));
            SetupPlayerIndex(inState.NextPlayerNumber);
        }

        private void InitialiseCollectionAndGameMode(GameModeType inGameMode)
        {
            _players = new List<IPlayer>();
            GameMode = inGameMode;
        }

        private IPlayer CreateHumanPlayer(IUserInput inInput, char inMarker, int inNumber) =>
            GamePlayer.CreateHumanConsolePlayer(inInput, inMarker, inNumber);
        
        private IPlayer CreateComputerPlayer(IGameReferee inReferee, char inMarker, int inNumber) =>
            GamePlayer.CreateComputerConsolePlayer(inReferee, inMarker, inNumber);
        
        private void AddPlayers(IPlayer inXPlayer, IPlayer inOPlayer)
        {
            _players = new List<IPlayer>();
            _players.Add(inXPlayer);
            _players.Add(inOPlayer);
        }

        private void SetupPlayerIndex(int inNextPlayerNumber)
        {
            _index = inNextPlayerNumber - 1;
        }
        
        public IPlayer Next()
        {
            IPlayer result;
            result = _players[_index++];
            _index = _index % _players.Count;
            return result;
        }

        public bool Equals(PlayerCollection other)
        {
            IPlayer thisPlayer = Next();
            IPlayer otherPlayer = other.Next();
            bool firstPlayerEqual = thisPlayer.PlayerNumber == otherPlayer.PlayerNumber &&
                                    thisPlayer.Marker == otherPlayer.Marker;
            thisPlayer = Next();
            otherPlayer = other.Next();
            bool secondPlayerEqual = thisPlayer.PlayerNumber == otherPlayer.PlayerNumber &&
                                     thisPlayer.Marker == otherPlayer.Marker;
            bool modesAreEqual = GameMode.Equals(other.GameMode);
            return firstPlayerEqual && secondPlayerEqual && modesAreEqual;
        }
    }
}