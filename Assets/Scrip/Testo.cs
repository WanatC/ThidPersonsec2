using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testo : MonoBehaviour
{
    public float speed = 20f;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inpitX = Input.GetAxis("Horizontal");
        float inpitY = Input.GetAxis("Vertical");
        movement = new Vector3 (inpitX, 0 , inpitY) * speed * Time.deltaTime;
        transform.position += movement;
    }
}
