using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SantaItem : MonoBehaviour
{
    public CraftingManager craftingManager;
    public Item itemInHand;

    public GameObject ItemInHandGameObject;
    public GameObject itemInHandVirtualInCanvas;
    public GameObject customCursor;

    private void Update()
    {
        if (craftingManager.currentResultItem != null)
        {
            float distance = Vector2.Distance(customCursor.transform.position, itemInHandVirtualInCanvas.transform.position);
            // Debug.Log("Distance: " + distance);
            if (distance < 70)
            {
                itemInHand = craftingManager.currentResultItem;
                Debug.Log("Item in hand: " + itemInHand.name);
                ItemInHandGameObject.GetComponent<Image>().sprite = itemInHand.GetComponent<Image>().sprite;
                ItemInHandGameObject.GetComponent<Item>().itemName = itemInHand.itemName;
                ItemInHandGameObject.SetActive(true);
                craftingManager.currentResultItem = null;
                craftingManager.ClearItems();
                customCursor.GetComponent<Image>().sprite = null;
                customCursor.SetActive(false);

            }
        }
        if (itemInHand == null)
        {
            ItemInHandGameObject.SetActive(false);
            ItemInHandGameObject.GetComponent<Image>().sprite = null;
        }
    }
    // Draw Gizmos
}
