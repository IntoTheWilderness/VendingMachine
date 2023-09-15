using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class FoodNode : MonoBehaviour
{
    public Item item { get; set; }
    public List<FoodNode> connections { get; set; }
    public Image itemImage;

    // Method to load the sprite from the associated Item
    public void LoadItemSprite()
    {

        if (item != null && itemImage != null)
        {
            itemImage.sprite = item.itemSprite;
        }
        else
        {
            Debug.LogError("Item or itemImage is not assigned in FoodNode.");
        }
    }


    public FoodNode(Item newItem)
    {
        item = newItem;
        connections = new List<FoodNode>();
    }
}
