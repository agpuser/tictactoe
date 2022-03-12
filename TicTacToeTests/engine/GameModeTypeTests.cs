using System.Runtime.InteropServices;
using TicTacToeProgram.engine;
using Xunit;

namespace TicTacToeTests.engine
{
    public class GameModeTypeTests
    {
        [Theory]
        [InlineData("0", true, GameModeEnum.HumanVsHuman)]
        [InlineData("a-1", false, GameModeEnum.HumanVsComputer)]
        public void ParseFromString_ConfirmGameModeObtainedOrNotFromString(
            string input, bool expected, GameModeEnum inGameMode)
        {
            GameModeType expectedGameMode = GameModeType.New(inGameMode);
            
            GameModeType sut = GameModeType.ParseFromString(input);
            bool actual = sut.Equals(expectedGameMode);
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("2", true, GameModeEnum.HumanVsComputer)]
        [InlineData("a+3", false, GameModeEnum.HumanVsComputer)]
        public void ParseFromMenuOption_ConfirmGameModeObtainedOrNotFromMenuOptionString(
            string input, bool expected, GameModeEnum inGameMode)
        {
            GameModeType expectedGameMode = GameModeType.New(inGameMode);
            
            GameModeType sut = GameModeType.ParseFromMenuOption(input);
            bool actual = sut.Equals(expectedGameMode);
            
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(GameModeEnum.HumanVsHuman, "HumanVsHuman")]
        [InlineData(GameModeEnum.HumanVsComputer, "HumanVsComputer")]
        public void ToString_ConfirmGameModeAsString(GameModeEnum inGameMode, string expected)
        {
            GameModeType sut = GameModeType.New(inGameMode);
            
            string actual = sut.ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}