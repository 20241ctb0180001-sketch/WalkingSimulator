using UnityEngine;
using TMPro;

public class AbrirTampinha : MonoBehaviour
{
    
    private Animator anim;

    private bool taEmAlcance = false;

    [SerializeField] private TextMeshProUGUI codetxt;
    string codeTxtvalue = "";
    public string senha;
    public GameObject painelcdg;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        codetxt.text = codeTxtvalue;

        if(codeTxtvalue == senha)
        {
            anim.SetTrigger("abrirtampa");
            painelcdg.SetActive(false);
        }

        if(codeTxtvalue.Length >= 6)
        {
            codeTxtvalue = "";
        }

        if(Input.GetKey(KeyCode.E) && taEmAlcance == true)
        {
            painelcdg.SetActive(true);
        }

        if(painelcdg.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            taEmAlcance = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        taEmAlcance = false;
        painelcdg.SetActive(false);
    }

    public void AddDigito(string digito)
    {
        codeTxtvalue += digito;
    }
}
