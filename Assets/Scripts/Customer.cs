using UnityEngine;
using System.Collections.Generic;


public class Customer : MonoBehaviour
{
    public int money = 100; // Amount of money the customer has
    public int purchaseAmount = 50; // Amount of money the player earns when the customer makes a purchase
    public float movementSpeed = 1.0f; // Speed at which the customer moves to the left
    public Transform player;
    public GameManager gameManager;

    private bool hasInteracted = false;
    private Transform playerPosition; // Reference to the player's transform
    private Rigidbody2D rb;
    private Vector2 initialDirection;

    public List<Item> favouriteItems = new List<Item>();
    public List<Item.ItemAttributes> desiredAttributes = new List<Item.ItemAttributes>();
    public Item currentDesire;
    public SpriteRenderer desireSpriteRenderer;
    public Sprite happySprite;
    public Sprite sadSprite;

    public float sweetDesire;
    public float sourDesire;
    public float bitterDesire;
    public float saltyDesire;
    public float umamiDesire;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = player.transform;
        rb = GetComponent<Rigidbody2D>();
        initialDirection = (playerPosition.position - transform.position).normalized;
        DetermineDesire();
        ReactToWeather();
    }



    void FixedUpdate()
    {
        if (!hasInteracted)
        {
            MoveTowardsMachine();
        }
        else
        {
            MoveAwayFromMachine();
        }
    }

    void MoveTowardsMachine()
    {
        if (playerPosition == null)
            return;

        // Calculate the direction towards the vending machine
        Vector3 targetDirection = (playerPosition.position - transform.position).normalized;

        // Move the customer towards the vending machine slowly
        Vector3 newPosition = transform.position + targetDirection * movementSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Border"))
        {
            Debug.Log("Touched the world border!");
            Destroy(gameObject); // Destroy the customer object
        }
        // Check if the customer collides with the player's collider
        if (other.CompareTag("Player") && !hasInteracted)
        {
            hasInteracted = true; // Prevent multiple interactions
            DeterminePurchase();
            //MakePurchase(currentDesire);
        }



    }

    public void ReactToWeather()
    {
        WeatherManager weatherManager;
        GameObject weatherManagerObject = GameObject.FindGameObjectWithTag("WeatherManager");
        weatherManager = weatherManagerObject.GetComponent<WeatherManager>();

        weatherManager.EffectCustomer(this);

        //So I have to pass the Customer to the WeatherManager, and then the Weather can effect them.

        //I want it to find the weather manager and then get the weather, and react to it.
        //I can put on the customer and hard code what each weather does,
        //Or I can get the Weather Manager to figure it out.

        //Okay, so, if they spawn then start looking for specific types of food during each weather? No, Thirst and Cold foods would compete...
        //So is the trick to keep getting items from the food web, and then trying to stock appropriately. Or they can pay more for guaranteed items.
        //I also want a trap door to capture bad guys.
        //Maybe you can figure out the qualities of weird food by selling it to people?

        //Maybe sometimes when a customer tries a food, they might stand around and say how great it is, and everyone else's favourite becomes that.
        //Or should they just buy multiple ones?
        //Maybe foods have distinct types. Could be represented by two colours. And most foods are 1 or 2 colours.
        //Maybe you have to mine the food web and there is just patches of certain types of things. Like Chicken Zone!
        //So I want the customer to look at each item and calculate a 'Worth' to them. Then rank and take the best. But the price has to factor.
        //Not every walk bmay end in a purchase
        //But some people will show what they want easily with a bubble above their head



        //How do you get weird stuff from the food web to
        //Could go off of 1
    }

    void MakePurchase(Item item)
    {
        // Get the GameManager script from the GameManager GameObject
        //GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

        //So I want the player to have preferences, and then they rate each item by desirability and choose the best one.
        //So the customer has to have item attribute preferences, and can grade 

        if (gameManager)
        {

            int purchaseAmount = item.suggestedValue;

            // Check if the customer has enough money to make a purchase
            if (money >= purchaseAmount)
            {
                // Player has enough money, make the purchase

                // Award the player with the purchaseAmount money
                //I don't like that this goes through the gameManager, it should go to the InventoryManager, but customers won't have access to that.
                //Maybe it should get a reference to it from the game manager, and then interact with it directly.


                gameManager.AddMoney(purchaseAmount);



                //This line needs to be modified. RemoveItem removes from the completeInventory, and not the stocked items. How do I get it from the stocked items?
                gameManager.inventoryManager.MakePurchase(currentDesire, 1);

                // Deduct the purchase amount from the customer's money
                money -= purchaseAmount;

                hasInteracted = true;
                Debug.Log("Updating buttons after purchase");
                gameManager.inventoryManager.UpdateButtons();
                DetermineHappyOrSad();


            }
        }
    }

    //So, based off the customer preferences, if their favourite item isn't there, this determines what item they are going to get.
    //So it needs a list of items in the vending machine for it to evaluate
    //It will cross examine each available items, and determine which one is correct.
    //Might include some fuzzing so it isn't 100% unless we need it to be (Quest items)
    //Customers will have prefabs, which will have set preferences. But we can randomize their preferences a little so they can hunt for specific foods
    //So either rank the options and choose the best, or it makes the favourite and compares against each other option, and if one is more favourite it takes that
    //

    //public void DeterminePurchase()
    //{
    //    //If they already have a Favourite Item that they are showing in the machine, they take it automatically
    //    //If Not, we have to calculate everything else


    //        Item bestItem = null;
    //        float bestScore = 0;


    //    //This can't be the gameManager.currentlyStockedItems. It needs to get to the Inventory Manager and then cycle through the item list. The problem will be about
    //    // when it does InventoryItems, it comes with a quantity value. 
    //        foreach (Item item in gameManager.currentlyStockedItems)
    //        {
    //            float itemScore = EvaluateItem(item);

    //            if (itemScore > bestScore)
    //            {
    //                bestScore = itemScore;
    //                bestItem = item;
    //            }
    //        }

    //        currentDesire = bestItem;
    //    Debug.Log("Customer chose " + bestItem.itemName);
    //    }

    public void DeterminePurchase()
    {
        Item bestItem = null;
        float bestScore = 0;

        // Assuming you have an InventoryManager reference in your GameManager
        InventoryManager inventoryManager = gameManager.GetInventoryManager();

        foreach (Item item in inventoryManager.GetAvailableItems())
        {
            if (item != null)
            {
                float itemScore = EvaluateItem(item);

                if (itemScore > bestScore)
                {
                    bestScore = itemScore;
                    bestItem = item;
                }
            }
        }

        if (bestItem != null)
        {
            currentDesire = bestItem;
            MakePurchase(currentDesire);
        }
        else
        {
            // Handle the case where no suitable item was found (e.g., display a message).
            Debug.Log("Customer couldn't find a suitable item.");
            desireSpriteRenderer.sprite = sadSprite;
        }
    }





    //gameManager.currentlyStockedItems 


    //public float CalculateDesirabilityOfItem(Item item)
    //{
    //    return 0f;
    //}

    public float EvaluateItem(Item item)
    {
        float score = 0;

        foreach (Item.ItemAttributes desiredAttribute in desiredAttributes)
        {
            if (item.qualities.Contains(desiredAttribute))
            {
                // Increase the score for a match
                score += 1.0f;
            }
        }

        //// Combine attributes score and taste score
        //score += tasteScore;
        //Debug.Log(item.itemName + " had a desirability of: " + score);
        return score;
    }


    //So this needs to be developed. Currently they always smile when they make a purchase. So I need some kind of thought process for them to like if they want it.
    //So, if they make no purchase, they should always be sad.
    //

    public void DetermineHappyOrSad()
    {
        desireSpriteRenderer.sprite = happySprite;
    }

    void MoveAwayFromMachine()
    {
        // Calculate the target position beyond the edge of the screen
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector2 targetPosition = (Vector2)transform.position + (initialDirection * 20f * screenBounds.x); // Adjust the factor (10f) as needed

        // Calculate the elapsed time since starting the movement
        //float elapsedTime = Time.time - startTime;

        // Calculate the interpolation factor based on elapsed time and move duration
        float t = Time.deltaTime; // Mathf.Clamp01(elapsedTime / moveDuration);

        // Smoothly interpolate the customer's position
        transform.position = Vector2.Lerp(transform.position, targetPosition, t);

    }

    public void DetermineDesire()
    {
        //Selects a random favourite item from their favourite item list and displays it
        //Sets the current Desire to a random favourite item.

        List<Item> favouriteItemsList = new List<Item>(favouriteItems);
        //Random random = new Random();
        int randomIndex = Random.Range(0, favouriteItemsList.Count);
        //int randomIndex = random.Next(favouriteItemsList.Count);
        // currentDesire = favouriteItemsList[randomIndex];

        SetDesire(favouriteItemsList[randomIndex]);
    }

    void SetDesire(Item newDesire)
    {
        //Sets the desire to the Item
        //Updates Desire Icon
        currentDesire = newDesire;
        UpdateDesireSprite(currentDesire.itemSprite);
    }

    public void UpdateDesireSprite(Sprite newDesireSprite)
    {
        desireSpriteRenderer.sprite = newDesireSprite;
    }


}
