using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    //I'm going to have to do something...
    //Food web!
    //But also, a Stocking interface for the Inventory Manager
    //So the manager should be used to control the item populator
    //I only want to show items that you have in your stock and their amounts
    
    
    //I sometimes want Slot 1 to be special. Any of them at any time could be great, but 1 should be perfect for testing. But that's an addiitional feature
    //Maybe there should be a bonus for being fully stocked?
    //Should I keep the stored items in a 6 part array, and then load the buttons off of that? I do have the Load Item function

    public GameObject restockPanel;
    public InventoryMenu inventoryMenu;
    public InventoryItem[] stockedItems = new InventoryItem[6];
    public List<InventoryItem> completeInventory = new List<InventoryItem>();
    //public List<StockButton> stockButtons = new List<StockButton>();
    public StockButton[] stockButtons = new StockButton[6];
    private int numStockedItems = 0;



    //Testing...
    void Start()
    {
        //TestStocking();
        //TransferItemToInventory(stockedItems[0].item, 5);
        UpdateButtons();
    }

    // Call this method to show the sale panel
    public void ShowRestockPanel()
    {
        restockPanel.SetActive(true);
        UpdateButtons();

        //Should refresh the fields after it gets shown
        //But this is currently not called...
        //Since each is a Button, I need to load the active items
        //So I need a list of active items and one for the back stock?
    }

    // Call this method to hide the sale panel
    public void HideRestockPanel()
    {
        restockPanel.SetActive(false);
    }

    public void ToggleRestockPanel()
    {
        if (!restockPanel.gameObject.activeSelf)
        {
            ShowRestockPanel();
        } else 
            HideRestockPanel();
            
    }

    public void AddItem(Item item, int quantity)
    {
        if (quantity < 0)
        {
            InventoryItem foundItem = null;

            foreach (InventoryItem inventoryItem in completeInventory)
            {
                if (inventoryItem.item.itemID == item.itemID)
                {
                    foundItem = inventoryItem;
                    break;
                }
            }

            if (foundItem != null)
            {
                foundItem.quantity += quantity;
            }
            else
            {
                completeInventory.Add(new InventoryItem(item, quantity));
            }
        }
    }

        public void RemoveItem(int itemID, int quantity)
    {
        InventoryItem foundItem = null;

        foreach (InventoryItem inventoryItem in completeInventory)
        {
            if (inventoryItem.item.itemID == itemID)
            {
                foundItem = inventoryItem;
                break;
            }
        }

        if (foundItem != null)
        {
            foundItem.quantity -= quantity;

            if (foundItem.quantity <= 0)
        {
                completeInventory.Remove(foundItem);
            }
        }
    }

    // Define a method to get the available items without quantities
    public List<Item> GetAvailableItems()
    {
        List<Item> availableItems = new List<Item>();

        foreach (InventoryItem inventoryItem in stockedItems)
        {
            availableItems.Add(inventoryItem.item);
        }

        return availableItems;
    }


    //This isn't working. Only loads in the 6th place.
    //Also dones't return items that were in that spot if you replace them.

    public void TransferItemToStocked(Item item, int quantity)
    {
        if (quantity > 0)
        {
            int index = 0;
            bool emptySpotFound = false;

            // Find the first empty spot in the stockedItems array
            for (int i = 0; i < stockedItems.Length; i++)
            {
                if (IsInventoryItemEmpty(stockedItems[i]))
                {
                    index = i;
                    emptySpotFound = true;
                    break;
                }
            }

            // If there is no empty spot, use the 6th spot
            if (!emptySpotFound)
            {
                index = 5;
                TransferItemToInventory(stockedItems[5].item, stockedItems[5].quantity);
            }

            stockedItems[index] = new InventoryItem(item, quantity);
            numStockedItems = Mathf.Min(numStockedItems + 1, 6);
            RemoveItem(item.itemID, quantity);
        }
        inventoryMenu.GenerateItemIcons();
        UpdateButtons();
    }




    public void TransferItemToInventory(Item item, int quantity)
    {
        // Find the stocked item with the given itemID
        int stockedItemIndex = -1;
        for (int i = 0; i < stockedItems.Length; i++)
        {
            if (stockedItems[i] != null && stockedItems[i].item.itemID == item.itemID)
            {
                stockedItemIndex = i;
                break;
            }
        }

        if (stockedItemIndex != -1)
        {
            // Remove the specified quantity from the stocked item
            stockedItems[stockedItemIndex].quantity -= quantity;

            // If the stocked item's quantity is less than or equal to 0, set the array element to null
            if (stockedItems[stockedItemIndex].quantity <= 0)
            {
                stockedItems[stockedItemIndex] = null;
            }

            // Add the item and quantity to the complete inventory
            InventoryItem completeItem = completeInventory.Find(x => x.item.itemID == item.itemID);
            if (completeItem != null)
            {
                completeItem.quantity += quantity;
            }
            else
            {
                completeInventory.Add(new InventoryItem(item, quantity));
            }
        }
        else
        {
            Debug.LogWarning("Item not found in stocked items.");
        }
        UpdateButtons();


    //if (completeItem != null)
    //{
    //    // Item is in the complete inventory, increase its quantity
    //    completeItem.quantity += quantity;
    //}
    //else
    //{
    //    // Item is not in the complete inventory, add it to the complete inventory list
    //    completeInventory.Add(new InventoryItem(item, quantity));
    //}

    //// Remove the transferred items from the stocked items list
    //RemoveStockedItem(item.itemID, quantity);
    }


public void RemoveStockedItem(int itemID, int quantity)
    {
        int index = -1;

        // Find the stocked item with the given itemID
        for (int i = 0; i < stockedItems.Length; i++)
        {
            if (stockedItems[i] != null && stockedItems[i].item.itemID == itemID)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            // Decrease the stocked item's quantity
            stockedItems[index].quantity -= quantity;

            // If the quantity reaches 0 or less, remove the item and shift the remaining items
            if (stockedItems[index].quantity <= 0)
            {
                for (int i = index; i < stockedItems.Length - 1; i++)
                {
                    stockedItems[i] = stockedItems[i + 1];
                }
                stockedItems[stockedItems.Length - 1] = null;
                numStockedItems = Mathf.Max(numStockedItems - 1, 0);
            }
        }

        UpdateButtons();
    }

    private bool IsInventoryItemEmpty(InventoryItem inventoryItem)
    {
        return inventoryItem == null || inventoryItem.item == null || inventoryItem.quantity == 0;
    }


    public void MakePurchase(Item item, int quantity)
    {
        // Check if the item is in the stocked items
        InventoryItem stockedItem = null;
        for (int i = 0; i < stockedItems.Length; i++)
        {
            if (stockedItems[i] != null && stockedItems[i].item.itemID == item.itemID)
            {
                stockedItem = stockedItems[i];
                break;
            }
        }

        if (stockedItem != null)
        {
            // Check if there are enough items in stock to make the purchase
            if (stockedItem.quantity >= quantity)
            {
                // Perform the purchase logic here

                // Deduct the purchased quantity from stocked items
                RemoveStockedItem(item.itemID, quantity);

                // You can add additional purchase logic here

                // For example, award the customer or deduct money
            }
            else
            {
                // Not enough items in stock
                Debug.LogWarning("Not enough items in stock to make the purchase.");
            }
        }
        else
        {
            // Item is not stocked
            Debug.LogWarning("Item is not stocked.");
        }

        UpdateButtons();
    }

    //public void UpdateButtons()
    //{
    //    foreach (StockButton button in stockButtons)
    //    {
    //        InventoryItem matchingItem = stockedItems.Find(item => item.item.itemID == button.inventoryItem.item.itemID);

    //        if (matchingItem != null)
    //        {
    //            button.SetInventoryItem(matchingItem);
    //        }


    //        button.UpdateButtonText(); // Call the UpdateButtonText() method on each button
    //    }
    //}


    public void UpdateButtons()
    {
        for (int i = 0; i < stockButtons.Length; i++)
        {
            if (i < stockedItems.Length && stockedItems[i] != null)
            {
                // Assign the Inventory Item to the Stock Button
                Debug.Log("Loaded an inventory item into a button!");
                stockButtons[i].LoadFoodItem(stockedItems[i]);
            }
            else
            {
                // If there are no more items to assign or the item is null, clear the button
                stockButtons[i].LoadNothing();
            }
        }
    }





}
