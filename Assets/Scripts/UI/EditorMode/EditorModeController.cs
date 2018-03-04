using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EditorModeController : MonoBehaviour
{
	private ShopDataController  dataController;
	private EditorModeDisplayController  displayController;

	void Start()
	{
		//dataController = GetComponent<ShopDataController>();
		displayController = GetComponent<EditorModeDisplayController>();
		StartEditorMode();
	}

	void Update()
	{
		/* This part no need as inputs are handled by individual buttons using onClick Listeners, which is a feature of the UI
        if (!Input.GetMouseButtonDown(0))
        {
            // No player input received
            return;
        }

        if (UserSelectedPurchase(Input.mousePosition))
        {
            PurchaseSelectedItem();
            return;
        }

        ShopItem selectedItem = GetSelectedShopItem(Input.mousePosition);
        if (selectedItem != null)
        {
            SelectItem(selectedItem);
        }
        */
	}

	private void StartEditorMode()
	{
		//dataController.Initialize();
		Debug.Log ("Shop Started");
		displayController.Initialize();
		//displayController.DisableItems(dataController.purchasedItems);
	}

	private void PurchaseSelectedItem()
	{
		bool success = dataController.PurchaseSelectedItem();
		if (!success)
		{
			// Purchase failed, possibly due to insufficient coins
			// in the inventory or the lack of selected items
			displayController.DisplayFailedPurchase();
		}
		displayController.UnselectSelectedItem();
		dataController.UnselectSelectedItem();
	}

	private void SelectItem_Master(EditorModeItem item)
	{
		//dataController.SelectItem(item);
		displayController.SelectItem(item);
	}

	private bool UserSelectedPurchase(Vector3 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hitInfo;
		return Physics.Raycast(ray, out hitInfo, Mathf.Infinity,
			1 << LayerMask.NameToLayer("BuyItem"));
	}

	/**
    * button.onClick is used in place of raycast 
    *
    private ShopItem GetSelectedShopItem(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;
        ShopItem item = null;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity,
                1 << LayerMask.NameToLayer("ShopItem")))
        {
            item = hitInfo.collider.gameObject.GetComponent<ShopItem>();
        }
        return item;
    }
    */
}
