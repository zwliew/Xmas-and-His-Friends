using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPool : MonoBehaviour {

    private List<Button> buttons;
    private Button prefab;
    private Transform parent;

	public void InitializeObjectPool(Button prefab, Transform parent) {
		buttons = new List<Button>();
		this.prefab = prefab;
		this.parent = parent;
	}


	/* Constructs an object pool with a prefab and a parent transform */
    public UIObjectPool(Button prefab, Transform parent) {
        buttons = new List<Button>();
        this.prefab = prefab;
        this.parent = parent;
    }

    /* Returns the next button in the pool */
    public Button GetButton() {
        Button button;
        if (buttons.Count > 0) {
            button = buttons[0];
            buttons.RemoveAt(0);
            button.gameObject.SetActive(true);
        } else {
            button = Instantiate(prefab, parent);
        }
        Debug.Log("GET: " + buttons.Count);
		button.GetComponent<ShopItem> ().Initialize ();
        return button;
    }

    /* Adds a button to the pool */
    public void DestroyPool(Button button) {
        buttons.Add(button);
        button.gameObject.SetActive(false);
        Debug.Log("DESTROY: " + buttons.Count);
    }
}
