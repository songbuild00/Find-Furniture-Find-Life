using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public float defaultVolume = 0.5f; // �⺻ ���� �� (0.5 = 50%)
    public Slider volumeSlider; // ������ ������ �����̴�

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolume; // �ʱ� ���� ����

        volumeSlider = GameObject.FindWithTag("VolumeSlider").GetComponent<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = defaultVolume * 100; // �����̴��� ���� ������ ���� ���� (0.5 * 100 = 50)
            volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged); // �����̴� ���� ����� �� ȣ��� �޼��� ���
        }
        else
        {
            Debug.LogWarning("VolumeSlider�� ã�� �� �����ϴ�.");
        }
    }

    private void OnVolumeSliderChanged(float value)
    {
        float volume = value / 100f; // �����̴��� ���� 0~1 ���̷� ��ȯ
        audioSource.volume = volume;
    }
}
