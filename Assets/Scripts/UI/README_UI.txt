//EditorModeItemData is found under EditorModeDataController.cs;
//
//PlayerPref Stores the itemNames of the items the player owns, and itemNames of the items equipped
//Pass the itemNames to EditorModeDataController and it will return a list of the EditorModeItemData equippedItemData
//Pass equippedItemData to EditorModeDisplayController to instantiate the correct Buttons as well as the equipped items
//
//For saving, ...
//
//Item is the script attached to the gameObject, ItemData is the class that contains all the information of that item
//TextAssets as Json files provides the strings and int types of data, while the image data is filled by the display controller
//
//DataController.ParseItem is for adding the correct properties to the correct itemData. However, I don't know if it works. See line 50 of EditorModeDataController
//
//Class ItemData is saved under PlayerData.cs
//
//Pass in the values for itemsScript at displayControllers
//