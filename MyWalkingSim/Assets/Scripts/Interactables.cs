using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    public Item item;
    public UnityEvent OnInteract;
    public UnityEvent CollectItem;
    [HideInInspector]public bool isMoving;
}
