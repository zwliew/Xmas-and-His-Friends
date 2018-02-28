using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public void DisableItems(List<ShopItem> items) {
        // TODO: Disable the items by 'greying' them out and preventing
        // the user from selecting them.
        // This is usually used for items that have already been purchased.
    }
}