using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class RoomSelectionAni : MonoBehaviour {

	public GameObject xmasGameObject;
	private NavMeshAgent xmasNavAgent;
	private RoomSelectionXmasAnimationController animationController;

	public GameObject sprfGameObject;
	private NavMeshAgent sprfNavAgent;
	private RoomSelectionSprfAni sprfAnimationController;


	private bool triggerOne;
	public GameObject destinationOne;
	public GameObject startPointOne;
	public GameObject destinationTwo;
	public GameObject startPointTwo;

	//private string nameOfHim;

	private float timeKeeper;
	private string phaseName;

	private RaycastHit hitInfo;

	public CanvasGroup UICvsGrp;

	public AudioClip XmasAudioClip;
	public AudioClip SdrClip;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		UICvsGrp.gameObject.SetActive (false);
		//nameOfHim = xmasGameObject.name;

		xmasNavAgent = xmasGameObject.GetComponent<NavMeshAgent> ();
		sprfNavAgent = sprfGameObject.GetComponent<NavMeshAgent> ();

		animationController = xmasGameObject.GetComponent<RoomSelectionXmasAnimationController> ();
		sprfAnimationController = sprfGameObject.GetComponent<RoomSelectionSprfAni> ();

		timeKeeper = 0f;
		triggerOne = true;
		phaseName = "phaseZero";
	}
	
	// Update is called once per frame
	void Update () {
		timeKeeper += Time.deltaTime;

		if (timeKeeper < 1f) {
			xmasNavAgent.SetDestination (destinationOne.transform.position);
			sprfNavAgent.SetDestination (destinationTwo.transform.position);
			animationController.SetAnimationState (1);
			Debug.Log ("go xmas and sprf");
			phaseName = "phaseOne";
			timeKeeper += 1f;
		}
		//Debug.Log(xmasNavAgent.remainingDistance);
		if (triggerOne && (((xmasNavAgent.remainingDistance >= 0f) && (xmasNavAgent.remainingDistance < 0.5f) 
			&& (sprfNavAgent.remainingDistance >= 0f)&& (sprfNavAgent.remainingDistance < 0.5f) && timeKeeper >1.3f) || (timeKeeper > 5f))) {
			phaseName = "phaseTwo";
			Debug.Log(phaseName);
			animationController.SetAnimationState (0);
			StartCoroutine(FaceTheAudience(xmasGameObject, destinationOne));
			StartCoroutine(FaceTheAudience(sprfGameObject, destinationTwo));
			animationController.SetTrigger ("Stand");
			animationController.SetTrigger ("Wave");
			sprfAnimationController.SetTrigger("Stand");
			triggerOne = false;
		}

		if (phaseName.Equals ("phaseThree")) {
			//Debug.Log (phaseName);
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer ("Furniture"))) {
					if(hitInfo.collider.gameObject.name.Equals("Xmas")){
						Debug.Log ("selected Xmas");
						StartCoroutine (ZoomInCamera (1));
					}else{
						StartCoroutine (ZoomInCamera (2));
					}
				}
			}
		}

		if (phaseName.Equals("phaseFour")){
			if (Input.GetMouseButtonDown (0)) {
				StartCoroutine (ZoomOutCamera ());
			}
		}
			

	}

	IEnumerator FaceTheAudience(GameObject gO, GameObject des){
		for (int i = 0; i < 100; i++) {
			gO.transform.rotation = Quaternion.Lerp (gO.transform.rotation,des.transform.rotation,0.1f);
			yield return null;
		}
		phaseName = "phaseThree";
	}

	IEnumerator ZoomInCamera(int i){
		UICvsGrp.gameObject.SetActive (true);
		Debug.Log (phaseName);
		Animator uiAnimator = UICvsGrp.gameObject.GetComponent<Animator> ();
		if (i == 1) {
			uiAnimator.SetTrigger ("Xmas");
			audioSource.Stop ();
			audioSource.PlayOneShot (XmasAudioClip);
		} else if (i == 2) {
			uiAnimator.SetTrigger ("SpringFes");
			audioSource.Stop ();
			audioSource.PlayOneShot (SdrClip);
		}
		yield return new WaitForSeconds (2f);
		phaseName = "phaseFour";
	}

	IEnumerator ZoomOutCamera(){
		phaseName = "phaseThree";
		for(int ii =0;ii<10;ii++){
			yield return null;

		}
		yield return null;
		UICvsGrp.gameObject.SetActive (false);
	}
}

