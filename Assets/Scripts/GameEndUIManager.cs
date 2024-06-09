using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUIManager : MonoBehaviour
{
    public TMP_Text ment;
    public StarRating starRating;
    public TMP_Text scoreText;
    public TMP_Text coinText;

    public Button replayButton;
    public Button homeButton;
    public Button nextButton;

    private float score;

    public void UISetting(float score, int coin)
    {
        this.score = score;
        gameObject.SetActive(true);
        if (score == 5.0)
        {
            ment.text = "Perfect!";
            nextButton.interactable = true;
        } 
        else
        {
            ment.text = "Bad!";
            nextButton.interactable = false;
        }
        scoreText.text = Math.Round(score, 1) + " / 5.0";
        starRating.UpdateStars(score);
        coinText.text = coin + "";
        GameManager.Instance.AddCoin(coin);

        replayButton.onClick.AddListener(() => OnReplayButtonClick());
        homeButton.onClick.AddListener(() => OnHomeButtonClick());
        nextButton.onClick.AddListener(() => OnNextButtonClick());
    }

    private void OnReplayButtonClick()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    private void OnHomeButtonClick()
    {
        if (score == 5.0)
        {
            GameManager.Instance.NextStage();
        }
        GameManager.Instance.StartGameButton();
        gameObject.SetActive(false);
    }

    private void OnNextButtonClick()
    {
        if (score == 5.0)
        {
            GameManager.Instance.NextStage();
        }
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
}