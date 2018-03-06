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
		Debug.Log ("EditorMode Started");
		dataController.Initialize();
		displayController.Initialize();
	}

	private void SelectItem_Master(EditorModeItem item)
	{
		dataController.SelectItem(item);
		displayController.SelectItem(item);
        //Debug.Log("SelectItem_Master() called");
	}
	public void EndandSave(){
		dataController.EndandSave ();
	}
}
