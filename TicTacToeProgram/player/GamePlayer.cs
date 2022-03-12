using System;
using TicTacToeProgram.board;
using TicTacToeProgram.engine;
using TicTacToeProgram.ProgramIO;

namespace TicTacToeProgram.player
{
    public class GamePlayer : IPlayer
    {
        public static IPlayer CreateHumanConsolePlayer(IUserInput inInput, char inMarker, int inPlayerNum) =>
            new GamePlayer(HumanConsoleResponder.CreateStandard(inInput), inMarker, inPlayerNum);
        
        public static IPlayer CreateComputerConsolePlayer(IGameReferee inReferee, char inMarker, int inPlayerNum) =>
                new GamePlayer(ComputerConsoleResponder.CreateStandard(inReferee), inMarker, inPlayerNum);

        private readonly IResponder _responder;
        
        public char Marker { get; }
        public int PlayerNumber { get; }
        
        public GamePlayer(IResponder inResponder, char inMarker, int inNumber)
        {
            _responder = inResponder;
            Marker = inMarker;
            PlayerNumber = inNumber;
        }
        
        public string GetResponse(IBoard inBoard) => _responder.GetResponse(inBoard, Marker);
    }
}