using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroudMusic : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip[] bgmClips;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySceneBGM(int bgmNumber)
    {
        audioSource.Stop();
        audioSource.clip = bgmClips[bgmNumber];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene loaded: " + scene.name);
        switch (scene.name)
        {
            case "HomeScreen":
                PlaySceneBGM(0);
                break;
            case "Intro":
                PlaySceneBGM(0);
                break;
            case "NewPinZiGame":
                PlaySceneBGM(1);
                break;
        }
    }

}
