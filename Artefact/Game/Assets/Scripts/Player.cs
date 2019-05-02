using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public static float speed = 6.0F;
    public static float xspeed = 3.0F;
    public static float jumpSpeed = 8.0F;
    public static float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float MaxJump = 1;
    public float Jumps;


    void Update()
    {
        MaxJump = SpeedUpgrade.jumplevel;

        if (SpeedUpgrade.speedlevel == 2)
        {
            speed = 8.0f;
        }
        if (SpeedUpgrade.speedlevel == 3)
        {
            speed = 10.0f;
        }
        if (SpeedUpgrade.speedlevel == 4)
        {
            speed = 12.0f;
        }
        if (SpeedUpgrade.speedlevel == 5)
        {
            speed = 14.0f;
        }




        CharacterController controller = GetComponent<CharacterController>();


        if (controller.isGrounded)
        {
            Jumps = MaxJump;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection.z *= speed;
            moveDirection.x *= xspeed;
        }
        if (Jumps > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {


                //Feed moveDirection with input.
            }
            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
            {

                moveDirection.y = jumpSpeed;

                Jumps = Jumps - 1;
            }

        }





        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);

    }










    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "slow")
        {
            speed = 3.0F;
            //Or:
            //SceneManager.LoadScene (SceneIndex); //(without these: ", because it's a number - an int, not a string)
        }
        if (collision.gameObject.tag == "regular" && SpeedUpgrade.speedlevel == 1)
        {
            speed = 6.0f;

        }
        if (collision.gameObject.tag == "fast")
        {
            speed = 11.0F;
            //Or:
            //SceneManager.LoadScene (SceneIndex); //(without these: ", because it's a number - an int, not a string)
        }
    }

}

