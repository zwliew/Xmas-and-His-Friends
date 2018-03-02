using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/**
 * Class containing the data of an individual item sold in the shop,
 * as well as any behaviour for the animation and display of the item.
 */
[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public int cost;
	public string fullName;
	public Image itemImage;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public bool isBuyable;
	public bool isOnSale;
	public bool isSelected;

	private Button itemButton;


	public void Initialize(){
		isSelected = false;
		itemButton = GetComponent<Button> ();
		itemButton.onClick.RemoveAllListeners ();
		itemButton.onClick.AddListener (()=>{SelectItem();});
		Debug.Log ("ItemButtonInitialized");
	}

	public void SelectItem(){
		if (!isSelected) {
			itemButton.GetComponent<Image> ().sprite = selectedSprite;
			Debug.Log (fullName + " is selected");
			isSelected = true;
		} else {
			itemButton.GetComponent<Image> ().sprite = unselectedSprite;
			isSelected = false;
		}
	}
}
