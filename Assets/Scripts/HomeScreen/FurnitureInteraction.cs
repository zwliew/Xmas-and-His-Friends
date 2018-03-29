using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInteraction : MonoBehaviour {

    private AudioSource source;
    private int clickCount;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        clickCount = 0;
    }

    public void Update()
    {
        // Return early if nothing is being pressed
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        // Return early if the previous audio source is still playing
        if (source == null || source.isPlaying)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer("Furniture")))
        {
            // Load AudioClip from Sounds/Furniture/<audioClipName>
            string soundName = "";
            if (hitInfo.collider.gameObject.transform.parent != null)
            {
                soundName = hitInfo.collider.gameObject.transform.parent.name;
            }
            else
            {
                soundName = hitInfo.collider.gameObject.name;
            }
            Debug.Log("Trying to play sound: " + soundName);
            AudioClip clip = Resources.Load<AudioClip>(
                "Sounds/Furniture/" + soundName);
            if (source != null && clip != null)
            {
                source.PlayOneShot(clip);
                clickCount = 0;
            }
            else
            {
                Debug.Log("didnot get the clip: " + soundName);
                clickCount += 1;
                if (clickCount > 4)
                {
                    Debug.Log("clicking too many times, stopping sound");
                    source.Stop();
                    clickCount = 0;
                }
            }
        }
        else {
            clickCount += 1;
            if (clickCount > 4)
            {
                Debug.Log("clicking too many times, stopping sound");
                source.Stop();
                clickCount = 0;
            }
        }
    }
    public void ResetClickCount()
    {
        clickCount = 0;
    }
}
