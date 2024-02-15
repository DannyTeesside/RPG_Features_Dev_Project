using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class ThirdPersonMovement : MonoBehaviour, ISaveable
{

    CharacterController controller;
    [SerializeField] Transform cam;
    Animator animator;

    [SerializeField] float speed = 6f;
    float x;
    float z;

    Vector3 velocity;
    float gravity = -9.81f;

    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale < 1f)
        {
            Walk();
        }

    }

    private void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        GroundCheck();
        if (Time.timeScale == 1f)
        {
            Walk();
        }
    }

    public void MovementActions()
    {
        Walk();
    }

    void Walk()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0, z);

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            animator.SetBool("isWalking", true);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }

        //if (Input.GetButtonDown("X"))
        //{
        //    animator.SetBool("isWalking", true);
        //}

        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    [System.Serializable]
    struct MovementSaveData
    {
        public SerializableVector3 position;
        public SerializableVector3 rotation;
    }

    public object CaptureState()
    {
        MovementSaveData data = new MovementSaveData();
        data.position = new SerializableVector3(transform.position);
        data.rotation = new SerializableVector3(transform.eulerAngles);

        return data;
    }

    public void RestoreState(object state)
    {
        MovementSaveData data = (MovementSaveData)state;
        controller.enabled = false;
        transform.position = data.position.ToVector();
        transform.eulerAngles = data.rotation.ToVector();
        controller.enabled = true;
    }
}
