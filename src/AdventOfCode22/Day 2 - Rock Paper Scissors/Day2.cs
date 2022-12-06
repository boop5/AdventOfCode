// ReSharper disable once CheckNamespace
namespace AdventOfCode22.Day2;

public class Day2
{
    private static string OPPONENT_ROCK = "A";
    private static string OPPONENT_PAPER = "B";
    private static string OPPONENT_SCISSOR = "C";
    private static string PLAYER_ROCK = "X";
    private static string PLAYER_PAPER = "Y";
    private static string PLAYER_SCISSORS = "Z";

    private static int RESULT_LOSE = 0;
    private static int RESULT_WIN = 6;
    private static int RESULT_DRAW = 3;

    [Fact]
    public void SolvePart1()
    {
        int CalculateMatchResult(string arg)
        {
            var picks = arg.Split(" ");
            var opponentsPlay = picks[0];
            var myPlay = picks[1];

            return PointsFromTool(myPlay) + PointsFromPlay(opponentsPlay, myPlay);
        }

        var points = InputReader.ReadLines().Select(CalculateMatchResult).Sum();

        Assert.Equal(11767, points);
    }

    [Fact]
    public void SolvePart2()
    {
        var RESULT_LOSE = "X";
        var RESULT_DRAW = "Y";
        var RESULT_WIN = "Z";

        string GetPlay(string opponentPlay, string expectedResult)
        {
            if (expectedResult == RESULT_LOSE)
            {
                if (opponentPlay == OPPONENT_ROCK) return PLAYER_SCISSORS;
                if (opponentPlay == OPPONENT_PAPER) return PLAYER_ROCK;
                if (opponentPlay == OPPONENT_SCISSOR) return PLAYER_PAPER;
            }
            else if (expectedResult == RESULT_DRAW)
            {
                if (opponentPlay == OPPONENT_ROCK) return PLAYER_ROCK;
                if (opponentPlay == OPPONENT_PAPER) return PLAYER_PAPER;
                if (opponentPlay == OPPONENT_SCISSOR) return PLAYER_SCISSORS;
            }
            else if (expectedResult == RESULT_WIN)
            {
                if (opponentPlay == OPPONENT_ROCK) return PLAYER_PAPER;
                if (opponentPlay == OPPONENT_PAPER) return PLAYER_SCISSORS;
                if (opponentPlay == OPPONENT_SCISSOR) return PLAYER_ROCK;
            }

            throw new ArgumentOutOfRangeException();
        }

        int CalculateMatchResult(string input)
        {
            var columns = input.Split(" ");
            var opponentPlay = columns[0];
            var expectedResult = columns[1];

            var play = GetPlay(opponentPlay, expectedResult);

            return PointsFromPlay(opponentPlay, play) + PointsFromTool(play);
        }

        var points = InputReader.ReadLines().Select(CalculateMatchResult).Sum();

        Assert.Equal(13886, points);
    }

    int PointsFromPlay(string opponent, string player)
    {
        if (opponent == OPPONENT_ROCK)
        {
            if (player == PLAYER_ROCK) return RESULT_DRAW;
            if (player == PLAYER_PAPER) return RESULT_WIN;
            if (player == PLAYER_SCISSORS) return RESULT_LOSE;
        }
        else if (opponent == OPPONENT_PAPER)
        {
            if (player == PLAYER_ROCK) return RESULT_LOSE;
            if (player == PLAYER_PAPER) return RESULT_DRAW;
            if (player == PLAYER_SCISSORS) return RESULT_WIN;
        }
        else if (opponent == OPPONENT_SCISSOR)
        {
            if (player == PLAYER_ROCK) return RESULT_WIN;
            if (player == PLAYER_PAPER) return RESULT_LOSE;
            if (player == PLAYER_SCISSORS) return RESULT_DRAW;
        }

        throw new ArgumentOutOfRangeException();
    }



    int PointsFromTool(string pick)
    {
        switch (pick.ToUpper())
        {
            case "A":
            case "X": return 1;
            case "B":
            case "Y": return 2;
            case "C":
            case "Z": return 3;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}