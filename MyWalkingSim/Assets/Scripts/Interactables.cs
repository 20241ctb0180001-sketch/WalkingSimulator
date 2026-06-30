using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    public Item item;
    public UnityEvent OnInteract;
    [HideInInspector]public bool isMoving;
}
