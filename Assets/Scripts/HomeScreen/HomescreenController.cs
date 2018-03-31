using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomescreenController : MonoBehaviour {

    private PlayerDataController playerDataController;
    private List<string> equippedItemNames;

	public List<GameObject> furnitures;
    // Use this for initialization
    void Start ()
    {
        playerDataController = GameObject.FindGameObjectWithTag("Persistent")
            .GetComponent<PlayerDataController>();
        PlayerData playerData = playerDataController.GetPlayerData();
        equippedItemNames = playerData.equippedItems;
		foreach (string thisItemName in equippedItemNames) {
			Debug.Log ("(HomeScreenController)"+thisItemName);
		}


		foreach (GameObject thisFurniture in furnitures) {
			thisFurniture.SetActive (false);
		}

        foreach (string itemName in equippedItemNames)
        {
			Debug.Log("name of equipped items when HomeScreen is loaded: "+itemName);

			foreach(GameObject thisFurniture in furnitures){
				if (thisFurniture.name.Equals (itemName)) {
					Debug.Log ("Setting furniture to equipped");
					thisFurniture.SetActive (true);
				}
			}
		}
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
