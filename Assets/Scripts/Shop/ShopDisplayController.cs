using UnityEngine;

public class ShopDisplayController : MonoBehaviour
{
    private ShopItem curSelectedItem;

    public void Initialize()
    {
        curSelectedItem = null;
    }

    public void SelectItem(ShopItem item)
    {
        // TODO: Display an item as 'selected', and unselect any previous 'selected' item
        UnselectItem(curSelectedItem);
        curSelectedItem = item;
    }

    private void UnselectItem(ShopItem item)
    {
        if (item == null)
        {
            // No item to unselect
            return;
        }
        // TODO: Unselected the item given as a parameter
    }

    public void UnselectSelectedItem()
    {
        UnselectItem(curSelectedItem);
    }

    public void DisplayFailedPurchase()
    {
        // TODO: Display an indicator that the purchase failed
        // (maybe a red ring around the 'purchase' button?)
    }
}