using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorModeItem : MonoBehaviour {

	public int cost;
	public string fullName;
	public Sprite itemSprite;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public bool isBuyable;
	public bool isOnSale;
	public bool isSelected;

	private Button itemButton;
	private EditorModeItem editorModeItem;

	public void Initialize(){
		isSelected = false;
		itemButton = GetComponent<Button> ();
		editorModeItem = GetComponent<EditorModeItem> ();
		itemButton.gameObject.transform.GetChild(0).GetComponentInChildren<Image> ().sprite = itemSprite;
		itemButton.onClick.RemoveAllListeners ();
		itemButton.onClick.AddListener (()=>{SelectItem();});
	}

	public void SelectItem(){
		SendMessageUpwards ("SelectItem_Master", editorModeItem);
	}

	public void SetSelected(){
		Debug.Log (fullName + " is selected");
		itemButton.GetComponent<Image> ().sprite = selectedSprite;
		isSelected = true;
	}

	public void SetUnselected(){
		itemButton.GetComponent<Image> ().sprite = unselectedSprite;
		isSelected = false;
	}

	public void SetDisabled(){
		itemButton.onClick.RemoveAllListeners ();
		Debug.Log ("One item has been disabled");
	}
}

[System.Serializable]
public class EditorModeItemData{
	public int cost;
	public string fullName;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public bool isBuyable;
	public bool isOnSale;
	public bool isSelected;
}
