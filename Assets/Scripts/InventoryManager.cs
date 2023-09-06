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
    public List<InventoryItem> stockedItems = new List<InventoryItem>();
    public List<InventoryItem> completeInventory = new List<InventoryItem>();
    //public List<StockButton> stockButtons = new List<StockButton>();
    public StockButton[] stockButtons = new StockButton[6];



    //Testing...
    void Start()
    {
        //TestStocking();
        //TransferItemToInventory(stockedItems[0].item, 5);
    }

    public void TestStocking()
    {
        if (completeInventory.Count > 0)
        {
            // Stock 10 of the first item in completeInventory
            Item itemToStock = completeInventory[0].item;
            int quantityToStock = 10;

            TransferItemToStocked(itemToStock, quantityToStock);

            Debug.Log("Stocked " + quantityToStock + " of " + itemToStock.itemName);
        }
        else
        {
            Debug.LogWarning("Complete inventory is empty. Add items to test stocking.");
        }
    }


    // Call this method to show the sale panel
    public void ShowRestockPanel()
    {
        restockPanel.SetActive(true);

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


    public void TransferItemToStocked(Item item, int quantity)
    {
        // Check if the item is already stocked
        InventoryItem stockedItem = stockedItems.Find(x => x.item.itemID == item.itemID);

        if (stockedItem != null)
        {
            // Calculate how many more can be stocked up to the MaxStockable limit
            int maxStockable = item.maxStockable - stockedItem.quantity;
            int toStock = Mathf.Min(quantity, maxStockable);

            // Increase the stocked quantity
            stockedItem.quantity = toStock;

            // Remove the transferred items from the complete inventory
            RemoveItem(item.itemID, toStock);

            // Check if we need to transfer more, if quantity exceeds MaxStockable
            if (toStock < quantity)
            {
                TransferItemToStocked(item, quantity - toStock);
            }
        }
        else
        {
            // If the item is not stocked, add it to the stocked items list
            stockedItems.Add(new InventoryItem(item, quantity));

            // Remove the transferred items from the complete inventory
            RemoveItem(item.itemID, quantity);
        }
    }


    public void TransferItemToInventory(Item item, int quantity)
    {
        // Check if the item is in the complete inventory
        InventoryItem completeItem = completeInventory.Find(x => x.item.itemID == item.itemID);

        if (completeItem != null)
        {
            // Item is in the complete inventory, increase its quantity
            completeItem.quantity += quantity;
        }
        else
        {
            // Item is not in the complete inventory, add it to the complete inventory list
            completeInventory.Add(new InventoryItem(item, quantity));
        }

        // Remove the transferred items from the stocked items list
        RemoveStockedItem(item.itemID, quantity);
    }





    public void RemoveStockedItem(int itemID, int quantity)
    {
        // Check if the item is stocked
        InventoryItem stockedItem = stockedItems.Find(x => x.item.itemID == itemID);

        if (stockedItem != null)
        {
            // Item is stocked, decrease its quantity
            stockedItem.quantity -= quantity;
            //AddItem(stockedItem.item, quantity);

            // Remove from stocked items if the quantity reaches 0 or less
            if (stockedItem.quantity <= 0)
            {
                stockedItems.Remove(stockedItem);
            }
        }
    }

    public void MakePurchase(Item item, int quantity)
    {
        // Check if the item is in the stocked items
        InventoryItem stockedItem = stockedItems.Find(x => x.item.itemID == item.itemID);

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

    public void UpdateButtons()
    {
        foreach (StockButton button in stockButtons)
        {
            InventoryItem matchingItem = stockedItems.Find(item => item.item.itemID == button.inventoryItem.item.itemID);
            
            if (matchingItem != null)
            {
                button.SetInventoryItem(matchingItem);
            }


            button.UpdateButtonText(); // Call the UpdateButtonText() method on each button
        }
    }

    public void LoadStockButtons()
    {
        for (int i = 0; i < stockButtons.Length; i++)
        {
            if (i < stockedItems.Count)
            {
                // Assign the Inventory Item to the Stock Button
                stockButtons[i].LoadFoodItem(stockedItems[i]);
            }
            else
            {
                // If there are no more items to assign, clear the button
                stockButtons[i].LoadFoodItem(null);
            }
        }
    }



}
