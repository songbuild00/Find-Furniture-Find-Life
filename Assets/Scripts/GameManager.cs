using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<FurnitureModel> furnitureModels;
    public GameObject playerObject;

    public List<GameStage> stages;
    public string startStage;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1.0f;

    public GameObject homeUI;
    public GameObject conditionTextUI;
    public GameObject shopUI;
    public GameObject stageViewUI;
    public GameObject requestDetailsUI;
    public GameObject shopViewUI;
    public GameObject settingViewUI;
    public TMP_Text requestDetailsText;
    public TMP_Text conditionText;
    public GameEndUIManager gameEndUI;
    public TMP_Text coinText;
   

    private bool started = false;
    private string currentStage;
    private int coin = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        started = false;
        currentStage = startStage;

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1;
            StartCoroutine(Fade(0));
        }
    }

    void Update()
    {
        coinText.text = coin + "";
    }

    GameStage FindStageByName(string name)
    {
        foreach (var stage in stages)
        {
            if (stage.stageName == name) return stage;
        }
        return null;
    }

    public void ToggleRequestDetailsUI()
    {
        Debug.Log("Toggle Request Details!");
        requestDetailsUI.SetActive(!requestDetailsUI.activeSelf);
        if (requestDetailsUI.activeSelf)
        {
            GameStage stage = FindStageByName(currentStage);
            requestDetailsText.text = stage.conditionText;
        }
    }

    public void ToggleConditionTextUI()
    {
        Debug.Log("Toggle Condition!");
        if (!started) return;
        conditionTextUI.SetActive(!conditionTextUI.activeSelf);
        if (conditionTextUI.activeSelf)
        {
            GameStage stage = FindStageByName(currentStage);
            conditionText.text = stage.conditionText;
        }
    }

    public void ToggleShopUI()
    {
        Debug.Log("Toggle Shop!");
        if (!started) return;
        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void StartGame()
    {
        started = true;
        homeUI.SetActive(false);
        SceneManager.sceneLoaded += OnLoadGameScene;
        LoadSceneWithFade("Scenes/GameScene");
    }

    private void OnLoadGameScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLoadGameScene;
        GameObject doneButton = GameObject.Find("Done Button");
        if (doneButton != null)
        {
            Button button = doneButton.GetComponent<Button>();
            if (button != null)
            {
                Debug.Log("Button not null! Setting started!");
                button.onClick.AddListener(() => StopGameAndCheckConditions());
            }
        }
    }

    public void StopGameAndCheckConditions()
    {
        started = false;
        conditionTextUI.SetActive(false);
        shopUI.SetActive(false);
        GameStage stage = FindStageByName(currentStage);
        if (stage != null) {
            // 조건 확인 후 완료 시, 다음 스테이지로 이동
            float score = stage.CheckAllConditions();
            if (score == 1.0)
            {
                Debug.Log("Clear!");
            }
            else
            {
                Debug.Log("No Clear!");
            }
            gameEndUI.UISetting(score * 5, score == 1.0 ? 1000 : 0);
        }
    }

    public void StartGameButton()
    {
        LoadSceneWithFade("Scenes/HomeScene");
        homeUI.SetActive(true);
    }

    public void NextStage()
    {
        currentStage = FindStageByName(currentStage).nextStage;
        FindStageByName(currentStage).stageButton.interactable = true;
    }

    public void EnableStageViewUI()
    {
        stageViewUI.SetActive(true);
    }

    public void EnableShopViewUI()
    {
        if (!shopViewUI.activeSelf)
        {
            shopViewUI.SetActive(true);
        }
        else
        {
            shopViewUI.SetActive(false);
        }
    }

    public void EnableSettingViewUI()
    {
        if (!settingViewUI.activeSelf)
        {
            settingViewUI.SetActive(true);
        }
        else
        {
            settingViewUI.SetActive(false);
        }
    }

    public void ClickStage(string stageName)
    {
        currentStage = stageName;
        stageViewUI.SetActive(false);
    }

    public void SpawnFurniture(int index, Vector3 position)
    {
        Debug.Log("Index: " + index);
        if (index >= 0 && index < furnitureModels.Count)
        {
            GameObject spawnedObject = Instantiate(furnitureModels[index].model, position, Quaternion.identity);
            furnitureModels[index].InstantiateSetting(spawnedObject, playerObject);
        }
        else
        {
            Debug.LogWarning("Invalid index!");
        }
    }
    
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1));

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(Fade(0));
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            playerObject.transform.position = spawnPoint.transform.position;
            playerObject.transform.rotation = spawnPoint.transform.rotation;
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }

    public void AddCoin(int coin)
    {
        this.coin += coin;
    }
}
