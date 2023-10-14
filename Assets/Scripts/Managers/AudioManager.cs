using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioA;
    public AudioClip audioB;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioA;
        audioSource.Play();
    }

    void Update()
    {
        // 检查音频是否已经播放完毕
        if (!audioSource.isPlaying)
        {
            // 切换到B段落并循环播放
            audioSource.clip = audioB;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
