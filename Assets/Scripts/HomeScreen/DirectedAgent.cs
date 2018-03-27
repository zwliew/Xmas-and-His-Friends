using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirectedAgent : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject mas;
	// Use this for initialization
	void Awake () {
        agent = GetComponent<NavMeshAgent>();
	}
	

    public IEnumerator MoveToLocation(Vector3 targetLocation)
    {
        Vector3 dir = targetLocation - mas.transform.position;
        Quaternion turn = Quaternion.LookRotation(dir);
       
        for (int i = 30; i > 0; i--)
        {
            Vector3 rotation = Quaternion.Slerp(mas.transform.rotation, turn, Time.deltaTime).eulerAngles;
            mas.transform.rotation = Quaternion.Euler(0f, rotation.y*Time.deltaTime/30, 0f);
            mas.transform.Rotate(0f, rotation.y, 0f);
            yield return new WaitForFixedUpdate();
        }
        //mas.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        agent.SetDestination(targetLocation);
        agent.isStopped = false;
        Debug.Log("move to location is called");

    }
}
