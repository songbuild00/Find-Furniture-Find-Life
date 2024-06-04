using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<FurnitureModel> furnitureModels;
    public GameObject playerObject;

    public SerializableDictionary<string, GameStage> stages;
    public string startStage;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1.0f;

    private bool started = false;
    private string currentStage = "TUTORIAL";

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

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1;
            StartCoroutine(Fade(0));
        }
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        started = true;
        // UI 끄기
        // 오른손에 상점, 왼손에 요청 목록
    }
    
    public void StopGameAndCheckConditions()
    {
        started = false;
        // UI 끄기
        GameStage stage = stages[currentStage];
        if (stage != null) {
            // 조건 확인 후 완료 시, 다음 스테이지로 이동
            if (stage.CheckAllConditions())
            {
                // 클리어 UI
            }
            else
            {
                // 실패
            }
        }
    }

    public void TestSpawnFurniture0() {
        SpawnFurniture(0, playerObject.transform.position + new Vector3(0, 2, -5));
    }

    public void SpawnFurniture(int index, Vector3 position)
    {
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
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(Fade(0));
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
