using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public UnityEngine.Camera target;
    public float camBaseSize = 11f;
    private Vector3 initialScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // move
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    
        // zoom
        float currentSize = target.orthographicSize;
        float scaleFactor = currentSize / camBaseSize;
        transform.localScale = initialScale * scaleFactor;
    }
}
