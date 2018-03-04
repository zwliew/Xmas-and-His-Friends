using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is copied from online and should navigate Xmas through the waypoints
 */
public class Movement1 : MonoBehaviour {
	
	[HideInInspector]
	public float delay = 0f;

	public GameObject elevatorUp;
	public GameObject elevatorDown;

	[HideInInspector]
	public bool goingUp;
	[HideInInspector]
	public bool navAgentOn;

	private Vector3 v3elevatorUp;
	private Vector3 v3elevatorDown;

	private UnityEngine.AI.NavMeshAgent navAgent;

	private Rigidbody rbXmas;
	private int stepCountOne = 0;
	private int stepCountTwo = 0;

	private List<Vector3> v3destinations;
	private int currentPoint;

	private AnimationController xmasAnimator;

	void Awake(){
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		xmasAnimator = GetComponent<AnimationController> ();
		rbXmas = GetComponent<Rigidbody> ();
		v3elevatorUp = elevatorUp.transform.position;
		v3elevatorDown = elevatorDown.transform.position;
		goingUp = true;
		navAgentOn = true;
	}

	void Start(){
	}

	public void SetDestinations(List<Vector3> deses){ 
		//Debug.Log ("SetDestination Called");
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		navAgent.updateRotation = true;
		StopAllCoroutines ();
		currentPoint = -1;

		v3destinations = deses;

		for (int i = 1; i < v3destinations.Count; i++) {

			Debug.DrawLine (v3destinations [i - 1], v3destinations [i], Color.red, 2f);
		}
			
		StartCoroutine (Move ());
	}

	IEnumerator Move(){
		xmasAnimator.SetMovingState(1);
		yield return NextWayPoint ();
	}

	IEnumerator WaitForDestination(){//wait for Xmas to reach the current destination
		yield return new WaitForEndOfFrame ();

		while (navAgent.pathPending) {//When the path is being computed, wait
			
			yield return null;

		}
		yield return new WaitForEndOfFrame();

		float remain = navAgent.remainingDistance;
		while(remain == Mathf.Infinity || remain - navAgent.stoppingDistance > float.Epsilon
			|| navAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete){//cannot reach||did not reach||haven't reached

			remain = navAgent.remainingDistance;

			yield return null;
		}

	}

	IEnumerator NextWayPoint(){
		currentPoint++;
		if (currentPoint >= v3destinations.Count) {
			xmasAnimator.SetMovingState (0);
			StopAllCoroutines ();
		} else {
			Vector3 v3next = v3destinations [currentPoint];
			if (v3next.Equals (v3elevatorUp) && goingUp) {
				navAgent.enabled = false;
				navAgentOn = false;
				yield return StartCoroutine (GoUp());
				navAgent.enabled = true;
				navAgentOn = true;
			} else {
				if (v3next.Equals (v3elevatorDown) && !goingUp) {
					navAgent.enabled = false;
					navAgentOn = false;
					yield return StartCoroutine (GoBackward());
					navAgent.enabled = true;
					navAgentOn = true;
				} else {
					navAgent.SetDestination (v3next);
				}
			}
		}
		yield return StartCoroutine (WaitForDestination ());
		StartCoroutine (NextWayPoint ());
	}

	IEnumerator GoUp(){
		while(stepCountOne < 50) {
			rbXmas.rotation =Quaternion.Lerp(rbXmas.rotation, Quaternion.Euler (new Vector3 (0f, 90f, 0f)), 0.2f);
			stepCountOne += 1;
			rbXmas.MovePosition (transform.position + new Vector3(0f, 0.5f, 0f));
			yield return new WaitForFixedUpdate ();
		}
		stepCountOne = 0;
		yield return StartCoroutine(GoForward ());
	}

	IEnumerator GoForward(){
		while (stepCountTwo < 10) {
			stepCountTwo += 1;
			rbXmas.MovePosition(transform.position + new Vector3(0.5f, 0f, 0f));
			yield return new WaitForFixedUpdate ();
		}
		stepCountTwo = 0;
	}
	
	IEnumerator GoBackward(){
		while (stepCountTwo < 10) {
			stepCountTwo += 1;
			rbXmas.MovePosition(transform.position + new Vector3(-0.5f, 0f, 0f));
			yield return new WaitForFixedUpdate ();
		}
		stepCountTwo = 0;
		yield return StartCoroutine(GoDown());
	}

	IEnumerator GoDown(){
		while(stepCountOne < 50) {
			rbXmas.rotation =Quaternion.Lerp(rbXmas.rotation, Quaternion.Euler (new Vector3 (0f, 90f, 0f)), 0.2f);
			stepCountOne += 1;
			rbXmas.MovePosition (transform.position + new Vector3(0f, -0.5f, 0f));
			yield return new WaitForFixedUpdate ();
		}

		stepCountOne = 0;
	}
}
