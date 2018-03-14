using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PanelController : MonoBehaviour {

    public Button yesBtn;
    public Button noBtn;
    public Text text;
    public string itemName;
    public string itemCost;
	// Use this for initialization
	void Start () {
		Refresh ();
	}

	public void Refresh(){
		text.text = "是否花" + itemCost + "元购买" + itemName + " ?";
		yesBtn.onClick.AddListener (PurchaseItem);
	}

    private void PurchaseItem()
    {
        SendMessageUpwards("PurchaseSelectedItem_Master");
        Debug.Log("item purchased");
    }


}
