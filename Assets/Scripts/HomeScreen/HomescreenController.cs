using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomescreenController : MonoBehaviour {

    private PlayerDataController playerDataController;
    private List<string> equippedItemNames;

    // Use this for initialization
    void Start ()
    {
        playerDataController = GameObject.FindGameObjectWithTag("Persistent")
            .GetComponent<PlayerDataController>();
        PlayerData playerData = playerDataController.GetPlayerData();
        equippedItemNames = playerData.equippedItems;
        Debug.Log("No. of equipped itemNames when HomeScreen is loaded: "+equippedItemNames.Count);
        foreach (string itemName in equippedItemNames)
        {
			Debug.Log("name of equipped items when HomeScreen is loaded: "+itemName);
            GameObject equippedGameObject = GameObject.Find(itemName);
            if (equippedGameObject != null) {
                equippedGameObject.transform.position = new Vector3(0, 0, 0);
				EditorModeItem itemScript =
					equippedGameObject.GetComponent<EditorModeItem> ();//The editor Mode Item script is attached to buttons not gameObjects!
				if(itemScript != null){
					Debug.Log (itemScript);
					itemScript.isSelected = true;
				}else{
					Debug.LogError ("did not find the itemScript, The editor Mode Item script is attached to buttons not gameObjects!");
				}
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
