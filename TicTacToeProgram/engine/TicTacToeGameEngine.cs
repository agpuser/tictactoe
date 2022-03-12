using System;
using TicTacToeProgram.board;
using TicTacToeProgram.builder;
using TicTacToeProgram.filehandler;
using TicTacToeProgram.player;
using TicTacToeProgram.ProgramIO;
using TicTacToeProgram.validation;

namespace TicTacToeProgram.engine
{
    public class TicTacToeGameEngine : IGameEngine
    {
        public bool QuitProgramSignalled { get; private set; }
        
        private readonly IUserInput _input;
        private readonly IDisplayOutput _output;
        private readonly IGameReferee _referee;
        private readonly IFileHandler<GameData> _fileHandler;
        private readonly IBoard _board;

        public TicTacToeGameEngine(IBoard inBoard, IUserInput inInput, 
            IDisplayOutput inOutput, IGameReferee inReferee, IFileHandler<GameData> inFileHandler)
        {
            _board = inBoard;
            _input = inInput;
            _output = inOutput;
            _referee = inReferee;
            _fileHandler = inFileHandler;
        }
        
        public void Run()
        {
            while (!QuitProgramSignalled)
                RunGameProgram();
        }

        private void RunGameProgram()
        {
            string mainMenuOption = RunMainMenu();
            if (UserSignalledToExitProgram(mainMenuOption))
                QuitProgramSignalled = true;
            else
                RunGame(mainMenuOption);
        }
        
        private string RunMainMenu()
        {
            _output.DisplayGameStartScreen();
            return DisplayMainMenuAndGetUserMenuOption();
        }
        
        private string DisplayMainMenuAndGetUserMenuOption()
        {
            string[] validValues = { "1", "2", "3", "4" };

            _output.DisplayMainMenu();
            return PromptUser(_output.DisplayGameModePromptMessage, validValues);
        }

        private void RunGame(string inMainMenuOption)
        {
            PlayerCollection players = RunPreGame(inMainMenuOption);
            GameStatus status = RunMainGame(players);
            RunPostGame(status);
        }

        private PlayerCollection RunPreGame(string inMenuOption)
        {
            GameData gameState;

            if (inMenuOption == "3")
                gameState = SetupGameFromFile();
            else
                gameState = SetupGameStateFromUser(inMenuOption);
            return SetupGameAndGetPlayers(gameState);
        }

        private GameData SetupGameFromFile() => _fileHandler.Load();

        private GameData SetupGameStateFromUser(string inGameMode)
        {
            string[] validValues = { "X", "O" };

            string playerOneMarker = PromptUser(_output.DisplayGetMarkerPromptMessage, validValues);
            return GameData.CreateForNewGame(playerOneMarker, inGameMode);
        }

        private PlayerCollection SetupGameAndGetPlayers(GameData inState)
        {
            _board.Init(inState.BoardState);
            return GetGamePlayers(inState);
        }

        private PlayerCollection GetGamePlayers(GameData inState)
        {
            PlayerCollection result;
            PlayersDirector director;

            switch (inState.GameMode.Mode)
            {
                case GameModeEnum.HumanVsHuman:
                    HumanVsHumanPlayerCollectionBuilder builderHvH = new HumanVsHumanPlayerCollectionBuilder();
                    director = new PlayersDirector(builderHvH);
                    director.ConstructHumanVsHumanCollection(inState, _input);
                    result = builderHvH.GetProduct();
                    break;
                default: // GameNodeEnum.HumanVsComputer
                    HumanVsComputerPlayerCollectionBuilder builderHvC = new HumanVsComputerPlayerCollectionBuilder();
                    director = new PlayersDirector(builderHvC);
                    director.ConstructHumanVsComputerCollection(inState, _input, _referee);
                    result = builderHvC.GetProduct();
                    break;
            }
            return result;
        }

        private GameStatus RunMainGame(PlayerCollection inPlayers)
        {
            GameStatus result = GameStatus.NewTurn;
            
            _output.DisplayInitialBoardMessage();
            while (NotGameOver(result))
            {
                IPlayer nextPlayer = inPlayers.Next();
                result = RunPlayerTurn(_board, nextPlayer, inPlayers.GameMode);
            }
            return result;
        }

        public GameStatus RunPlayerTurn(IBoard inBoard, IPlayer inPlayer, GameModeType inGameMode)
        {
            GameStatus playerTurnResult = GameStatus.NewTurn;

            while (TurnNotCompleted(playerTurnResult))
            {
                _output.DisplayPlayScreen(inBoard, inPlayer);
                string response = inPlayer.GetResponse(inBoard);
                if (UserSignalledToQuit(response))
                    playerTurnResult = GameStatus.QuitSignalled;
                else if (UserSignalledToSave(response))
                    playerTurnResult = SaveGameState(inBoard, inPlayer, inGameMode);
                else
                    playerTurnResult = ParseAndTryToPlayPositionThenReturnResult(response, inBoard, inPlayer);
            }
            DisplayMoveAcceptedMessage(playerTurnResult);
            return playerTurnResult;
        }
 
        private GameStatus SaveGameState(IBoard inBoard, IPlayer inPlayer, GameModeType inGameMode)
        {
            GameData saveData = GameData.CreateUsingBoardPlayerAndMode(inBoard, inPlayer, inGameMode);
            if (_fileHandler.Save(saveData))
                _output.DisplaySaveMessage(true);
            else
                _output.DisplaySaveMessage(false);
            return GameStatus.SaveGame;
        }
        
        private void DisplayMoveAcceptedMessage(GameStatus inStatus)
        {
            if (inStatus != GameStatus.QuitSignalled && inStatus != GameStatus.SaveGame)
                _output.DisplayMoveAcceptedMessage();
        }
        
        private GameStatus ParseAndTryToPlayPositionThenReturnResult(string inResponse, IBoard inBoard, IPlayer inPlayer)
        {
            TicTacToeValidator ticTacToeValidator = new TicTacToeValidator();

            Position pos = ticTacToeValidator.GetPositionFromString(inResponse);
            string msg = ticTacToeValidator.GetValidationPositionMessage(pos, inBoard);
            return DisplayInvalidPositionMessageOrPlayPosition(pos, inPlayer, inBoard, msg);
        }

        private GameStatus DisplayInvalidPositionMessageOrPlayPosition(
            Position inPosition, IPlayer inPlayer, IBoard inBoard, string inMsg)
        {
            GameStatus result;
            
            if (inMsg != String.Empty)
            {
                _output.DisplayInvalidPositionMessage(inMsg);
                result = GameStatus.InvalidInput;
            }
            else
                result = PlayPositionAndGetEndTurnStatus(inPosition, inPlayer, inBoard);
            return result;
        }
        
        private GameStatus PlayPositionAndGetEndTurnStatus(
            Position inPosition, IPlayer inPlayer, IBoard inBoard)
        {
            inBoard.AddMarker(inPosition, inPlayer.Marker);
            return _referee.GetEndOfTurnStatus(inPosition, inBoard);
        }
        
        private void RunPostGame(GameStatus inStatus)
        {
            RunEndGame(inStatus);
            PromptUserToReplayGameAndProcessResponse();
        }

        private void RunEndGame(GameStatus inStatus)
        {
            const char XMarker = 'X', OMarker = 'O';
            switch (inStatus)
            {
                case GameStatus.DrawnGame: 
                    CheckIfDrawnGameAndRunDrawnEndGame(inStatus);
                    break;
                case GameStatus.PlayerXWin:
                    RunPlayerVictoryEndGame(XMarker);
                    break; 
                case GameStatus.PlayerOWin:
                    RunPlayerVictoryEndGame(OMarker);
                    break;
            }
        }
        
        private void CheckIfDrawnGameAndRunDrawnEndGame(GameStatus inStatus)
        {
            if (inStatus == GameStatus.DrawnGame)
                RunDrawnEndGame();
        }
        
        private void RunPlayerVictoryEndGame(char inMarker) => _output.DisplayPlayerVictory(_board, inMarker);

        private void RunDrawnEndGame() => _output.DisplayDrawnGame(_board);

        private void PromptUserToReplayGameAndProcessResponse()
        {
            string[] validValues = { "y", "n" };
            string response = PromptUser(_output.DisplayReplayMessage, validValues);
            ProcessReplayResponse(response);
        }

        private void ProcessReplayResponse(string inResponse)
        {
            if (UserResponseIsToPlayAgain(inResponse))
                RunGameReset();
            else
                QuitProgramSignalled = true;
        }

        private void RunGameReset()
        {
            _board.Reset();
            QuitProgramSignalled = false;
        }
        
        private string PromptUser(Action inDisplayMethod, string[] inValidValues)
        {
            IValidator validator = new TicTacToeValidator();
            string result = "";

            do
            {
                inDisplayMethod();
                result = _input.ReadInput();
            } while (!validator.IsValidValue(result, inValidValues));
            return result;
        }
        
        private bool NotGameOver(GameStatus inStatus) =>
            inStatus != GameStatus.PlayerXWin && inStatus != GameStatus.PlayerOWin &&
            inStatus != GameStatus.DrawnGame && inStatus != GameStatus.QuitSignalled;

        private bool TurnNotCompleted(GameStatus inStatus) =>
            inStatus == GameStatus.NewTurn || inStatus == GameStatus.InvalidInput ||
            inStatus == GameStatus.SaveGame;

        private bool UserSignalledToExitProgram(String inString) => inString.ToLower() == "4";
        private bool UserSignalledToQuit(string inString) => inString.ToLower() == "q";
        private bool UserSignalledToSave(string inString) => inString.ToLower() == "s";
        
        private bool UserResponseIsToPlayAgain(string inResponse) => inResponse == "y";
    }
}