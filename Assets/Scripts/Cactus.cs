using UnityEngine;

public class Cactus : ItemCollector
{
    public Sprite cactusSprite;  // Reference to the cactus sprite

    // Override the AddItemToInventory method to add cactus to inventory
    protected override void AddItemToInventory()
    {
        InventoryManager.InventoryItem cactus = new InventoryManager.InventoryItem
        {
            itemName = "Cactus",
            healthRestoration = 10,
            itemIcon = cactusSprite
        };

        inventoryManager.AddItem(cactus);  // Add cactus to inventory
        Debug.Log("Cactus collected and added to inventory!");
    }
}