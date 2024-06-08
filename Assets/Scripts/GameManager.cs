using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private bool started = false;
    private string currentStage;

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
        
    }
    
    GameStage FindStageByName(string name)
    {
        foreach (var stage in stages)
        {
            if (stage.stageName == name) return stage;
        }
        return null;
    }

    public void ToggleConditionTextUI()
    {
        Debug.Log("Toggle Condition!");
        if (!started) return;
        conditionTextUI.SetActive(!conditionTextUI.activeSelf);
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
        LoadSceneWithFade("Scenes/GameScene");
    }
    
    public void StopGameAndCheckConditions()
    {
        started = false;
        conditionTextUI.SetActive(false);
        shopUI.SetActive(false);
        GameStage stage = FindStageByName(currentStage);
        if (stage != null) {
            // 조건 확인 후 완료 시, 다음 스테이지로 이동
            if (stage.CheckAllConditions())
            {
                // 클리어 UI
                Debug.Log("Clear!");
            }
            else
            {
                // 실패
                Debug.Log("No Clear!");
            }
        }
    }

    public void StartGameButton()
    {
        LoadSceneWithFade("Scenes/HomeScene");
    }

    public void EnableStageViewUI()
    {
        stageViewUI.SetActive(true);
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
}
