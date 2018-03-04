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

public class EditorModeDisplayController : MonoBehaviour
{
	private EditorModeItem curSelectedItem;
	//private ShopItem previouslySelectedItem;
	private List<EditorModeItemData> items;//Stores the item data
	private List<Button> itemButton;//Stores button instantiated on runtime;

	private int pageNumber;
	private float[] pagePositions;

    //public List<EditorModeItem> purchasedItems;
	public GameObject editorModeWindowContent;//The parent of the button
	public Button prefabItemButton;

	public void Initialize()
	{
		items = TempGetSomeItem ();

		pageNumber = 0;
		pagePositions = new float[items.Count / 4 + 1];
		for(int i = 0; i < items.Count/4 + 1; i++){
			pagePositions [i] = 210f*4 * i;
		}

		curSelectedItem = null;
		RefreshEditorModeDisplay ();
	}

	private List<EditorModeItemData> TempGetSomeItem(){//Useanother container for data for ShopItems as Class ShopItem is used as the script controllling the shopitem attached
		List<EditorModeItemData> itemList = new List<EditorModeItemData> ();

        itemList.Add(new EditorModeItemData { cost = 2, fullName = "Item2", isBuyable = false, isOnSale = false, furniture = GameObject.FindGameObjectWithTag("Fire"), position = new Vector3(0f,0f,0f), rotation = new Vector3(0f,0f,0f)});
		itemList.Add (new EditorModeItemData { cost = 3, fullName = "Item3", isBuyable = false, isOnSale = false, furniture = GameObject.FindGameObjectWithTag("ChristmasTree"), position = new Vector3(0f, 0f, 0f), rotation = new Vector3(0f, 0f, 0f) });
		itemList.Add (new EditorModeItemData{cost = 4, fullName = "Item4", isBuyable = false, isOnSale = false, furniture = GameObject.FindGameObjectWithTag("Carpet"), position = new Vector3(0f, 0f, 0f), rotation = new Vector3(0f, 0f, 0f) });
		itemList.Add (new EditorModeItemData{cost = 5, fullName = "Item5", isBuyable = false, isOnSale = false});

		itemList.Add (new EditorModeItemData{cost = 2, fullName = "Item1", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 3, fullName = "Item2", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 4, fullName = "Item3", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 5, fullName = "Item4", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 6, fullName = "Item5", isBuyable = false, isOnSale = false});

		itemList.Add (new EditorModeItemData{cost = 6, fullName = "Item6", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 6, fullName = "Item7", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 6, fullName = "Item8", isBuyable = false, isOnSale = false});
		itemList.Add (new EditorModeItemData{cost = 6, fullName = "Item9", isBuyable = false, isOnSale = false});
		return itemList;
	}

	private void RefreshEditorModeDisplay (){

		//TODO Clear the content of the editorModeWindowContent
		//Loop through the children and set them to inactive
		for(int i = 0; i < editorModeWindowContent.transform.childCount; i++){
			editorModeWindowContent.transform.GetChild(i).gameObject.SetActive(false);
			Debug.Log("clearing existing items in the shop");// Somehow this does not work. Possible reason is that the shop items are inactive
			//GameObjectUtility.customDestroy (editorModeWindowContent.transform.GetChild (0).gameObject);
		}

		foreach (EditorModeItemData itemData in items) {
			Button item = Instantiate (prefabItemButton, editorModeWindowContent.transform);
			/*Button item = GameObjectUtility.customInstantiate(prefabItemButton.gameObject, Vector3.zero).GetComponent<Button>();
			item.transform.SetParent (editorModeWindowContent.transform, false);
			*/
			EditorModeItem itemScript = item.gameObject.GetComponent<EditorModeItem> ();
			itemScript.fullName = itemData.fullName;
            itemScript.furniture = itemData.furniture;
            itemScript.position = itemData.position;
            itemScript.rotation = itemData.rotation;
            Debug.Log("parameters passed successfully!");
            //Pass in the values here
            itemScript.Initialize ();
		}

		for (int i = 0; i < 4 - items.Count % 4; i++) {
			Button item = Instantiate (prefabItemButton, editorModeWindowContent.transform);
			item.interactable = false;
		}

	}

	public void SelectItem(EditorModeItem item)
	{
		// TODO: Display an item as 'selected', and unselect any previous 'selected' item

		if (item.isSelected) {
			UnselectItem (item);
		} else {
			//if (curSelectedItem)
				//UnselectItem (curSelectedItem);
			item.SetSelected();
			curSelectedItem = item;
            item.furniture.transform.position = item.position;
            Debug.Log(item.furniture.transform.position.x);
            //item.furniture.transform.rotation = Quaternion.Euler(item.position);
            //Debug.Log(item.furniture.transform.rotation.y);
            item.furniture.SetActive(true);
 
		}

	}

	private void UnselectItem(EditorModeItem item)
	{
		if (item == null)
		{
			// No item to unselect
			return;
		}
		// TODO: Unselected the item given as a parameter
		item.SetUnselected();
		curSelectedItem = null;
        item.furniture.SetActive(false);
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

	public void DisableItems(List<EditorModeItem> items) {
		// TODO: Disable the items by 'greying' them out and preventing
		// the user from selecting them.
		// This is usually used for items that have already been purchased.
	}

	public void UIShopItemNextPage(){
		StopAllCoroutines ();
		if (curSelectedItem)
			UnselectItem (curSelectedItem);
		pageNumber += 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (0f, pagePositions[pageNumber], 0f)));
	}
	public void UIShopItemPreviousPage(){
		StopAllCoroutines ();
		if(curSelectedItem)
			UnselectItem (curSelectedItem);
		pageNumber -= 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (0f, pagePositions[pageNumber], 0f)));
	}

	IEnumerator MoveItemPage(Vector3 PagePosition){
		Vector3 desiredPosition = PagePosition;
		//Debug.Log (desiredPosition + " is where I am heading to" + " i am at" + editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition);
		//Debug.Log ("PageNumber is " + pageNumber + " Total pages are " + items.Count/4);
		while (Mathf.Abs (editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition.y - desiredPosition.y) > 0.1f) {
			editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition = Vector3.Lerp (editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition, desiredPosition, 0.1f);
			yield return new WaitForFixedUpdate ();
		}
	}
}