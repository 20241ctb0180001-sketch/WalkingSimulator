using UnityEngine;
using UnityEngine.XR;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject cursor;
    void Awake()
    {
        instance = this;
    }

    public void SetCursor(bool state)
    {
        cursor.SetActive(state);

    }
}
