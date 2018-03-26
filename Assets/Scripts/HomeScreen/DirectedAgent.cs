using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirectedAgent : MonoBehaviour {

    private NavMeshAgent agent;
	// Use this for initialization
	void Awake () {
        agent = GetComponent<NavMeshAgent>();

	}
	
    public void MoveToLocation(Vector3 targetLocation)
    {
        agent.SetDestination(targetLocation);
        agent.isStopped = false;
    }
}
