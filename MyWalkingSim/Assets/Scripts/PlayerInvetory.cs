using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInvetory : MonoBehaviour
{
    public List<Item> itens;
    
    public void AddItem(Item item)
    {
        if (itens.Contains(item))
        {
            return;
        }
        UiManager.instance.SetItems(item, itens.Count);
        itens.Add(item);
    }
}
