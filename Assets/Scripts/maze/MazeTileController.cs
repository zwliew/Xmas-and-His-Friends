using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MazeTileController : MonoBehaviour {

    private Rigidbody rgdBody;
    private Renderer rend;
    private NavMeshObstacle obs;
	// Use this for initialization
	void Start () {
        rgdBody = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rgdBody.useGravity = false;
        rgdBody.isKinematic = false;
        obs = GetComponentInParent<NavMeshObstacle>();
        Debug.Log(obs.name);
        obs.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (other.name == "Mas1") {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
        rend.material.color = Color.red;
        rgdBody.useGravity = true;
        obs.enabled = true;
        Debug.Log(other.ClosestPointOnBounds(transform.position).z);
        other.GetComponent<NavMeshAgent>().enabled = false;
        other.GetComponent<Rigidbody>().useGravity = true;
        rgdBody.detectCollisions = false;
        yield return new WaitForSeconds(2f);
        rgdBody.useGravity = false;
        rgdBody.isKinematic = true;
    }


}
