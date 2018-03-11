using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public string name;
	public List<string> displayedShopItems;
    public List<string> purchasedShopItems; // String list of all the names of the purchased shop items
	public List<string> equippedItems;
}

public class ItemData{
	public int cost;
	public string fullName;
	public bool equipped;
	public bool purchased;
}
