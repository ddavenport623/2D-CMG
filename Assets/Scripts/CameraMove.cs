using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    // public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public InputAction playerControls;
    public float z;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
        transform.position += new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed, 0);
    }
}
