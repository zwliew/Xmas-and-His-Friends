using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelController : MonoBehaviour {

    public Button yesBtn;
    public Button noBtn;
    public Text text;
    public string itemName;
    public string itemCost;
	// Use this for initialization
	void Start () {
        text.text = "是否花" + itemCost + "元购买" + itemName + " ?";
        yesBtn.onClick.AddListener(()=> { PurchaseItem(); });
	}

    private void PurchaseItem()
    {
        SendMessageUpwards("PurchaseSelectedItem");
        Debug.Log("item purchased");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
