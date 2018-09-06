using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraController : MonoBehaviour {
    [Tooltip("The target transform to follow.")]
    [SerializeField] Transform target = null;
    [SerializeField] float minSpeed = 5;
    
    [Tooltip("Global maximum y value of the cameras postion.")]
    [SerializeField] float yMin = 0;
    [SerializeField] float yMax = 10;

    Vector3 offset;

    void Awake()
    {
        offset = new Vector3(0, 0, transform.position.z);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Calculating the new player position and following this
        Vector3 newPos = transform.position;
        Vector3 targetPosition = target.position + offset;
       
        // Creates a smoother animation - the farther the target from its original place, the faster the camera moves to catch it.
        if ((newPos - targetPosition).magnitude > minSpeed * Time.deltaTime)
        {
            Vector3 targetDir = targetPosition - newPos;
            targetDir.Normalize();
            newPos += targetDir * (Time.deltaTime * minSpeed);
            newPos.x = 0;
            newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);
        }

        // Prevents the camera from moving in the x direction, will keep the y position from moving too far outside what we want
        newPos.x = 0;
        newPos.y = Mathf.Clamp(newPos.y, yMin, yMax);

        transform.position = newPos;
    }

}
