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

	private List<Vector3> v3destinations = new List<Vector3> ();
	int currentPoint;


	void Awake(){
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		currentPoint = 0;
	}

	public void SetDestinations(List<Vector3> deses){ 
		v3destinations = deses;
		currentPoint = 0;
		StartCoroutine (Move ());
	}

	IEnumerator Move(){
		navAgent.updateRotation = true;
		navAgent.SetDestination (v3destinations [currentPoint]);
		Debug.Log ("break point one");
		yield return StartCoroutine (WaitForDestination ());
		StartCoroutine (NextWayPoint ());
	}

	IEnumerator WaitForDestination(){//wait for Xmas to reach the current destination
		yield return new WaitForEndOfFrame ();
		Debug.Log ("break point two");
		while (navAgent.pathPending) {//When the path is being computed, wait
			yield return null;
			Debug.Log ("break point three");
		}
		yield return new WaitForEndOfFrame();
		Debug.Log ("break point four");
		Debug.Log (GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled);

		while (!GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled) {
			yield return null;
			Debug.Log ("disabled nav agent one");

		}

		Debug.Log ("break point five");
		float remain = navAgent.remainingDistance;
		while(remain == Mathf.Infinity || remain - navAgent.stoppingDistance > float.Epsilon
			|| navAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete){//cannot reach||reached||haven't reached
			while (!GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled) {
				yield return new WaitForSeconds(1f);
				Debug.Log ("disabled nav agent two");
			}
			Debug.Log ("nav agent two resumed");
			remain = navAgent.remainingDistance;
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
