using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CalculateWinnerStrategy
{
    public abstract GameResult ReturnRoundWinner(int player_number, int dealer_number);
}

public class CW_Default : CalculateWinnerStrategy
{
    public override GameResult ReturnRoundWinner(int player_number, int dealer_number)
    {
        if (player_number > dealer_number) return GameResult.Win;
        else return GameResult.Lose;
    }
}

public class CW_Equal : CalculateWinnerStrategy
{
    public override GameResult ReturnRoundWinner(int player_number, int dealer_number)
    {
        if (player_number >= dealer_number) return GameResult.Win;
        else return GameResult.Lose;
    }
}