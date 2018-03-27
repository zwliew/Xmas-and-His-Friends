using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class RaycastDestinationSetter2 : MonoBehaviour {

	public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
	public float weaponRange = 500f;                                     // Distance in Unity units over which the player can fire
                                         
    public DirectedAgent directedAgent;
	public Camera fpsCam;                                              // Holds a reference to the first person camera
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
                                     // Reference to the audio source which will play our shooting sound effect                                  // Reference to the LineRenderer component which will display our laserline
	private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing

	void Start () 
	{
	}


	void Update () 
	{
		// Check if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetMouseButtonDown(0) && Time.time > nextFire) 
		{
			// Update the time when our player can fire next
			nextFire = Time.time + fireRate;

			// Start our ShotEffect coroutine to turn our laser line on and off
			StartCoroutine (ShotEffect());

			// Create a vector at the center of our camera's viewport
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Set the start position for our visual effect for our laser to the position of gunEnd


            // Check if our raycast has hit anything
            if (Physics.Raycast (ray,  out hit, weaponRange, 1 << LayerMask.NameToLayer("Road")))
			{
                // Set the end position for our laser line 
                Debug.Log(hit.point);
                StartCoroutine(directedAgent.MoveToLocation(hit.point));
                
			}
			else
			{
				// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
			}
		}
	}


	private IEnumerator ShotEffect()
	{

		// Turn on our line renderer

		//Wait for .07 seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
	}
}