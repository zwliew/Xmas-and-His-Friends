using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MazeTileController : MonoBehaviour {
    public int serialNumber = 0;
    private Rigidbody rgdBody;
    private Renderer rend;
    private NavMeshObstacle obs;
	// Use this for initialization
	void Start () {
        rgdBody = GetComponentInChildren<Rigidbody>();
        rend = GetComponentInChildren<Renderer>();
       // rgdBody.useGravity = false;
        rgdBody.isKinematic = false;
        obs = GetComponent<NavMeshObstacle>();
        obs.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (serialNumber == 0)
        {
            yield return new WaitForSeconds(2f);
            rend.material.color = Color.red;
            rgdBody.useGravity = true;
            obs.enabled = true;
            Debug.Log(other.ClosestPointOnBounds(transform.position).z);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            rgdBody.detectCollisions = false;
            yield return new WaitForSeconds(5f);
        }
        if (other.name == "Mas1")
        {
            if (other.GetComponent<ModelInfo>().count < serialNumber)
            {
                yield return new WaitForSeconds(2f);
                rend.material.color = Color.red;
                rgdBody.useGravity = true;
                obs.enabled = true;
                Debug.Log(other.ClosestPointOnBounds(transform.position).z);
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.GetComponent<Rigidbody>().useGravity = true;
                rgdBody.detectCollisions = false;
                yield return new WaitForSeconds(5f);
            }
            else
            {
                 other.GetComponent<ModelInfo>().count = serialNumber + 1;
                Debug.Log("Xmas count is equal to: " + other.GetComponent<ModelInfo>().count);
            }
        }
        //rgdBody.useGravity = false;
    }


}
