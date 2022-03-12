using System;
using System.Collections.Generic;
using TicTacToeProgram.board;
using TicTacToeProgram.engine;

namespace TicTacToeProgram.player
{
    public class ComputerConsoleResponder : IResponder
    {
        public static ComputerConsoleResponder CreateStandard(IGameReferee inReferee) =>
            new ComputerConsoleResponder(inReferee);
        
        private const int MinScore = -1000, MaxScore = 1000, WinScore = 10, LoseScore = -10;
        private const char X = 'X', O = 'O';
        
        private IGameReferee _referee;
        
        public ComputerConsoleResponder(IGameReferee inReferee)
        {
            _referee = inReferee;
        }

        public string GetResponse(IBoard inBoard, char inMarker)
        { 
            Position result = FindBestMove(inBoard, inMarker);
            return (result.Row + 1) + "," + (result.Col + 1);
        }

         private int Evaluate(IBoard inBoard, char maxPlayerMarker, int depth)
         {
             int score = 0;
             
             for (int row = 0; row < inBoard.MaxRow; row++)
             {
                 for (int col = 0; col < inBoard.MaxCol; col++)
                 {
                     score = GetEndOfTurnScore(new Position(row, col), inBoard, maxPlayerMarker, depth);
                     if (score != 0)
                         return score;
                 }
             }
             return score;
         }

         private int GetEndOfTurnScore(Position inPosition, IBoard inBoard, char maxPlayerMarker, int depth)
         {
             int score = 0;

             GameStatus status = _referee.GetEndOfTurnStatus(inPosition, inBoard);
             if (maxPlayerMarker == X)
             {
                 if (status == GameStatus.PlayerXWin)
                     return WinScore - depth;
                 if (status == GameStatus.PlayerOWin)
                     return LoseScore + depth;
             }
                     
             if (maxPlayerMarker == O)
             {
                 if (status == GameStatus.PlayerXWin)
                     return LoseScore + depth;
                 if (status == GameStatus.PlayerOWin)
                     return WinScore - depth;
             }
             return score;
         }

         private int Minimax(IBoard inBoard, char maxPlayerMarker, char playerMarker, int depth, bool isMax)
         {
             int score = Evaluate(inBoard, maxPlayerMarker, depth);
             if (score == WinScore || score == WinScore + depth || 
                 score == LoseScore || score == depth + LoseScore)
                 return score;
             if (inBoard.AllSpacesPlayed())
                 return 0;

             char otherMarker = playerMarker == X ? O : X;
             if (isMax)
                 return GetBestScore(MinScore, inBoard, maxPlayerMarker, otherMarker, depth, !isMax, true);
             else
                 return GetBestScore(MaxScore, inBoard, maxPlayerMarker, otherMarker, depth, !isMax, false);
         }

         private int GetBestScore(int inBest, IBoard inBoard, char maxPlayerMarker,
             char nextPlayerMarker, int inDepth, bool inIsMaxPlayer, bool inMaxValue)
         {
             int best = inBest;
             
             for (int row = 0; row < inBoard.MaxRow; row++)
                for (int col = 0; col < inBoard.MaxCol; col++) 
                    best = GetMinMaxBestScore(best, inBoard, new Position(row, col), maxPlayerMarker, 
                        nextPlayerMarker, inDepth, !inIsMaxPlayer, inMaxValue);
             return best;
         }

         private int GetMinMaxBestScore(int inBest, IBoard inBoard, Position inPosition, char maxPlayerMarker,
             char nextPlayerMarker, int inDepth, bool inIsMaxPlayer, bool inMaxValue)
         {
             int result = inBest;
             
             if (inBoard.IsPositionEmpty(inPosition))
             {
                 inBoard.AddMarker(inPosition, nextPlayerMarker);
                 if (inMaxValue)
                     result = Math.Max(result, Minimax(inBoard, maxPlayerMarker, 
                     maxPlayerMarker, inDepth + 1, !inIsMaxPlayer));
                 else
                     result = Math.Min(result, Minimax(inBoard, maxPlayerMarker, 
                         nextPlayerMarker, inDepth + 1, !inIsMaxPlayer));
                 inBoard.Remove(inPosition);
             }
             return result;
         }

         private Position FindBestMove(IBoard inBoard, char maxPlayerMarker)
         {
             int moveVal, bestVal = MinScore;
             Position bestMove = Position.InvalidPosition;
             List<int> moves = new();

             for (int row = 0; row < inBoard.MaxRow; row++)
             {
                 for (int col = 0; col < inBoard.MaxCol; col++)
                 {
                     if (inBoard.IsPositionEmpty(new Position(row, col)))
                     {
                         inBoard.AddMarker(new Position(row, col), maxPlayerMarker);
                         moveVal = Minimax(inBoard, maxPlayerMarker, maxPlayerMarker, 0, false);
                         inBoard.Remove(new Position(row, col));
                         moves.Add(moveVal);
                         if (moveVal > bestVal)
                         {
                             bestMove = new Position(row, col);
                             bestVal = moveVal;
                         }
                     }
                 }
             }
             return bestMove;
         }
    }
}