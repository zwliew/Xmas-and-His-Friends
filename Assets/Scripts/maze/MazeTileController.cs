using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MazeTileController : MonoBehaviour {

    private Rigidbody rgdBody;
    public NavMeshAgent xmas;
    private Rigidbody rgdXmas;
    private Renderer rend;
    private NavMeshObstacle obs;
	// Use this for initialization
	void Start () {
        rgdBody = GetComponent<Rigidbody>();
        rgdXmas = xmas.gameObject.GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        Debug.Log("it is enabled" + !xmas.isActiveAndEnabled);
        rgdXmas.useGravity = false;
        xmas.gameObject.SetActive(true);
        rgdBody.useGravity = false;
        obs = GetComponentInParent<NavMeshObstacle>();
        Debug.Log(obs.name);
        obs.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator OnTriggerStay(Collider other)
    {

        yield return new WaitForSeconds(5f);

        rend.material.color = Color.red;
        rgdBody.useGravity = true;
        obs.enabled = true;
        Debug.Log(other.ClosestPointOnBounds(transform.position).z);
        if ((other.ClosestPointOnBounds(transform.position) - transform.position).magnitude < 0.7)
        {
            xmas.enabled = false;
            rgdXmas.useGravity = true;
            rgdBody.detectCollisions = false;
        }

        yield return new WaitForSeconds(2f);
        rgdBody.useGravity = false;
        rgdBody.isKinematic = true;
    }


}
