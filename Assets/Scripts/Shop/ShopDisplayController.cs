using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopDisplayController : MonoBehaviour
{
    private ShopItem curSelectedItem;
	private List<ShopItem> items;
	private List<Button> itemButtons;

	public GameObject shopWindowContent;
	public GameObject prefabItemButton;

    public void Initialize()
    {
		items = TempGetSomeItem ();
		RefreshShopDisplay ();
		curSelectedItem = null;
    }

	private List<ShopItem> TempGetSomeItem(){
		List<ShopItem> itemList = new List<ShopItem> ();
		itemList.Add (new ShopItem{cost = 2, name = "Item2", itemImage = null, isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItem{cost = 3, name = "Item3", itemImage = null, isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItem{cost = 4, name = "Item4", itemImage = null, isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItem{cost = 5, name = "Item5", itemImage = null, isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItem{cost = 6, name = "Item6", itemImage = null, isBuyable = false, isOnSale = false});
		return itemList;
	}

	private void RefreshShopDisplay (){
		Button btn = null;
		btn.transform.SetParent (shopWindowContent.transform);
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