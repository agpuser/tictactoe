using System;
using TicTacToeProgram.board;

namespace TicTacToeProgram.player
{
    public interface IPlayer
    {
        public char Marker { get; }
        public int PlayerNumber { get; }
        public string GetResponse(IBoard inBoard);
    }
}