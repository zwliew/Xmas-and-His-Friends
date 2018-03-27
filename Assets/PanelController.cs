using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PanelController : MonoBehaviour {

    public Button yesBtn;
    public Button noBtn;
	public Text price;
	public Text nameText;
	[HideInInspector]
    public string itemName;
	[HideInInspector]
    public string itemCost;
	// Use this for initialization
	void Start () {
		//Refresh ();
	}

	public void Refresh(){
		price.text = itemCost.ToString ();
		nameText.text = "\"" + itemName.ToString () + "\"";
		yesBtn.onClick.RemoveAllListeners ();
		yesBtn.onClick.AddListener (PurchaseItem);
	}

    private void PurchaseItem()
    {
        SendMessageUpwards("PurchaseSelectedItem_Master");
        Debug.Log("item purchased btn click in panel");
    }


}
