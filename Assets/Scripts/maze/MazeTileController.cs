using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MazeTileController : MonoBehaviour {
    public int serialNumber = 0;
    private Rigidbody rgdBody;
    private Renderer rend;
    private NavMeshObstacle obs;
    private MazeDataController dataController;
    private OverallGameManager ogm;
    public GameObject gameManager;
	// Use this for initialization
	void Start () {
        rgdBody = GetComponentInChildren<Rigidbody>();
        rend = GetComponentInChildren<Renderer>();
       // rgdBody.useGravity = false;
        rgdBody.isKinematic = false;
        obs = GetComponent<NavMeshObstacle>();
        obs.enabled = false;
        dataController = gameManager.GetComponent<MazeDataController>();
        ogm = gameManager.GetComponent<OverallGameManager>();
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
            if (other.name == "Mas1")
            {
                ogm.SendMessage("Result", false);
            }
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
                ogm.SendMessage("Result", false);
                rgdBody.detectCollisions = false;
                yield return new WaitForSeconds(5f);
            }
            else if(other.GetComponent<ModelInfo>().count == serialNumber)
            {
                 other.GetComponent<ModelInfo>().count = serialNumber + 1;
                 Debug.Log("Xmas count is equal to: " + other.GetComponent<ModelInfo>().count);
                 if(serialNumber == dataController.length  )
                {
                    ogm.SendMessage("Result", true);
                }
            }
        }
        //rgdBody.useGravity = false;
    }


}
