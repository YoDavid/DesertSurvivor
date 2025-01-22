using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public GameObject collectionTrigger;  // Reference to the CollectionTrigger GameObject
    public ItemCollector itemCollector;   // Reference to the ItemCollector script on the player
    private bool isCollecting = false;    // To track if player is holding the G key

    void Start()
    {
        // Initially disable the collider
        collectionTrigger.SetActive(false);
    }

    void Update()
    {
        // When the player presses G, activate the collection collider
        if (Input.GetKey(KeyCode.G) && !isCollecting)
        {
            isCollecting = true;
            collectionTrigger.SetActive(true);  // Enable the collection collider
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            isCollecting = false;
            collectionTrigger.SetActive(false); // Disable the collection collider
        }
    }
}