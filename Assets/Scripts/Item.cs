using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    public enum ItemAttributes
    {
        Hot, Cold, Spicy, Alcoholic, Crunchy, Cheap, Chicken, Ginger, Beef, Sweet, Sour, Salty, Bitter, Unami, Perfect, Cake, Dairy, Chocolate, Dessert
    }

    public string itemName;
    public int itemID;
    public int suggestedValue;
    public Sprite itemSprite;
    public int maxStockable;
    public List<ItemAttributes> qualities = new List<ItemAttributes>();


}


