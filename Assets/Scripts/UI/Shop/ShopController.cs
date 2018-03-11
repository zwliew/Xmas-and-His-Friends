using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShopController : MonoBehaviour
{
	private ShopDataController  dataController;
	private ShopDisplayController  displayController;

    public void Initialize()
    {
		dataController = GetComponent<ShopDataController>();
		displayController = GetComponent<ShopDisplayController>();
		StartShop();
    }

    private void StartShop()
    {
		Debug.Log ("Shop Started");
        dataController.Initialize();
        displayController.Initialize();
		//displayController.DisableItems(dataController.purchasedItems);
    }


    private void SelectItem_Master(ShopItem item)
    {
        dataController.SelectItem(item);
        displayController.SelectItem(item);
    }

	private void PurchaseSelectedItem()
	{
		dataController.PurchaseSelectedItem1 ();
		displayController.UnselectSelectedItem();
		dataController.UnselectSelectedItem();
	}
		
	public void EndandSave(){
		displayController.EndandSave ();
//		dataController.EndandSave ();
	}

}
