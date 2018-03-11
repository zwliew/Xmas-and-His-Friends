using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/**
 * This is the global data controller for the entire game.
 * This must be attached to a scene (called Persistent) that
 * will be persisted via DontDestroyOnLoad().
 */
public class PlayerDataController : MonoBehaviour
{
    private PlayerData playerData;

    void Awake()
    {
		DontDestroyOnLoad (gameObject);
		PlayerPrefs.DeleteAll ();
        LoadPlayerData();
    }

    public void UpdatePlayerName(string name)
    {
        playerData.name = name;
        SavePlayerData();
    }

    /**
     * Updates the number of coins in the player's inventory
     * given a parameter 'delta'.
     * 
     * If the resulting number of coins is negative, this method
     * returns _false_ and does _not_ update the inventory.
     * Otherwise, it returns _true_.
     */
    private bool UpdatePlayerCoins(int delta)
    {
        int initial = playerData.coins;
        int final = initial + delta;
        if (final < 0)
        {
            return false;
        }
        playerData.coins = final;
        SavePlayerData();
        return true;
    }

    public bool PurchaseShopItem(string name, int cost) {//Use UpdatePurchasedShopItems instead
        bool success = UpdatePlayerCoins(-cost);
        if (!success) {
            return false;
        }
        playerData.purchasedShopItems.Add(name);
        return true;
    }

	public void UpdateEquippedEditorModeItem(List<String> itemNames){
        List<string> newEquippedItems = new List<string>();
        foreach (string itemName in itemNames)
        {
            newEquippedItems.Add(itemName);
        }
		playerData.equippedItems.Clear ();
        playerData.equippedItems = newEquippedItems;
		Debug.Log ("EquippedItems has been updated in PlayerDataController");
		foreach (string str in playerData.equippedItems) {
			Debug.Log ("This item is saved to playerpref as equipped: " + str.ToString());
		}
	}

    public bool IsShopItemPurchased(string name) {
        return playerData.purchasedShopItems.Contains(name);
    }

    private void LoadPlayerData()
    {
		playerData = new PlayerData ();
		PlayerPrefs.SetString ("displayedShopItems", "ChristmasTree,Fire");
		PlayerPrefs.SetString ("purchasedShopItems", "ChristmasTree,Fire");//Manually add ChristmasTree

        if (PlayerPrefs.HasKey("name"))
        {
        } else
        {
			PlayerPrefs.SetString ("name", "");
        }
		playerData.name = PlayerPrefs.GetString("name");

        if (PlayerPrefs.HasKey("coins"))
        {
        } else
        {
			PlayerPrefs.SetInt ("coins", 0);
        }
		playerData.coins = PlayerPrefs.GetInt("coins");

		if (PlayerPrefs.HasKey("displayedShopItems"))
		{
		} else
		{
			PlayerPrefs.SetString ("displayedShopItems", "");
		}
		string displayedShopItemsString = PlayerPrefs.GetString("displayedShopItems");
		playerData.displayedShopItems = displayedShopItemsString.Split(',').ToList();

        if (PlayerPrefs.HasKey("purchasedShopItems"))
        {
        } else
        {
			PlayerPrefs.SetString ("purchasedShopItems", "");
        }
		string purchasedShopItemsString = PlayerPrefs.GetString("purchasedShopItems");
		playerData.purchasedShopItems = purchasedShopItemsString.Split(',').ToList();

		if (PlayerPrefs.HasKey("equippedItems"))
		{
		} else
		{
			PlayerPrefs.SetString ("equippedItems", "");
			Debug.Log ("Initializing equippedItems");
		}
		string equippedItemsString = PlayerPrefs.GetString("equippedItems");
		playerData.equippedItems = equippedItemsString.Split (',').ToList ();

    }

	public PlayerData GetPlayerData(){
		Debug.Log ("Getting Player Data");
		return playerData;
	}

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("coins", playerData.coins);
        PlayerPrefs.SetString("name", playerData.name);
		PlayerPrefs.SetString("displayedShopItems", String.Join(",", playerData.displayedShopItems.ToArray()));
		PlayerPrefs.SetString("purchasedShopItems", String.Join(",", playerData.purchasedShopItems.ToArray()));
		PlayerPrefs.SetString("equippedItems", String.Join(",", playerData.equippedItems.ToArray()));

		PlayerPrefs.Save ();
    }
}
