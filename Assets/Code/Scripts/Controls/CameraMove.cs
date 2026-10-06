using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    public Camera cam;
    // Move
    public float moveSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    // Zoom
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    // General controls
    PlayerControls controls;




    public float scroll;

    void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.PlayerCamera.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.PlayerCamera.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Up, down, left, right movement
        moveDirection = controls.PlayerCamera.WASD.ReadValue<Vector2>();
        transform.position += new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed, 0);

        // zoom
        scroll = controls.PlayerCamera.Zoom.ReadValue<float>();
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

    }
}
