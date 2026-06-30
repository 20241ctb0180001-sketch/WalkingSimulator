using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public float raydistance;
    [SerializeField] private Camera Mycam;
    [SerializeField] private Transform objViwer;
    public UnityEvent OnView;
    public UnityEvent OffView;
    private bool taVendo;
    [SerializeField] InputActionAsset inputActions;
    private InputAction interAct;
    private InputAction rotateAct;
    private Interactables CurrentInteractable;
    private Vector3 PosOriginal;
    private Quaternion RotatOriginal;
    private Quaternion cameraRotationOriginal;
    private Quaternion playerRotationOriginal;
    private bool canFinish;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float objVel;
    private AudioPlayer audioPlayer;

    void Awake()
    {
        interAct = inputActions.FindAction("Interact");
        rotateAct = inputActions.FindAction("Look");
        audioPlayer = GetComponent<AudioPlayer>();
    }

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
        if (taVendo)
        {
            transform.rotation = playerRotationOriginal;
            if (Mycam != null)
            {
                Mycam.transform.rotation = cameraRotationOriginal;
            }
            if (CurrentInteractable.item.grabbable && Mouse.current.leftButton.isPressed)
            {
                RotateObj();
            }
            if (canFinish && Mouse.current.rightButton.isPressed)
            {
                FinishView();
            }
            return;
        }
        RaycastHit hit;
        Vector3 RayOrigin = Mycam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(RayOrigin, Mycam.transform.forward, out hit, raydistance))
        {
            Interactables interactable = hit.collider.GetComponent<Interactables>();
            if (interactable != null)
            {
                UiManager.instance.SetCursor(true);
                if (interAct.WasPressedThisFrame())
                {
                    if (interactable.isMoving) { return; }
                    playerRotationOriginal = transform.rotation;
                    if (Mycam != null)
                    {
                        cameraRotationOriginal = Mycam.transform.rotation;
                    }
                    CurrentInteractable = interactable;
                    CurrentInteractable.OnInteract.Invoke();
                    if (CurrentInteractable.item != null)
                    {
                        OnView.Invoke();
                        taVendo = true;
                        Interact(CurrentInteractable.item);
                        if (CurrentInteractable.item.grabbable)
                        {
                            PosOriginal = CurrentInteractable.transform.position;
                            RotatOriginal = CurrentInteractable.transform.rotation;
                            StartCoroutine(MovingObject(CurrentInteractable, objViwer.position));
                        }
                    }
                }
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

    void RotateObj()
    {
        float x = rotateAct.ReadValue<Vector2>().x;
        float y = rotateAct.ReadValue<Vector2>().y;
        CurrentInteractable.transform.Rotate(Mycam.transform.right, -Mathf.Deg2Rad * y * RotateSpeed, Space.World);
        CurrentInteractable.transform.Rotate(Mycam.transform.up, -Mathf.Deg2Rad * x * RotateSpeed, Space.World);
    }

    void Interact(Item item)
    {
        if (item.image != null)
        {
            UiManager.instance.SetImage(item.image);
        }
        audioPlayer.PlayAudio(item.audioClip);
        UiManager.instance.SetCaptions(item.text);
        Invoke("CanFinish", item.audioClip.length + 0.5f);
    }

    void CanFinish()
    {
        canFinish = true;
        if (CurrentInteractable.item.image == null && !CurrentInteractable.item.grabbable)
        {
            FinishView();
        }
        else
        {
            UiManager.instance.SetBackImage(true);
        }
        UiManager.instance.SetCaptions("");
    }

    void FinishView()
    {
        canFinish = false;
        taVendo = false;
        UiManager.instance.SetBackImage(false);
        if (CurrentInteractable.item.grabbable)
        {
            CurrentInteractable.transform.rotation = RotatOriginal;
            StartCoroutine(MovingObject(CurrentInteractable, PosOriginal));
        }
        OffView.Invoke();
    }

    IEnumerator MovingObject(Interactables obj, Vector3 position)
    {
        obj.isMoving = true;
        float timer = 0;
        while (timer < 1)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, position, Time.deltaTime * objVel);
            timer += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = position;
        obj.isMoving = false;
    }
}
