using System.Collections;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public InventoryManager inventoryManager;  // Reference to the InventoryManager
    private bool isCollidingWithItem = false;  // Track if the player is in range of an item
    private GameObject currentItem;           // To store the current item being collected

    // When the player starts colliding with the collection object's trigger (CollectionCollider)
    void OnTriggerEnter(Collider other)
    {
        // Ensure we are detecting collision with the Cactus' trigger collider (which has the "Item" tag)
        if (other.CompareTag("Item"))
        {
            isCollidingWithItem = true;
            currentItem = other.gameObject;  // Store the current item
            Debug.Log("CollectionCollider started colliding with item.");
        }
    }

    // When the player continues colliding with the collection object's trigger
    void OnTriggerStay(Collider other)
    {
        if (isCollidingWithItem && other.CompareTag("Item"))
        {
            Debug.Log("CollectionCollider is staying in collision with item.");

            // If the player presses G for 2 seconds, start the collection
            if (Input.GetKey(KeyCode.G))
            {
                StartCoroutine(TryCollectItem());
            }
        }
    }

    // When the player exits the collision area (CollectionCollider leaves the Item's trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            isCollidingWithItem = false;
            currentItem = null;  // Clear the item reference
            Debug.Log("CollectionCollider exited item collision.");
        }
    }

    private IEnumerator TryCollectItem()
    {
        float timer = 0f;

        // Track time while G is held down and the CollectionCollider is in contact with the item
        while (timer < 2f && isCollidingWithItem && Input.GetKey(KeyCode.G))
        {
            timer += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // If 2 seconds have passed, collect the item
        if (timer >= 2f)
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        if (currentItem != null)
        {
            // Call the AddItemToInventory method (overridden by the child class)
            currentItem.GetComponent<ItemCollector>().AddItemToInventory();
            Destroy(currentItem);  // Destroy the item after collecting
        }
    }

    // This method will be overridden by the child class (e.g., Cactus)
    protected virtual void AddItemToInventory()
    {
        Debug.Log("Item collected!");
    }
}
