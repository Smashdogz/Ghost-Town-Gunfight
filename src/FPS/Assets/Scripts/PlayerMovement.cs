using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

public CharacterController controller;

public int maxHealth = 100;
public int currentHealth;

public HealthBar healthBar;


public float speed = 12f;
public float gravity = -9.81f;
public float jumpHeight = 3f;

public Transform groundCheck;
public float groundDistance = 0.4f;
public LayerMask groundMask;

Vector3 velocity;
bool isGrounded;

//When the level is loaded this sets the players health to be the max specified

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
 
    // This check to see if the players health reaches 0 or lower
    //if it is it changes to the GameOver scene and also 
    //enables the mouse cursor to appear on screen again
    void Update()
    {

        if(currentHealth <= 0){
            SceneManager.LoadScene("GameOver");
           Cursor.lockState = CursorLockMode.None; 
        }

//this check to see if the player is stading on a ground and if they are it
//enables the player to jump 
//it also allows the player to move in any direction around using the wasd keys
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
//checks to see if the player has taken damage and if it has it decreases the players health


    public void PlayerDamage(int damage){

                TakeDamage(damage);

    }


   public void TakeDamage(int damage)
    {
        currentHealth -= damage;



        healthBar.SetHealth(currentHealth);

    
}
}
