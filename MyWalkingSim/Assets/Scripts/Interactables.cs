using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class PreviousItem
{
    public Item requiredItem;
    public Item InteractionItem;
    public UnityEvent Oninteract;
}
public class Interactables : MonoBehaviour
{
    public Item item;
    public PreviousItem[] previousItem;
    public UnityEvent OnInteract;
    public UnityEvent CollectItem;
    [HideInInspector]public bool isMoving;
}
