using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isGameOver;

    public float speed = 6f;
    public float rotationSpeed;
    public float jump = 3f;
    public float jumpSpeed;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public HealthBar healthBar;

    float turnSmoothVelocity;

    private Vector3 velocity;
    private Animator animator;
    private CharacterController controller;
    private bool isGrounded;
    private bool isJumping;

    private float jumpButtonGracePeriod;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;


    void Start()
    {
        //HealthBar code starts
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //HealthBar code ends

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;

        isGameOver = false;
    }

   

    void Update()
    {

        // cia tik health tikrinimui ar veikia
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(3);
        }
        //

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        //animacijos
        if (direction != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // event
        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity);
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);
            

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
            ySpeed = jumpSpeed;
            animator.SetBool("IsJumping", true);
            isJumping = true;   
            jumpButtonPressedTime = null;
            lastGroundedTime = null; 
             }
        }
        else
        {
            controller.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && ySpeed < 0 ) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }
        //animacijos baigiasi



        //gravitacija
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }

    //AttackMode
    public  void TakeDamage(int damage)
    {
        //maxHealth -= damage;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
            isGameOver = true;
       
        
    }


    //Pasiema PlayerData ir SaveSystem duomenys
    //darbas su data
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);

        Debug.Log("Saved");

    }


    //kazkas su loadu, veikia, bet neveikia
    public void LoadPlayer()
    {
        PlayerData data =  SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        Debug.Log("Loaded");
    }



}
