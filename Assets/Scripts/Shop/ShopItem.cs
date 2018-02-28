using UnityEngine;
using UnityEngine.UI;

/**
 * Class containing the data of an individual item sold in the shop,
 * as well as any behaviour for the animation and display of the item.
 */
public class ShopItem : MonoBehaviour
{
    public int cost;
    public string name;
	public Image itemImage;
	public bool isBuyable;
	public bool isOnSale;
}
