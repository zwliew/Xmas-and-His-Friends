using UnityEngine;

public class FurnitureSoundController : MonoBehaviour {

	private AudioSource source;

	public void Start() {
		source = GetComponent<AudioSource>();
	}

	public void Update() {
		// Return early if nothing is being pressed
		if (!Input.GetMouseButtonDown(0)) {
			return;
		}

		// Return early if the previous audio source is still playing
		if (source == null || source.isPlaying) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, 300f, 1 << LayerMask.NameToLayer("Road"))) {
			// Load AudioClip from Sounds/Furniture/<audioClipName>
			AudioClip clip = Resources.Load<AudioClip>(
				"Sounds/Furniture/" + hitInfo.collider.gameObject.name);
			if (source != null && clip != null) {
				source.PlayOneShot(clip);
			}
		}
	}
}