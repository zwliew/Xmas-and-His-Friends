using UnityEngine;

/**
 * Class handling the updates to the player data based on
 * actions made in the shop (purchasing, etc.)
 *
public class ShopDataController : MonoBehaviour
{
    private PlayerDataController playerDataController;

    private ShopItem curSelectedItem;

    public void Initialize()
    {
        curSelectedItem = null;
        // TODO: Add a "Persistent" game object scene with the PlayerDataController script attached
        playerDataController = GameObject.FindGameObject("Persistent")
                .GetComponent<playerDataController>();
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
        bool success = playerDataController.UpdatePlayerCoins(curSelectedItem.cost);
        return success;
    }
}
*/