using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryMenu : MonoBehaviour
{
    public Transform itemPanel; // Reference to the UI panel to hold the items
    public GameObject itemPrefab; // Reference to the item prefab
    public Transform gridLayout; // Reference to the grid layout where icons will be placed
    public InventoryManager inventoryManager;
    public List<InventoryItem> testList = new List<InventoryItem>();

    public Item testItem;

    void Start()
    {
        //AddItemToMenu(testItem, 6);
        GenerateItemIcons(inventoryManager.completeInventory);
    }

    // Create a method to add items to the menu
    public void AddItemToMenu(Item item, int quantity)
    {
        // Instantiate the item prefab
        GameObject newItem = Instantiate(itemPrefab, itemPanel);

        // Get references to the image and TextMeshPro components of the item prefab
        Image itemIcon = newItem.GetComponentInChildren<Image>();
        TextMeshProUGUI itemQuantityText = newItem.GetComponentInChildren<TextMeshProUGUI>();

        // Set the item's icon
        itemIcon.sprite = item.itemSprite;

        // Set the item's quantity
        itemQuantityText.text = "x" + quantity.ToString();
    }



    public void GenerateItemIcons(List<InventoryItem> unstockedInventoryItems)
    {
        // Clear the existing icons
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        testList = unstockedInventoryItems;

        // Create icons for each unstocked item
        foreach (InventoryItem inventoryItem in unstockedInventoryItems)
        {
            GameObject icon = Instantiate(itemPrefab, gridLayout);
            // Set the item's icon in the Image component
            icon.GetComponentInChildren<Image>().sprite = inventoryItem.item.itemSprite;
            // Set the quantity in the TextMeshPro component
            icon.GetComponentInChildren<TextMeshProUGUI>().text = inventoryItem.quantity.ToString();
            icon.GetComponentInChildren<DraggableItem2D>().item = inventoryItem;
        }
    }


    public void GenerateItemIcons()
    {
        // Clear the existing icons
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        ///testList = unstockedInventoryItems;

        // Create icons for each unstocked item
        foreach (InventoryItem inventoryItem in inventoryManager.completeInventory)
        {
            GameObject icon = Instantiate(itemPrefab, gridLayout);
            // Set the item's icon in the Image component
            icon.GetComponentInChildren<Image>().sprite = inventoryItem.item.itemSprite;
            // Set the quantity in the TextMeshPro component
            icon.GetComponentInChildren<TextMeshProUGUI>().text = inventoryItem.quantity.ToString();
            icon.GetComponentInChildren<DraggableItem2D>().item = inventoryItem;
        }
    }
}
