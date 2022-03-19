using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float horizontalLookSpeed;
    Vector3 velocity;
    bool isGrounded;

    
     public AudioClip footStepSound;
     public float footStepDelay;
 
     private float nextFootstep = 0;

    void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.eulerAngles = new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
            {
            velocity.y = -2f;
            }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("HorizontalLook");
        float z = Input.GetAxis("Vertical");


        Vector3 motion = Camera.main.transform.right * x + Camera.main.transform.forward * z;
        motion.y = 0f;
        controller.Move(motion * speed * Time.deltaTime);

        Vector3 changeView = new Vector3(0f, y * horizontalLookSpeed, 0f);
        transform.eulerAngles += changeView;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if ((Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f) && isGrounded)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0) 
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                nextFootstep += footStepDelay;
            }
        }
    }
}


