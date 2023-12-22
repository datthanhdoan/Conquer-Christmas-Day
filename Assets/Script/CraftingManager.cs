using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Item currentResultItem { get; set; }
    public Item[] santaItem;
    public Image customCursor;
    public Slot[] slots;

    public List<Item> itemsList;
    public string[] recipes;
    public Item[] recipeResults;
    public Slot resultSlot;
    public bool isPause;
    private void Update()
    {
        if(isPause) return;
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in slots)
                {
                    float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestSlot = slot;
                    }

                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                itemsList[nearestSlot.index] = currentItem;
                currentItem = null;

                CheckForCreateRecipes();
            }

        }
    }
    void CheckForCreateRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;
        string currentRecipeString = "";
        foreach (Item item in itemsList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        for (int i = 0; i < recipes.Length; i++)
        {
            if (currentRecipeString == recipes[i])
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
            }
        }
    }
    
    public void ClearItems()
    {
        customCursor.gameObject.SetActive(false);
        foreach (Slot slot in slots)
        {
            slot.item = null;
            slot.gameObject.SetActive(false);
        }
        for (int i = 0; i < itemsList.Count; i++)
        {
            itemsList[i] = null;
        }
        resultSlot.item = null;
        resultSlot.gameObject.SetActive(false);
    }
    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemsList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreateRecipes();
    }
    public void OnMouseDownResultItem()
    {
        if (resultSlot.item != null)
        {
            currentResultItem = resultSlot.item;
            resultSlot.item = null;
            resultSlot.gameObject.SetActive(false);
            customCursor.sprite = currentResultItem.GetComponent<Image>().sprite;
            customCursor.gameObject.SetActive(true);
        }
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}

