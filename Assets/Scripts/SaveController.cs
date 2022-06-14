using System;
using UnityEngine;

internal class SaveController
{
    static string ScoreSuffix = "_score";
    internal static int GetScore(CardType cardType)
    {
        return PlayerPrefs.GetInt(cardType.ToString() + ScoreSuffix, 0);
    }
    internal static void SetScore(CardType cardType, int scoreValue)
    {
        PlayerPrefs.SetInt(cardType.ToString() + ScoreSuffix, scoreValue);
    }
    internal static void ResetScores()
    {
        PlayerPrefs.DeleteAll();
    }
}