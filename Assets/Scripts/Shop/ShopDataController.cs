using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Class handling the updates to the player data based on
 * actions made in the shop (purchasing, etc.)
 */
public class ShopDataController : MonoBehaviour
{
    private PlayerDataController playerDataController;

    private ShopItem[] shopItems;
    public List<ShopItem> purchasedItems;

    private ShopItem curSelectedItem;

    public void Initialize()
    {
        curSelectedItem = null;
        // TODO: Add a "Persistent" game object scene with the PlayerDataController script attached
		playerDataController = GameObject.FindGameObjectWithTag("Persistent")
                .GetComponent<PlayerDataController>();

        TextAsset dataAsJson = Resources.Load<TextAsset> ("Shop/ShopData");
        ShopJsonData shopJsonData = JsonUtility.FromJson<ShopJsonData>(dataAsJson.text);
        shopItems = shopJsonData.shopItems;

        foreach (ShopItem shopItem in shopItems) {
            if (playerDataController.IsShopItemPurchased(shopItem.fullName)) {
                purchasedItems.Add(shopItem);
            }
        }
    }

    public void SelectItem(ShopItem item)
    {
        curSelectedItem = item;
    }

    public void UnselectSelectedItem()
    {
        curSelectedItem = null;
    }

    public bool PurchaseSelectedItem()
    {
        if (curSelectedItem == null)
        {
            // No item to purchase
            return false;
        }
        bool success = playerDataController.PurchaseShopItem(curSelectedItem.fullName, curSelectedItem.cost);
        if (success) {
            purchasedItems.Add(curSelectedItem);
        }
        return success;
    }
}
