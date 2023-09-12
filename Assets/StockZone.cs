using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockZone : MonoBehaviour
{

    public InventoryManager inventoryManager; // Reference to the InventoryManager
    public InventoryItem inventoryItem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //When a FoodTile touches the stock zone, it loads the item's max grabbable amount and puts it into the first available spot stockedInventory.
    //If it is full, the zone will do nothing.

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item touched Stock Zone"!);

        DraggableItem2D loadedItem = other.GetComponent<DraggableItem2D>();

            LoadFoodItem(loadedItem.item);
        

    }

    //*Note to self, this load is only happening when the customer tries to make  a purchase
    //No, incorrect, it's not immediately updating the tiles after you load a food
    //When a player tries to buy a food, it resets it, but placing it in the zone doesn't regenerate the tiles.

    public void LoadFoodItem(InventoryItem newItem)
    {
        if (newItem != null)
        {
            int maxTransferToStocked = Mathf.Min(newItem.quantity, newItem.item.maxStockable);

        // Create a new InventoryItem instance for the stocked item
        InventoryItem stockedItem = new InventoryItem(newItem.item, maxTransferToStocked);
        inventoryManager.TransferItemToStocked(stockedItem.item, maxTransferToStocked);
            //inventoryItem = stockedItem;

        // Check if there was a previous item on the button

            // Return all remaining stock of the previous item to the inventory
            //inventoryManager.TransferItemToStocked(inventoryItem.item, inventoryItem.quantity);

            // Remove the previous item from the stocked items
            //inventoryManager.RemoveStockedItem(inventoryItem.item.itemID, inventoryItem.quantity);
        }

    }

}
