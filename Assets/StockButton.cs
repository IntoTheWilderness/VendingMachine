using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockButton : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public TextMeshProUGUI buttonText;
    public Image buttonImage; // Reference to the Image component of the Button
    public InventoryManager inventoryManager; // Reference to the InventoryManager
    public InventoryMenu inventoryMenu; // Reference to your InventoryMenu
    public Sprite originalSprite;




    // Call this method to update the button text
    public void UpdateButtonText()

        //What is happening is that when a player makes a purchase, the InventoryItem that StockButton has just remains the same. The one that's being edited is the one
        //That's in the stocked inventory. It doesn't link to this inventoryItem.
        //So maybe I have to update each button's InventoryItem?
        //How does that work if they run out?
    {
        if (inventoryItem != null)
        {
            buttonText.text = inventoryItem.quantity.ToString();
            buttonText.color = Color.white;
        } else
        {
            buttonText.text = "";
        }
    }

    void Start()
    {

        originalSprite = buttonImage.sprite;
        // Find the InventoryManager in the scene
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene.");
        }

        inventoryMenu = FindObjectOfType<InventoryMenu>();

    }


    public void UpdateItemImage()
    {
        if (inventoryItem == null)
        {
            Debug.Log("Inventory item was empty on button");
            buttonImage.sprite = originalSprite;
        }
        else if (inventoryItem != null)
        {

            //Currently have each stock button homed with an Empty item, which just doesn't have anything happen.
            Debug.Log(inventoryItem.item);
            buttonImage.sprite = inventoryItem.item.itemSprite; // Update the button's image with the item's sprite
        }
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

        public void LoadNothing()
        {
        buttonText.text = "";
        buttonImage.sprite = originalSprite;
    }

        public void LoadFoodItem(InventoryItem newItem)
    {
        //if (newItem == null)
        //{
        //    //Test
        //}
        //else
        //{

            // Check if there was a previous item on the button
            //if (inventoryItem != null)
            //{
            //    // Return all remaining stock of the previous item to the inventory
            //    inventoryManager.TransferItemToInventory(inventoryItem.item, inventoryItem.quantity);

            //    // Remove the previous item from the stocked items
            //    inventoryManager.RemoveStockedItem(inventoryItem.item.itemID, inventoryItem.quantity);
            //}

            // Set the new item and update the button's visuals

            //int maxTransferToStocked = Mathf.Min(newItem.quantity, newItem.item.maxStockable);

            // Create a new InventoryItem instance for the stocked item
            //InventoryItem stockedItem = new InventoryItem(newItem.item, maxTransferToStocked);
            //inventoryManager.TransferItemToStocked(stockedItem.item, maxTransferToStocked);
            //inventoryItem = stockedItem;
            inventoryItem = newItem;
            UpdateItemImage();
            UpdateButtonText();
        //}
        inventoryMenu.GenerateItemIcons();
    }

    public void SetInventoryItem(InventoryItem newItem)
    {
        inventoryItem = newItem;
        UpdateButtonText();
        UpdateItemImage();
    }





    public void OnEnable()
    {
        UpdateButtonText();
        UpdateItemImage();
    }

    public void StockItem()
    {

    }

    public void RemoveItem()
    {

    }
}
