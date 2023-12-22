using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWiner : MonoBehaviour
{
    public static CheckWiner instance;
    public Camera defualtCamera;
    public Camera winerCamera;
    public bool isWiner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;
    public Transform playerRotation;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
         defualtCamera.enabled = true;
         winerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWiner) 
        {
            defualtCamera.enabled = false;
            winerCamera.enabled = true;
        }
    }
    private void LateUpdate()
    {
        if (target != null && isWiner)
        {
            Vector3 desiraedPostion = new Vector3(target.position.x + 0.2f, target .position.y + 0.1f, target .position.z + 2.2f);

            Vector3 smoothPositon = Vector3.Lerp(winerCamera.transform.position, desiraedPostion, smoothSpeed*Time.deltaTime);
            winerCamera.transform .position = smoothPositon;

            playerRotation.LookAt(new Vector3(playerRotation.position.x, playerRotation.position.y, playerRotation.position.z));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            isWiner = true;
    }
}
