using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MazeTileController : MonoBehaviour {

    private Rigidbody rgdBody;
    public NavMeshAgent xmas;
    private Rigidbody rgdXmas;
    private Renderer rend;
	// Use this for initialization
	void Start () {
        rgdBody = GetComponent<Rigidbody>();
        rgdXmas = xmas.gameObject.GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        Debug.Log("it is enabled" + !xmas.isActiveAndEnabled);
        rgdXmas.useGravity = false;
        xmas.gameObject.SetActive(true);
        rgdBody.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator OnTriggerEnter(Collider other)
    {
        Debug.Log("object detected");
        yield return new WaitForSeconds(1f);
        rend.material.color = Color.red;
        Debug.Log("color is " + rend.material.color);
        rgdBody.useGravity = true;
        xmas.enabled = false;
        rgdXmas.useGravity = true;
        rgdBody.detectCollisions = false;
    }
}
