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
    public Button purchaseButton;
	public Text DescriptionText;
    public ShowPanel panel;

    public void Initialize()
    {
		items = GetComponent<ShopDataController>().GetDisplayedItems();
		pageNumber = 0;
		pagePositions = new float[items.Count / 4 + 1];
		for(int i = 0; i < items.Count/4 + 1; i++){
			pagePositions [i] = - 880f * i;
		}
		curSelectedItem = null;
		RefreshShopDisplay ();
        purchaseButton.gameObject.SetActive(false);
        purchaseButton.onClick.AddListener(() => { purchaseItem(); });
    }

    private void purchaseItem()
    {
        SendMessageUpwards("PurchaseSelectedItem");
    }

    private List<ShopItemData> TempGetSomeItem(){//Useanother container for data for ShopItems as Class ShopItem is used as the script controllling the shopitem attached
		List<ShopItemData> itemList = new List<ShopItemData> ();
		itemList.Add (new ShopItemData{cost = 2, fullName = "Item1", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 3, fullName = "Item2", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 4, fullName = "Item3", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 5, fullName = "Item4", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item5", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item6", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item7", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item8", isBuyable = false, isOnSale = false});
		itemList.Add (new ShopItemData{cost = 6, fullName = "Item9", isBuyable = false, isOnSale = false});
		return itemList;
	}

	private void RefreshShopDisplay (){

		DescriptionText.text = "";

		//TODO Clear the content of the shopWindowContent
		//Loop through the children and set them to inactive
		for(int i = 0; i < shopWindowContent.transform.childCount; i++){
			shopWindowContent.transform.GetChild(i).gameObject.SetActive(false);
			Debug.Log("clearing existing items in the shop");
		}

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
			item.transform.GetChild (0).GetComponent<Image> ().color = Color.clear;
			item.GetComponent<Image> ().color= Color.clear;
		}
	}



    public void SelectItem(ShopItem item)
    {
        // TODO: Display an item as 'selected', and unselect any previous 'selected' item

		if (item.isSelected) {
			UnselectItem (item);
		} else {
			if (curSelectedItem)
				UnselectItem (curSelectedItem);
			item.SetSelected();
            purchaseButton.gameObject.SetActive(true);
            purchaseButton.interactable = true;
            Debug.Log("button is active: " + purchaseButton.gameObject.active);
			DescriptionText.text = item.fullName.ToString ();
			curSelectedItem = item;

		}

    }

    private void UnselectItem(ShopItem item)
    {
        if (item == null)
        {
            return;
        }
		DescriptionText.text = "";
		item.SetUnselected();
		curSelectedItem = null;
        purchaseButton.gameObject.SetActive(false);
        purchaseButton.interactable = false;
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

	public void EndandSave(){
		purchaseButton.gameObject.SetActive (false);
	}

	public void UIShopItemNextPage(){
		StopAllCoroutines ();
		if (curSelectedItem)
			UnselectItem (curSelectedItem);
		pageNumber += 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (pagePositions[pageNumber], 0f, 0f)));
	}
	public void UIShopItemPreviousPage(){
		StopAllCoroutines ();
		if(curSelectedItem)
			UnselectItem (curSelectedItem);
		pageNumber -= 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (pagePositions[pageNumber], 0f, 0f)));
	}

	IEnumerator MoveItemPage(Vector3 PagePosition){
		Vector3 desiredPosition = PagePosition;
		//Debug.Log (desiredPosition.x + " is where I am heading to" + " i am at" + shopWindowContent.GetComponent<RectTransform> ().anchoredPosition.x);
		while (Mathf.Abs (shopWindowContent.GetComponent<RectTransform> ().anchoredPosition.x - desiredPosition.x) > 0.1f) {
			shopWindowContent.GetComponent<RectTransform> ().anchoredPosition = Vector3.Lerp (shopWindowContent.GetComponent<RectTransform> ().anchoredPosition, desiredPosition, 0.1f);
			yield return new WaitForFixedUpdate ();
		}
	}
}