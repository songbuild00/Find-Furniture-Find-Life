using System;
using TMPro;
using UnityEngine;

public class GameEndUIManager : MonoBehaviour
{
    public TMP_Text ment;
    public StarRating starRating;
    public TMP_Text scoreText;
    public TMP_Text coinText;

    public void UISetting(float score, int coin)
    {
        gameObject.SetActive(true);
        if (score == 5.0)
        {
            ment.text = "Perfect!";
        } 
        else
        {
            ment.text = "Bad!";
        }
        scoreText.text = Math.Round(score, 1) + " / 5.0";
        starRating.UpdateStars(score);
        coinText.text = coin + "";
    }
}