using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is copied from online and should navigate Xmas through the waypoints
 */
public class Movement1 : MonoBehaviour {
	
	[HideInInspector]
	public float delay = 0f;

	private UnityEngine.AI.NavMeshAgent navAgent;

	private List<Vector3> v3destinations;
	int currentPoint;

	void Awake(){
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

	}

	void Start(){
	}

	public void SetDestinations(List<Vector3> deses){ 
		//Debug.Log ("SetDestination Called");
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		navAgent.updateRotation = true;
		StopAllCoroutines ();
		currentPoint = 0;


		v3destinations = deses;

		for (int i = 1; i < v3destinations.Count; i++) {
			//Debug.Log (v3destinations [i]);
			Debug.DrawLine (v3destinations [i - 1], v3destinations [i], Color.red, 2f);
		}

		//Debug.Log ("Destination set, ready to go" + v3destinations);
		StartCoroutine (Move ());
	}

	IEnumerator Move(){
		navAgent.SetDestination (v3destinations [currentPoint]);
		//Debug.Log ("Move()");
		yield return StartCoroutine (WaitForDestination ());
		StartCoroutine (NextWayPoint ());
	}

	IEnumerator WaitForDestination(){//wait for Xmas to reach the current destination
		yield return new WaitForEndOfFrame ();
		//Debug.Log ("Wait for destination to complete");
		while (navAgent.pathPending) {//When the path is being computed, wait
			//Debug.Log("Finding path");
			yield return null;
			//Debug.Log ("break point three");
		}
		yield return new WaitForEndOfFrame();
		//Debug.Log ("break point four");
		//Debug.Log (GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled);
		/*
		while (!GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled) {
			yield return null;
			//Debug.Log ("disabled nav agent one");

		}
        */
		//Debug.Log ("break point five");
		float remain = navAgent.remainingDistance;
		while(remain == Mathf.Infinity || remain - navAgent.stoppingDistance > float.Epsilon
			|| navAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete){//cannot reach||did not reach||haven't reached
			/*
			while (!GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled) {
				yield return new WaitForSeconds(1f);
				//Debug.Log ("disabled nav agent two");
			}
			*/
			remain = navAgent.remainingDistance;
			//Debug.Log ("nav agent has " + remain + " to go");
			yield return null;
		}

	}

	IEnumerator NextWayPoint(){
		currentPoint++;
		if (currentPoint >= v3destinations.Count) {
			StopAllCoroutines ();
		} else {
			Vector3 v3next = v3destinations [currentPoint];
			navAgent.SetDestination (v3next);
			yield return StartCoroutine (WaitForDestination ());
			StartCoroutine (NextWayPoint ());
		}
	}
}
