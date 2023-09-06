using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public float floatingSpeed = 0.5f; // Speed at which the music note floats up
    public float rotationSpeed = 50f; // Speed at which the music note rotates
    public float timeToDisappear = 2;

    public float disappearTime;
    private bool isDisappearing = false;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the time when the music note should disappear
        disappearTime = timeToDisappear; // Disappear after 0.4 seconds
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disappearTime <= 0)
            isDisappearing = true;
        // If the music note should be disappearing, handle the disappearance process
        if (isDisappearing)
        {
            Disappear();
        }
        else
        {
            // Otherwise, make the music note float up and rotate in place
            FloatingAndRotating();
            disappearTime -= Time.deltaTime;
        }
    }

    // Function to start the disappearance process
    public void StartDisappear()
    {
        isDisappearing = true;
    }

    // Function to handle the disappearance process
    private void Disappear()
    {
        // Destroy the music note after the specified delay
            Destroy(gameObject);

    }

    // Function to make the music note float up and rotate in place
    private void FloatingAndRotating()
    {
        // Make the music note float up
        transform.position += Vector3.up * floatingSpeed * Time.deltaTime;

        // Rotate the music note in place
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
