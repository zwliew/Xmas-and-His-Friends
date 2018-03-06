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
        DontDestroyOnLoad(gameObject);
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
		playerData.equippedItems.Clear ();
		foreach (string itemName in itemNames) {
			playerData.equippedItems.Add (itemName);
		}
	}

    public bool IsShopItemPurchased(string name) {
        return playerData.purchasedShopItems.Contains(name);
    }

    private void LoadPlayerData()
    {
		playerData = new PlayerData ();
        if (PlayerPrefs.HasKey("name"))
        {
            playerData.name = PlayerPrefs.GetString("name");
        } else
        {
			PlayerPrefs.SetString ("name", null);
        }

        if (PlayerPrefs.HasKey("coins"))
        {
            playerData.coins = PlayerPrefs.GetInt("coins");
        } else
        {
			PlayerPrefs.SetInt ("coins", 0);
        }

        if (PlayerPrefs.HasKey("purchasedShopItems"))
        {
            string purchasedShopItemsString = PlayerPrefs.GetString("purchasedShopItems");
            playerData.purchasedShopItems = purchasedShopItemsString.Split(',').ToList();
        } else
        {
			PlayerPrefs.SetString ("purchasedShopItems", null);
        }
		if (PlayerPrefs.HasKey("equippedItems"))
		{
			string equippedItemsString = PlayerPrefs.GetString("equippedItems");
			playerData.equippedItems = equippedItemsString.Split (',').ToList ();
		} else
		{
			PlayerPrefs.SetString ("equippedItems", null);
		}
		PlayerPrefs.SetString ("purchasedShopItems", "ChristmasTree,Fire");//Manually add ChristmasTree
		PlayerPrefs.Save ();

    }

	public PlayerData GetPlayerData(){
		LoadPlayerData ();
		return playerData;
	}

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt("coins", playerData.coins);
        PlayerPrefs.SetString("name", playerData.name);
		PlayerPrefs.SetString("purchasedShopItems", String.Join(",", playerData.purchasedShopItems.ToArray()));//String.Join(",", playerData.purchasedShopItems));
		PlayerPrefs.SetString("equippedItems", String.Join(",", playerData.equippedItems.ToArray()));//String.Join(",", playerData.equippedItems));

		PlayerPrefs.Save ();
    }
}
