using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public float defaultVolume = 0.5f; // 기본 볼륨 값 (0.5 = 50%)
    public Slider volumeSlider; // 볼륨을 조절할 슬라이더

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolume; // 초기 볼륨 설정

        volumeSlider = GameObject.FindWithTag("VolumeSlider").GetComponent<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = defaultVolume * 100; // 슬라이더의 값을 볼륨에 맞춰 설정 (0.5 * 100 = 50)
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged); // 슬라이더 값이 변경될 때 호출될 메서드 등록
        }
        else
        {
            Debug.LogWarning("VolumeSlider를 찾을 수 없습니다.");
        }
    }

    private void OnVolumeSliderChanged(float value)
    {
        float volume = value / 100f; // 슬라이더의 값을 0~1 사이로 변환
        audioSource.volume = volume;
    }
}
