using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public TextMeshProUGUI captionstxt;
    public GameObject cursor;
    public GameObject backImage;
    public Image interactImage;
    public GameObject InvetoryImg;
    public TextMeshProUGUI[] inventoryItems;
    public TextMeshProUGUI infoText;
    [SerializeField] InputActionAsset inputActions;
    private InputAction OpenInvAct;
    void Awake()
    {
        instance = this;
        OpenInvAct = inputActions.FindAction("Previous");
    }

    void Update()
    {
        if (OpenInvAct.WasPressedThisFrame())
        {
            InvetoryImg.SetActive(!InvetoryImg.activeInHierarchy);
        }
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

    public void SetItems(Item item, int index)
    {
        inventoryItems[index].text = item.CollectMessage;
        infoText.text = item.CollectMessage;
        StartCoroutine(FadingText());
    }

    IEnumerator FadingText()
    {
        Color newcolor = infoText.color;
        while(newcolor.a < 1)
        {
            newcolor.a += Time.deltaTime;
            infoText.color = newcolor;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while(newcolor.a > 0)
        {
            newcolor.a -= Time.deltaTime;
            infoText.color = newcolor;
            yield return null;
        }
    }
}
