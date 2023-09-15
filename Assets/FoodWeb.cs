using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodWeb : MonoBehaviour
{
    public List<Item> itemPool;
    public int foodWebSize;

    public void GenerateFoodWeb(int size)
    {
        foodWebSize = size;
        for (int i = 0; i < size; i++)
        {
            //FoodNode node = CreateNode();
            //node.item = GetRandomItem();
            // Assign other properties to the node as needed
        }
    }

    //Alright, so I want to make a food web of size X, and then randomly put items in each spot
    //I want them connected to a center node. And then we start building the tree off of that.
    //The center node can have at most 4 nodes connected to it, and every other node can only have 2
    //The depth of the tree cannot go bigger than 4
    //When someone clicks on the node, the player buys it for an amount I have to figure out
    //Once the item is bought, it is put into the completeInventory
    //Different areas will have different items available in their foodwebs

    //public FoodNode CreateNode()
    //{
    //    // Logic to create a FoodNode
        
    //}

    //public FoodNode CreateCenterNode()
    //{
        
    //    FodeNode foodno
    //    return
    //}

    private Item GetRandomItem()
    {
        if (itemPool.Count == 0)
        {
            Debug.LogError("Item pool is empty.");
            return null;
        }

        int randomIndex = Random.Range(0, itemPool.Count);
        Item randomItem = itemPool[randomIndex];
        itemPool.RemoveAt(randomIndex); // Remove the item to avoid duplicates
        return randomItem;
    }
}


//public class FoodNode
//{
//    public Item item { get; set; }
//    public List<FoodNode> connections { get; set; }

//    public FoodNode(Item newItem)
//    {
//        item = newItem;
//        connections = new List<FoodNode>();
//    }
//}
