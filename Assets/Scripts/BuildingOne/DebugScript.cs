using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//GetComponent<Renderer>().material.SetTexture ("_MainTex", (Texture2D)listTextures[2]);  //Set texture

public class DebugScript : MonoBehaviour{
    private AudioSource source;
    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Audio/" + Random.Range(1, 5).ToString() + ".mp3");
            source.PlayOneShot(clip);
        }
        
    }

}