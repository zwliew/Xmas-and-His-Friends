using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/**
 * The following comments are not about this script
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
	private List<EditorModeItemData> items;//Stores the item data
	private List<Button> itemButton;//Stores button instantiated on runtime;

	private int pageNumber;
	private float[] pagePositions;

    //public List<EditorModeItem> purchasedItems;
	public GameObject editorModeWindowContent;//The parent of the button
	public Button prefabItemButton;

	public void Initialize()
	{
		items = GetComponent<EditorModeDataController> ().GetPurchasedItemsData();

		foreach (EditorModeItemData itemData in items) {
			itemData.furniture = GameObject.FindGameObjectWithTag (itemData.fullName);
		}

		pageNumber = 0;
		pagePositions = new float[items.Count / 4 + 1];
		for(int i = 0; i < items.Count/4 + 1; i++){
			pagePositions [i] = 210f * 4 * i;
		}

		curSelectedItem = null;
		RefreshEditorModeDisplay ();
	}


	private void RefreshEditorModeDisplay (){

		for(int i = 0; i < editorModeWindowContent.transform.childCount; i++){//Clear the content of the editorModeWindowContent, Loop through the children and set them to inactive
			editorModeWindowContent.transform.GetChild(i).gameObject.SetActive(false);
//			GameObjectUtility.customDestroy (editorModeWindowContent.transform.GetChild (0).gameObject); <Implement this in the future>
		}

		foreach (EditorModeItemData itemData in items) {
			Button item = Instantiate (prefabItemButton, editorModeWindowContent.transform);
//			Button item = GameObjectUtility.customInstantiate(prefabItemButton.gameObject, Vector3.zero).GetComponent<Button>();
//			item.transform.SetParent (editorModeWindowContent.transform, false);

			EditorModeItem itemScript = item.gameObject.GetComponent<EditorModeItem> ();
			itemScript.fullName = itemData.fullName;//Pass in the values here
            itemScript.furniture = itemData.furniture;
            itemScript.position = itemData.position;
            itemScript.rotation = itemData.rotation;
			itemScript.isSelected = itemData.equipped;
//			Debug.Log("parameters passed successfully!" +itemScript.isSelected );
            itemScript.Initialize ();
		}

		for (int i = 0; i < 4 - items.Count % 4; i++) {//fill up the pages with invisible buttons
			Button item = Instantiate (prefabItemButton, editorModeWindowContent.transform);
			item.interactable = false;
			item.transform.GetChild (0).GetComponent<Image> ().color = Color.clear;
			item.GetComponent<Image> ().color= Color.clear;
		}

	}

	public void SelectItem(EditorModeItem item)
	{
		// TODO: Display an item as 'selected', and unselect any previous 'selected' item

		if (item.isSelected) {
			UnselectItem (item);
		} else {
			item.SetSelected();
			curSelectedItem = item;
            if (item.furniture)
            {
                item.furniture.transform.position = item.position;
                //Debug.Log(item.furniture.transform.position.x);
                //item.furniture.transform.rotation = Quaternion.Euler(item.position);
                //Debug.Log(item.furniture.transform.rotation.y);
                item.furniture.SetActive(true);
            }
 
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


	public void UIShopItemNextPage(){
		StopAllCoroutines ();
		pageNumber += 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (0f, pagePositions[pageNumber], 0f)));
	}
	public void UIShopItemPreviousPage(){
		StopAllCoroutines ();
		pageNumber -= 1;
		pageNumber = Mathf.Clamp (pageNumber, 0, items.Count / 4);
		StartCoroutine( MoveItemPage (new Vector3 (0f, pagePositions[pageNumber], 0f)));
	}

	IEnumerator MoveItemPage(Vector3 PagePosition){
		Vector3 desiredPosition = PagePosition;
		while (Mathf.Abs (editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition.y - desiredPosition.y) > 0.1f) {
			editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition = Vector3.Lerp (editorModeWindowContent.GetComponent<RectTransform> ().anchoredPosition, desiredPosition, 0.1f);
			yield return new WaitForFixedUpdate ();
		}
	}
	
	public void DisableItems(List<EditorModeItem> items) {
		// TODO: Disable the items by 'greying' them out and preventing
		// the user from selecting them.
		// This is usually used for items that have already been purchased.
	}
}