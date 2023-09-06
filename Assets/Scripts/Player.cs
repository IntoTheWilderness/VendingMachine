using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip vendingMachineSound; // Sound to play when the player interacts with the vending machine
    public AudioSource audioSource; // Reference to the AudioSource component
    public GameObject musicNotes;

    public bool canInteract = true; // To prevent rapid interaction

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the Space key and can interact
        if (Input.GetKeyDown(KeyCode.Space) && canInteract)
        {
            PlayCuteMusic();
        }
    }

    // Function to interact with the vending machine
    void PlayCuteMusic()
    {
        // Play the vending machine sound if an audio source is provided
        if (audioSource && vendingMachineSound)
        {
            audioSource.PlayOneShot(vendingMachineSound);
            // Calculate the position to instantiate the music note above the player
            Vector3 musicNotePosition = transform.position + new Vector3(0f, 2, 0f);

            // Instantiate the music note prefab at the calculated position
            GameObject musicNote = Instantiate(musicNotes, musicNotePosition, Quaternion.identity);

            musicNotes.SetActive(true);
        }

        // Perform other actions related to the vending machine here
        // For example, handle the item selection, purchase, etc.

        // Prevent rapid interaction for a short duration (to avoid spamming)
        canInteract = false;
        Invoke(nameof(EnableInteraction), 0.5f);
    }

    // Function to enable interaction after a short delay
    void EnableInteraction()
    {
        canInteract = true;
    }
}

