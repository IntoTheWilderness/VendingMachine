using UnityEngine;

public class GameBorder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Customer touched boarder!");
        //System.out("Customer touched edge");
        // Check if the collider entering the game border is a customer
        if (other.CompareTag("Customer"))
        {
            // Destroy the customer that entered the game border
            Destroy(other.gameObject);
        }
    }
}
