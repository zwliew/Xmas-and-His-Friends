using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class containing the data of an individual item sold in the shop,
 * as well as any behaviour for the animation and display of the item.
 */

[System.Serializable]
public class ShopItem
{
    public int cost;
    public string name;
    public string fullName;
}
