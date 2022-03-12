using System;
using System.Collections.Generic;
using System.IO;

namespace TicTacToeProgram.filehandler
{
    public class GameFileHandler : IFileHandler<GameData>
    {
        private string _filename;

        public GameFileHandler(string inFilename)
        {
            _filename = inFilename;
        }
        
        public GameData Load()
        {
            GameData result;
            
            try
            {
                result = LoadData(_filename);
            }
            catch (Exception)
            {
                result = GameData.Default;
            }
            return result;
        }

        private GameData LoadData(string inFilename)
        {
            const int BoardState = 0, PlayerOne = 1, PlayerTwo = 2, NextPlayerNo = 3, GameMode = 4;
            List<string> lines = new();
            FileStream loadStream;  
            GameData result;
            
            string pathAndFile = Directory.GetCurrentDirectory() + inFilename;
            loadStream = File.Open(pathAndFile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(loadStream);
            while (!sr.EndOfStream)
                lines.Add(sr.ReadLine());
            sr.Close();
            loadStream.Close();
            result = GameData.CreateWithStrings(lines[BoardState], 
                lines[PlayerOne], lines[PlayerTwo],
                lines[NextPlayerNo], lines[GameMode]);
            return result;
        }

        public bool Save(GameData inData)
        {
            bool result;
            
            try
            {
                result = SaveData(inData, _filename);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private bool SaveData(GameData inData, string inFilename)
        {
            FileStream saveStream;

            string pathAndFile = Directory.GetCurrentDirectory() + inFilename;
            saveStream = File.Open(pathAndFile, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(saveStream);
            sw.WriteLine(inData.Serialise());
            sw.Close();
            saveStream.Close();
            return true;
        }
    }
}