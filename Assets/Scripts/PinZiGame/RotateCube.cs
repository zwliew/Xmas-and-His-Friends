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

	public float fForceConstant;

	private Rigidbody rbTetra;
	private Animator animatorTetra;

	private bool isDragging;
	private bool isPlaying;

	private Vector3[] v3Nodes = new Vector3[4];
	private float[] fDistances = new float[4];
	private Vector3 v3ViewPointPosition;
	private Vector3 v3BottomPosition;

	private Vector3 touchStartPosition;
	private Vector3 touchEndPosition;

	private Vector3 touchRealTimePosition;

	void Start(){
		isPlaying = true;
		rbTetra = goTetra.GetComponent<Rigidbody> ();	
		animatorTetra = goTetra.GetComponent<Animator> ();
		//animatorTetra.updateMode = AnimatorUpdateMode.Normal; 
		//animatorTetra.Play ("TetrahedronRotateAnimation");
		animatorTetra.updateMode = AnimatorUpdateMode.AnimatePhysics;
		v3ViewPointPosition = goViewPoint.transform.position;
		for (int i = 0; i < 4; i++) {
			v3Nodes [i] = goNodes [i].transform.position;
		}
	}


	void FixedUpdate(){
		if (isPlaying) {
			#if UNITY_ANDROID			
			if(Input.touchCount>0){
				Touch touch0 = Input.GetTouch(0);
				switch(touch0.phase){
				case TouchPhase.Began:
					touchStartPosition = touch0.position;
			 		touchRealTimePosition = touch0.position;
					break;
				case TouchPhase.Moved:

					Vector3 speed = Vector3.zero;

					speed = Vector3.Lerp(speed, touch0.deltaPosition, 0.1f);

					if(touch0.deltaPosition.magnitude > 0.1f){
						rbTetra.AddTorque (new Vector3 (speed.y * 0.5f * fForceConstant,-speed.x * 0.5f* fForceConstant, 0f));
					}
					if(touch0.deltaPosition.magnitude > 1.1f){
						rbTetra.AddTorque (new Vector3 (speed.y * 0.5f * fForceConstant,-speed.x * 0.5f* fForceConstant, 0f));
					}
					if(touch0.deltaPosition.magnitude > 10f){
						rbTetra.AddTorque (new Vector3 (speed.y * 0.5f * fForceConstant,-speed.x * 0.5f* fForceConstant, 0f));
					}
			
					break;
				case TouchPhase.Ended:
					touchEndPosition = touch0.position;
					if((touchEndPosition-touchStartPosition).magnitude< 10f){
						GameObject.Find("GameController").GetComponent<GameController>().ClickedHere(touchStartPosition);
					}
					break;
				case TouchPhase.Stationary:
					break;
				}
			}



			#endif

			#if UNITY_EDITOR
			if(Input.GetMouseButtonDown(0))
				touchRealTimePosition = Input.mousePosition;
			if (Input.GetMouseButton (0)) {//Hold mouse left btn to drag
			isDragging = true;
			} else {
			isDragging = false;
			}

			if (isDragging) {//To achieve rotation
				//---------Copied from online---------------
				Vector3 touchDeltaPosition = touchRealTimePosition - Input.mousePosition;
				Vector3 speed = Vector3.zero;
				Vector3 avgSpeed = Vector3.zero;
				speed = Vector3.Lerp(speed, touchDeltaPosition, 0.1f);
				//speed = new Vector3 (-Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
				//avgSpeed = Vector3.Lerp (avgSpeed, speed, Time.deltaTime * 5);
				//float i = Time.deltaTime;
				//speed = Vector3.Lerp (speed, Vector3.zero, i);
				//---------Copied from online---------------

				rbTetra.AddTorque (new Vector3 (-speed.y * 1f * fForceConstant, speed.x * 1f* fForceConstant, 0f));
				touchRealTimePosition =  Input.mousePosition;
			} 
			#endif

			#if UNITY_STANDALONE
			if(Input.GetMouseButtonDown(0))
				touchRealTimePosition = Input.mousePosition;
			if (Input.GetMouseButton (0)) {//Hold mouse left btn to drag
				isDragging = true;
			} else {
				isDragging = false;
			}

			if (isDragging) {//To achieve rotation
				//---------Copied from online---------------
				Vector3 touchDeltaPosition = touchRealTimePosition - Input.mousePosition;
				Vector3 speed = Vector3.zero;
				Vector3 avgSpeed = Vector3.zero;
				speed = Vector3.Lerp(speed, touchDeltaPosition, 0.1f);
				//speed = new Vector3 (-Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
				//avgSpeed = Vector3.Lerp (avgSpeed, speed, Time.deltaTime * 5);
				//float i = Time.deltaTime;
				//speed = Vector3.Lerp (speed, Vector3.zero, i);
				//---------Copied from online---------------

				rbTetra.AddTorque (new Vector3 (-speed.y * 1f * fForceConstant, speed.x * 1f* fForceConstant, 0f));
				touchRealTimePosition =  Input.mousePosition;
			} 
			#endif

			if(!isDragging){//To achieve snapping effect
				
				for (int i = 0; i < 4; i++) {//Find which one is at the most back
					v3Nodes [i] = goNodes [i].transform.position;
					fDistances [i] = (v3ViewPointPosition - v3Nodes [i]).magnitude;
				}

				float fMaxDistance = Mathf.Max (fDistances);

				for (int i = 0; i < 4; i++) {
					if (fDistances [i] == fMaxDistance) { //Find the vertex to be placed at the furthest
						float fForceFactor = Mathf.Exp (-fDistances [i]) * 200f; //A simple damping
						rbTetra.AddForceAtPosition (//Place the vertex at the furthest
							(v3ViewPointPosition - v3Nodes [i] + new Vector3 (0f, 0f, 5f)) * fForceFactor* fForceConstant,
							goNodes [i].transform.position,
							ForceMode.Acceleration);
						
						float fBottomDistance = (goNodes [(i + 1) % 4].transform.position - goBottomPoint.transform.position).magnitude;
						fForceFactor = Mathf.Exp (-fDistances [i]) * 200f;
						rbTetra.AddForceAtPosition (//Place the vertex to the bottom, So that the character displayed is not upsideDown
							(new Vector3 (0f, -1f, 0f)) * fForceFactor* fForceConstant,
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
