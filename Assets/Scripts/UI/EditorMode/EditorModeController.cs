using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class EditorModeController : MonoBehaviour
{
	private EditorModeDataController  dataController;
	private EditorModeDisplayController  displayController;

	public void Initialize(){
		dataController = GetComponent<EditorModeDataController>();
		displayController = GetComponent<EditorModeDisplayController>();
		StartEditorMode();
	}

	private void StartEditorMode()
	{
		dataController.Initialize();
		displayController.Initialize();
		Debug.Log ("EditorMode Started");
	}

	private void SelectItem_Master(EditorModeItem item)
	{
		dataController.SelectItem(item);
		displayController.SelectItem(item);
        //Debug.Log("SelectItem_Master() called");
	}
}
