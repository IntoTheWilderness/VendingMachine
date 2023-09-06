using UnityEngine;

public class DraggableItem2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public InventoryItem item;
    public bool isInteracting = false;

    private StockButton currentButton = null;

    private void OnMouseDown()
    {
        if (!isInteracting)
        {
            isInteracting = true;

            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}
