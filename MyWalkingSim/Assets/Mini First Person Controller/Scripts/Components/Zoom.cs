using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    public CinemachineCamera cam;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;
    [SerializeField] InputActionAsset inputActions;
    private InputAction ZoomAct;


    void Awake()
    {
        ZoomAct = inputActions.FindAction("Zoom");
        // Get the camera on this gameObject and the defaultZoom.
        //cam = GetComponent<CinemachineCamera>();
        if (cam)
        {
            defaultFOV = cam.Lens.FieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += ZoomAct.ReadValue<Vector2>().y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        cam.Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
