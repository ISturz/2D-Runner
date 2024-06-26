using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    private AudioSource music;
    public AudioMixer mixer;
    private GameObject[] other;
    private bool NotFirst = false;
    private void Awake()
    {
        other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        music = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic();
        mixer.SetFloat("GameVol", Mathf.Log10(0.5f) * 20);
    }

    public void PlayMusic()
    {
        if (music.isPlaying) return;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }
}