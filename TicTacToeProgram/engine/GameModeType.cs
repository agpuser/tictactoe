using System;

namespace TicTacToeProgram.engine
{
    public class GameModeType
    {
        public static GameModeType Default = new GameModeType(GameModeEnum.HumanVsHuman);
        public static GameModeType HumanVsHuman = new GameModeType(GameModeEnum.HumanVsHuman);
        public static GameModeType HumanVsComputer = new GameModeType(GameModeEnum.HumanVsComputer);
        public static GameModeType New(GameModeEnum inGameMode) => new GameModeType(inGameMode);

        public static GameModeType ParseFromString(string inModeString)
        {
            GameModeEnum temp;
            GameModeType result;
            
            try
            {
                temp = (GameModeEnum) Enum.Parse(typeof(GameModeEnum), inModeString, true);
                result = new GameModeType(temp);
            }
            catch (Exception)
            {
                result = new GameModeType();
            }
            return result;
        }
        
        public static GameModeType ParseFromMenuOption(string inModeString)
        {
            GameModeEnum temp;
            GameModeType result;
            
            try
            {
                temp = (GameModeEnum)(int.Parse(inModeString) - 1);
                result = new GameModeType(temp);
            }
            catch (Exception)
            {
                result = new GameModeType();
            }
            return result;
        }
        
        public GameModeEnum Mode { get; set; }

        private GameModeType()
        {
            const int firstEnumValue = 0;
            Mode = (GameModeEnum)firstEnumValue;
        }
        
        private GameModeType(GameModeEnum inMode)
        {
            Mode = inMode;
        }

        public bool Equals(GameModeType other) => Mode == other.Mode;

        public override string ToString()
        {
            return Mode.ToString();
        }
    }
}