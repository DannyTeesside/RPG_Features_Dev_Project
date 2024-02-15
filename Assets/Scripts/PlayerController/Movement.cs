using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.PlayerController
{
    public class Movement : MonoBehaviour
    {

        [SerializeField] float speed = 10;
        float x;
        float z;
        public bool canMove = true;
        CharacterController characterController;
        Animator animator;

        Vector3 velocity;
        float gravity = -9.81f;


        
        
        

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }



        //Player - character controls

        public void MovementActions()
        {
            Walk();
        }

        //Action functions
        void Walk()
        {
            if (canMove)
            {

                x = Input.GetAxis("Horizontal");
                z = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(x, 0, z);

                if (z != 0)
                {
                    
                    animator.SetBool("isWalking", true);
                }
                else if (x != 0)
                {
                    
                    animator.SetBool("isWalking", true);
                }

                else
                {
                    animator.SetBool("isWalking", false);
                }

                characterController.Move(movement * speed * Time.deltaTime);

                if (z != 0)
                {
                    transform.rotation = Quaternion.LookRotation(movement);
                }

                else if (x != 0)
                {
                    transform.rotation = Quaternion.LookRotation(movement);
                }

            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }



        // Update is called once per frame
        void Update()
        {

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

    }
}
