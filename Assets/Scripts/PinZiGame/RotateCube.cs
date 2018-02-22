using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * Currently, I need to combine this script to the GameController script
 * But I don't know what to do
 */
public class RotateCube : MonoBehaviour {

	public GameObject goTetra;
	public GameObject[] goNodes;//The vertex of the tetrahedron
	public GameObject goViewPoint;//is actually used to determine a point at the back
	public GameObject goBottomPoint;

	private Rigidbody rbTetra;
	private Animator animatorTetra;

	private bool isDragging;
	private bool isPlaying;

	private Vector3[] v3Nodes = new Vector3[4];
	private float[] fDistances = new float[4];
	private Vector3 v3ViewPointPosition;
	private Vector3 v3BottomPosition;

	void Start(){
		isPlaying = true;
		rbTetra = goTetra.GetComponent<Rigidbody> ();	
		animatorTetra = goTetra.GetComponent<Animator> ();
		//animatorTetra.updateMode = AnimatorUpdateMode.Normal; 
		//animatorTetra.Play ("TetrahedronRotateAnimation");
		v3ViewPointPosition = goViewPoint.transform.position;
		for (int i = 0; i < 4; i++) {
			v3Nodes [i] = goNodes [i].transform.position;
		}
	}


	void FixedUpdate(){
		if (isPlaying) {
			
			if (Input.GetMouseButton (0)) {//Hold mouse left btn to drag
				animatorTetra.updateMode = AnimatorUpdateMode.AnimatePhysics;
				isDragging = true;
			} else {
				isDragging = false;
			}

			if (isDragging) {//To achieve rotation
				//---------Copied from online---------------
				Vector3 speed = Vector3.zero;
				Vector3 avgSpeed = Vector3.zero;
				speed = new Vector3 (-Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
				avgSpeed = Vector3.Lerp (avgSpeed, speed, Time.deltaTime * 5);
				float i = Time.deltaTime;
				speed = Vector3.Lerp (speed, Vector3.zero, i);
				//---------Copied from online---------------

				rbTetra.AddTorque (new Vector3 (speed.y * 8f, speed.x * 8f, 0f));

			} else {//To achieve snapping effect
				
				for (int i = 0; i < 4; i++) {//Find which one is at the most back
					v3Nodes [i] = goNodes [i].transform.position;
					fDistances [i] = (v3ViewPointPosition - v3Nodes [i]).magnitude;
				}

				float fMaxDistance = Mathf.Max (fDistances);

				for (int i = 0; i < 4; i++) {
					if (fDistances [i] == fMaxDistance) { //Find the vertex to be placed at the furthest
						float fForceFactor = Mathf.Exp (-fDistances [i]) * 200f; //A simple damping
						rbTetra.AddForceAtPosition (//Place the vertex at the furthest
							(v3ViewPointPosition - v3Nodes [i] + new Vector3 (0f, 0f, 5f)) * fForceFactor,
							goNodes [i].transform.position,
							ForceMode.Acceleration);
						
						float fBottomDistance = (goNodes [(i + 1) % 4].transform.position - goBottomPoint.transform.position).magnitude;
						fForceFactor = Mathf.Exp (-fDistances [i]) * 200f;
						rbTetra.AddForceAtPosition (//Place the vertex to the bottom, So that the character displayed is not upsideDown
							(new Vector3 (0f, -1f, 0f)) * fForceFactor,
							goNodes [(i + 1) % 4].transform.position,
							ForceMode.Acceleration);
					} else {
						//Debug.LogError ("None of the vertexes are this far from the back");
					}

				}
			}
		}
	}

	public void PlayRotateAnimation(){
		StartCoroutine (RotateAnimation ());
	}

	IEnumerator RotateAnimation(){
		isPlaying = false;

		for (int i = 0; i < 100; i++) {
			rbTetra.AddTorque (new Vector3 (0f, 5f, 0f)*Mathf.Abs(i-50), ForceMode.Impulse);
			yield return new WaitForFixedUpdate ();
		}

		isPlaying = true;
	}
}
