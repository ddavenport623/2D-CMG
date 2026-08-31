using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    UnityEngine.Camera target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = target.transform.position;
    }
}
