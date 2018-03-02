using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/*
 * ShopItem is displayed as buttons with an image as its child.
 * This is because I cannot figure out how to add event triggers onto Image
 */

public class ShopDisplayController : MonoBehaviour
{
    private ShopItem curSelectedItem;
	private List<ShopItem> items;//Stores the item data
	private List<Button> itemButton;//Stores button instantiated on runtime;

	public List<ShopItem> purchasedItems;
	public GameObject shopWindowContent;//The parent of the button
	public Button prefabItemButton;

	public Image itemImage;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;

	public void Start(){//for debug purposes only
		Initialize();
	}

    public void Initialize()
    {
		items = TempGetSomeItem ();
		RefreshShopDisplay ();
		curSelectedItem = null;
    }

	private List<ShopItem> TempGetSomeItem(){//Useanother container for data for ShopItems as Class ShopItem is used as the script controllling the shopitem attached
		List<ShopItem> itemList = new List<ShopItem> ();
		itemList.Add (new ShopItem{cost = 2, fullName = "Item2", itemImage = null, isBuyable = false, isOnSale = false, selectedSprite = this.selectedSprite, unselectedSprite = this.unselectedSprite});
		itemList.Add (new ShopItem{cost = 3, fullName = "Item3", itemImage = null, isBuyable = false, isOnSale = false, selectedSprite = this.selectedSprite, unselectedSprite = this.unselectedSprite});
		itemList.Add (new ShopItem{cost = 4, fullName = "Item4", itemImage = null, isBuyable = false, isOnSale = false, selectedSprite = this.selectedSprite, unselectedSprite = this.unselectedSprite});
		itemList.Add (new ShopItem{cost = 5, fullName = "Item5", itemImage = null, isBuyable = false, isOnSale = false, selectedSprite = this.selectedSprite, unselectedSprite = this.unselectedSprite});
		itemList.Add (new ShopItem{cost = 6, fullName = "Item6", itemImage = null, isBuyable = false, isOnSale = false, selectedSprite = this.selectedSprite, unselectedSprite = this.unselectedSprite});
		return itemList;
	}

	private void RefreshShopDisplay (){
		foreach (ShopItem shopItem in items) {
			Button item = Instantiate (prefabItemButton, shopWindowContent.transform);
			ShopItem itemScript = item.gameObject.GetComponent<ShopItem> ();
			itemScript.fullName = shopItem.fullName;
			//Pass in the values here
			itemScript.Initialize ();
		}
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