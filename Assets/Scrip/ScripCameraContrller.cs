using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScripCameraContrller : MonoBehaviour
{

    [SerializeField] private float mouseSensitive = 3.0f;

    private float rotationX;
    private float rotationY;

    [SerializeField] private Transform target;
    [SerializeField] private float distanceFromtarget;

    private Vector3 currentRotation;
    private Vector3 smoothVelcoity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2(-10, 40);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* mouseSensitive;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensitive;

        rotationY += mouseX;
        rotationX -= mouseY;
        
        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelcoity , smoothTime);

        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromtarget;
    }
}
