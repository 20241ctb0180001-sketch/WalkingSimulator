using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public float raydistance;
    [SerializeField] private Camera Mycam;

    void Start()
    {
        Mycam = Camera.main;
    }
    void Update()
    {
        veInteracao();
    }
    void veInteracao()
    {
        RaycastHit hit;
        Vector3 RayOrigin = Mycam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(RayOrigin, Mycam.transform.forward, out hit, raydistance))
        {
            Interactables interactable = hit.collider.GetComponent<Interactables>();
            if (interactable != null)
            {
                UiManager.instance.SetCursor(true);
            }
            else
            {
                UiManager.instance.SetCursor(false);
            }
        }
        else
        {
            UiManager.instance.SetCursor(false);
        }
    }
}
