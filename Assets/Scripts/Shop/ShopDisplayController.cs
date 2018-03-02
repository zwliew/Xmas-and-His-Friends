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
 * 
 * Class ShopItem is the controller
 * Class ShopItemData is the data
 * 
 * If you find this too confusing then you can refactor back to ShopItem and ShopItemScript
 * 
 * Item Description is displayed by this script
 * 
 * 4 button distance: 870
 */

public class ShopDisplayController : MonoBehaviour
{
    private ShopItem curSelectedItem;
	//private ShopItem previouslySelectedItem;
	private List<ShopItemData> items;//Stores the item data
	private List<Button> itemButton;//Stores button instantiated on runtime;

	private int pageNumber;
	private float[] pagePositions;

	public List<ShopItem> purchasedItems;
	public GameObject shopWindowContent;//The parent of the button
	public Button prefabItemButton;

	public Text DescriptionText;

	public void Start(){//for debug purposes only
		Initialize();
	}

    public void Initialize()
    {
		items = TempGetSomeItem ();
		pageNumber = 0;
		pagePositions = new float[items.Count / 4 + 1];
		for(int i = 0; i < items.Count/4 + 1; i++){
			pagePositions [i] = - 435f - 870f * i;
		}
		curSelectedItem = null;
		RefreshShopDisplay ();
    }

	private List<ShopItemData> TempGetSomeItem(){//Useanother container for data for ShopItems as Class ShopItem is used as the script controllling the shopitem attached
		List<ShopItemData> itemList = new List<ShopItemData> ();
		itemList.Add (new ShopItemData{cost = 2, fullName = "Item2", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 3, fullName = "Item3", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 4, fullName = "Item4", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 5, fullName = "Item5", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item6", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item7", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item8", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item9", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item6", isBuyable = false, isOnSale = false});
		return itemList;
	}

	private void RefreshShopDisplay (){

		DescriptionText.text = "";

		foreach (ShopItemData itemData in items) {
			Button item = Instantiate (prefabItemButton, shopWindowContent.transform);
			ShopItem itemScript = item.gameObject.GetComponent<ShopItem> ();
			itemScript.fullName = itemData.fullName;
			//Pass in the values here
			itemScript.Initialize ();
		}
		for (int i = 0; i < 4 - items.Count % 4; i++) {
			Button item = Instantiate (prefabItemButton, shopWindowContent.transform);
			item.interactable = false;
		}
	}

    public void SelectItem(ShopItem item)
    {
        // TODO: Display an item as 'selected', and unselect any previous 'selected' item

		if (item.isSelected) {
			item.SetUnselected ();
			DescriptionText.text = "";
			curSelectedItem = null;
		} else {
			if (curSelectedItem)
				curSelectedItem.SetUnselected ();
			item.SetSelected();
			DescriptionText.text = item.fullName.ToString ();
			curSelectedItem = item;
		}

    }

    private void UnselectItem(ShopItem item)
    {
        if (item == null)
        {
            // No item to unselect
            return;
        }
        // TODO: Unselected the item given as a parameter
		item.SetUnselected();
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

	public void UIShopItemNextPage(){
		StopAllCoroutines ();
		//Debug.Log ("current page" + pageNumber + " " + items.Count/4 + " is the max page number");
		pageNumber += 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (pagePositions[pageNumber], 0f, 0f)));
	}
	public void UIShopItemPreviousPage(){
		StopAllCoroutines ();
		//Debug.Log ("current page" + pageNumber + " " + items.Count/4 + " is the max page number");
		pageNumber -= 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (pagePositions[pageNumber], 0f, 0f)));
	}

	IEnumerator MoveItemPage(Vector3 PagePosition){
		Vector3 desiredPosition = PagePosition;
		Debug.Log (desiredPosition.x + " is where I am heading to" + " i am at" + shopWindowContent.GetComponent<RectTransform> ().localPosition.x);
		while (Mathf.Abs (shopWindowContent.GetComponent<RectTransform> ().localPosition.x - desiredPosition.x) > 4f) {
			shopWindowContent.GetComponent<RectTransform> ().localPosition = Vector3.Lerp (shopWindowContent.GetComponent<RectTransform> ().localPosition, desiredPosition, 0.1f);
			yield return new WaitForFixedUpdate ();
		}
	}
}