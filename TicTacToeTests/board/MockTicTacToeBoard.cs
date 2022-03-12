using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeProgram.board;

namespace TicTacToeTests.board
{
    public class MockTicTacToeBoard : IBoard
    {
        private char[] _board;

        public int MaxRow { get; }
        public int MaxCol { get; }
        public char this[int inRow, int inCol] => _board[(inRow * 3) + inCol];

        public MockTicTacToeBoard()
        {
            Reset();
            MaxRow = 3;
            MaxCol = 3;
        }
        
        public MockTicTacToeBoard(string inSpaces) : this()
        {
            Init(inSpaces);
        }

        public void Init(string inSpaces)
        {
            _board = inSpaces.ToCharArray();
        }

        public bool IsEmpty()
        {
            int count = 0;
            new List<char>(_board).ForEach(s => count += s == '.' ? 1 : 0 );
            return count == _board.Length;
        }

        public void AddMarker(Position inPosition, char inMarker)
        {
            _board[(inPosition.Row * 3) + inPosition.Col] = inMarker;
        }

        public void Remove(Position inPosition)
        {
            int pos = (inPosition.Row * 3) + inPosition.Col;
            _board[pos] = '.';
        }

        public void Reset()
        {
            _board = new char[9]
            {
                '.', '.', '.', '.', '.', '.', '.', '.', '.'
            };
        }

        public bool IsPositionEmpty(Position inPosition)
        {
            return _board[inPosition.Row * 3 + inPosition.Col] == '.';
        }

        public bool AllSpacesPlayed()
        {
            int count = 0;
            
            foreach (char space in _board)
                count += space == '.' ? 0 : 1;
            return count == 9;
        }

        public bool IsValidPosition(Position inPosition)
        {
            return true;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < 9; i++)
            {
                result += _board[i] + " ";
                if (((i+1) % 3 == 0) && i != 8)
                    result += Environment.NewLine;
            }
            return result;
        }

        public string Serialise()
        {
            return _board.ToArray().ToString();
        }
    }
}