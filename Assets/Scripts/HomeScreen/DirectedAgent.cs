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
        Vector3 rotation = Quaternion.Slerp(mas.transform.rotation, turn, Time.deltaTime*40f).eulerAngles;
        mas.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        int difference = (int)Mathf.Abs(rotation.y - mas.transform.rotation.y);
        for (int i = difference; i > 0; i--)
        {
            mas.transform.Rotate(0f, rotation.y*(1/difference), 0f);
            yield return new WaitForSeconds(0.3f / difference);
        }
        //mas.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        yield return new WaitForSeconds(0.3f);
        agent.SetDestination(targetLocation);
        agent.isStopped = false;
        Debug.Log("move to location is called");

    }
}
