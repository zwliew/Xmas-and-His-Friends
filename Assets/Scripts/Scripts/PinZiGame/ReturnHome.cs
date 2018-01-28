using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour {
    public string sceneName = "TestScene";
	// Use this for initialization
	public void returnHome()
    {
        SceneManager.LoadScene(sceneName);
    }
}
