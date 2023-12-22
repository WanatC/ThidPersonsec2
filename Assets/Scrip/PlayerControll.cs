using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 20f;

    private Vector3 playerVloctity = Vector3.zero;
    [SerializeField] private float gravityValue = -8f;

    private bool groundPlayer;
    [SerializeField] private float jumpHeight = 2.5f;

    public Animator animator;

    public  static PlayerControll instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();    
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckWiner.instance.isWiner)
        {
            case true:
                animator.SetBool("Victory", CheckWiner.instance.isWiner); break;
                case false:
                    Movement();
                break;
        }
       
    }

   

    void Movement()
    {
        groundPlayer = characterController.isGrounded;
        if(characterController.isGrounded  && playerVloctity.y <-2f)
        {
            playerVloctity.y = -1f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0 , followCamera.transform.eulerAngles.y, 0)
            *new Vector3 (horizontalInput,0 , verticalInput);
                                            
        Vector3 movementDirection = movementInput.normalized;

        characterController.Move(movementDirection * playerSpeed * Time.deltaTime);

        if(movementDirection != Vector3.zero)
        {
            Quaternion desiredRototation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRototation, rotationSpeed * Time.deltaTime);
        }



        if(Input.GetButtonDown("Jump") && groundPlayer)
        {
            playerVloctity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        playerVloctity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVloctity * Time.deltaTime);

        animator.SetFloat("speed",Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.y) + Mathf.Abs(movementDirection.z));
        animator.SetBool("ground", characterController.isGrounded);
    }
}
