using UnityEngine;
using System;
/*
public class ShopController : MonoBehaviour
{
    private DataController dataController;
    private DisplayController displayController;

    void Start()
    {
        dataController = GetComponent<DataController>();
        displayController = GetComponent<DisplayController>();
        StartShop();
    }

    void Update()
    {
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
    }

    private void StartShop()
    {
        dataController.Initialize();
        displayController.Initialize();
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

    private void SelectItem(ShopItem item)
    {
        dataController.SelectItem(item);
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
    * Gets the side being selected based on position
    * Returns null if no side is being selected
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
}
*/