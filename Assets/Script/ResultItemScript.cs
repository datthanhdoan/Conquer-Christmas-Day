using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultItemScript : MonoBehaviour
{
    public GameObject CraftingTableItem;
    public void ShowCrafting(GameObject resultItem)
    {
        for (int i = 0; i < 4; i++)
        {
            int count = resultItem.GetComponent<ResultItem>().itemsToCraft.Length;
            if (i < count)
            {
                CraftingTableItem.transform.GetChild(i).gameObject.SetActive(true);
                Image temp = resultItem.GetComponent<ResultItem>().itemsToCraft[i].GetComponent<Image>();
                CraftingTableItem.transform.GetChild(i).GetComponent<Image>().sprite = temp.sprite;
            }
            else
            {
                CraftingTableItem.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}
