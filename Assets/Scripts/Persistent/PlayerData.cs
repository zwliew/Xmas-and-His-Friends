using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public string name;
    public List<string> purchasedShopItems; // String list of all the names of the purchased shop items
}
