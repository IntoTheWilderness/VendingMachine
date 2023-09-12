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


    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item touched Stock Button"!);

        DraggableItem2D loadedItem = other.GetComponent<DraggableItem2D>();

        //if (!other.GetComponent<DraggableItem2D>().IsInteracting)
        {
            LoadFoodItem(loadedItem.item);
        }

        //StockButton droppedItem = other.GetComponent<StockButton>();



        //    LoadFoodItem(loadedItem.item);

    }

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
            inventoryManager.TransferItemToStocked(inventoryItem.item, inventoryItem.quantity);

            // Remove the previous item from the stocked items
            //inventoryManager.RemoveStockedItem(inventoryItem.item.itemID, inventoryItem.quantity);
        }

    }

}
