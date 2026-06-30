using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public TextMeshProUGUI captionstxt;
    public GameObject cursor;
    public GameObject backImage;
    public Image interactImage;
    void Awake()
    {
        instance = this;
    }

    public void SetCaptions(string text)
    {
        captionstxt.text = text;
    }

    public void SetCursor(bool state)
    {
        cursor.SetActive(state);

    }

    public void SetBackImage(bool state)
    {
        backImage.SetActive(state);
        if (!state)
        {
            interactImage.enabled = false;
        }
    }

    public void SetImage(Sprite sprite)
    {
        interactImage.sprite = sprite;
        interactImage.enabled = true;
    }
}
