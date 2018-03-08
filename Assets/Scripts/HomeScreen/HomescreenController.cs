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
        foreach (string itemName in equippedItemNames)
        {
            GameObject equippedGameObject = equippedGameObject.Find(itemName);
            equippedGameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
