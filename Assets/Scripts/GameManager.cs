using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public int startingMoney = 100;
    public int maxCustomersPerDay = 10;
    public float dayDurationSeconds = 60f;

    public int money;
    public int currentDay;
    public int remainingCustomers;
    public float remainingTimer;
    public GameObject customerPrefab;
    public InventoryManager inventoryManager;
    public List<Item> currentlyStockedItems; //This is handled by the game manager, so... they need to communicate witht he inventoryManager

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI customersText;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        money = startingMoney;
        remainingCustomers = maxCustomersPerDay;
        remainingTimer = dayDurationSeconds;

        UpdateUI();

    }

    // Update the UI to display current values
    void UpdateUI()
    {
        moneyText.text = "$" + money.ToString();
        daysText.text = "Day " + currentDay.ToString();
        customersText.text = "Ppl Left: " + remainingCustomers.ToString();
        timerText.text = Mathf.RoundToInt(remainingTimer).ToString() + "s";
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            remainingTimer -= Time.deltaTime;

            if (remainingTimer <= 0f)
            {
                EndDay();
            }

            UpdateUI();

        if (GameObject.FindGameObjectsWithTag("Customer").Length == 0)
        {
                // Spawn a new customer
                SpawnCustomer();
            }
        }

    // Function to handle the purchase of an item
    public bool PurchaseItem(int itemPrice)
    {
        if (money >= itemPrice)
        {
            money -= itemPrice;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public InventoryManager GetInventoryManager()
    {
        return inventoryManager;
    }


    void SpawnCustomer()
    {
        // Get the screen boundaries in world coordinates
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        // Calculate a random Y position within the screen's height
        //float randomYPosition = Random.Range(-screenBounds.y, screenBounds.y);

        // Determine the side of the screen (left or right) from which the customer will come
        Vector3 spawnPosition;
        if (Random.Range(0, 2) == 0) // 0 for left, 1 for right
        {
            spawnPosition = new Vector3(-screenBounds.x, -2f, 0f);
        }
        else
        {
            spawnPosition = new Vector3(screenBounds.x, -2f, 0f);
        }

        // Spawn a new customer at the determined position
        Instantiate(customerPrefab, spawnPosition, Quaternion.identity);

    }

    public void EndDay()
    {
        if(IsGameOver()){
            EndGame();
        }
    }

    public void EndGame()
    {

    }

    // Function to handle the start of a new day
    public void StartNewDay()
    {
            currentDay = currentDay++;
            UpdateUI();
    }

    // Function to check if the game is over (no more days left)
    public bool IsGameOver()
    {
        return money <= 0;
    }
}
