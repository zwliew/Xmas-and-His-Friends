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
    private GameObject gorakutile;
    public GameObject trees; 
	// Use this for initialization
	void Start () {
        rgdBody = GetComponentInChildren<Rigidbody>();
        gorakutile = GetComponentInChildren<Identifier>().gameObject;
        gorakutile.SetActive(false);
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
        Debug.Log("name is " + other.name);
        if (serialNumber == 0)
        {
            yield return new WaitForSeconds(0.3f);
            rend.material.color = Color.red;
            rgdBody.useGravity = true;
            obs.enabled = true;
            Debug.Log(other.ClosestPointOnBounds(transform.position).z);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            rgdBody.detectCollisions = false;
            if (other.name == "Mas1(Clone)")
            {
                ogm.SendMessage("Result", false);
            }
            yield return new WaitForSeconds(5f);
           
        }
        if (other.name == "Mas1(Clone)")
        {
            if (other.GetComponent<ModelInfo>().count < serialNumber)
            {
                yield return new WaitForSeconds(0.3f);
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
                rgdBody.gameObject.SetActive(false);
                gorakutile.SetActive(true);
                Instantiate(trees);
                trees.SetActive(true);
                trees.transform.position = new Vector3(this.transform.position.x - 0.9f, this.transform.position.y, this.transform.position.z +0.37f);
                 if(serialNumber == dataController.length)
                {
                    ogm.SendMessage("Result", true);
                }
            }
        }
        //rgdBody.useGravity = false;
    }


}
