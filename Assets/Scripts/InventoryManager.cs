using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public Sprite itemIcon;
        public int healthRestoration; // Example effect
    }

    // Fixed inventory size
    public InventoryItem[] inventorySlots = new InventoryItem[5];  // Inventory slots
    public Image[] inventoryIcons = new Image[5];  // The UI images to represent inventory slots
    public Sprite cactusSprite;
    public Sprite stoneSprite;
    public Sprite heartSprite;

    void Start()
    {
        // Initially, no items in the inventory, so everything remains null
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = null;
            inventoryIcons[i].sprite = null;  // Empty the icons as well
        }

        Debug.Log("Inventory initialized as empty.");
    }

    void Update()
    {
        // Check for input keys 1-5 to activate items
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Alpha1 is key '1'
            {
                ActivateItem(i);
            }
        }
    }

    public void ActivateItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Length || inventorySlots[slotIndex] == null)
        {
            Debug.Log("No item in this slot.");
            return;
        }

        InventoryItem item = inventorySlots[slotIndex];
        Debug.Log($"Activated {item.itemName}, restoring {item.healthRestoration} health.");

        // Example effect: restore health (you would add your player health restoration logic here)

        // Remove the item after use
        inventorySlots[slotIndex] = null;
        inventoryIcons[slotIndex].sprite = null; // Clear the icon when the item is used
        Debug.Log($"Slot {slotIndex + 1} is now empty.");
    }

    public void AddItem(InventoryItem newItem)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                inventorySlots[i] = newItem;
                inventoryIcons[i].sprite = newItem.itemIcon; // Dynamically assign the sprite
                Debug.Log($"Added {newItem.itemName} to slot {i + 1}");
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }
}
