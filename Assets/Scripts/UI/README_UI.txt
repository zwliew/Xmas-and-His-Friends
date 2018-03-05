//EditorModeItemData is found under EditorModeItem.cs;
//
//PlayerPref Stores the itemNames of the items the player owns, and itemNames of the items equipped
//Pass the itemNames to EditorModeDataController and it will return a list of the EditorModeItemData equippedItemData
//Pass equippedItemData to EditorModeDisplayController to instantiate the correct Buttons as well as the equipped items
//
//For saving, ...
//
//Item is the script attached to the gameObject, ItemData is the class that contains all the information of that item
//TextAssets as Json files provides the strings and int types of data, while the image data is filled by the display controller
