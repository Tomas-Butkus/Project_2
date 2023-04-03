using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private int turnSpeed;
    [Header("Dashing:")]
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldownTime;
    [SerializeField] private float dashSpeedIncrease;
    [SerializeField] private GameObject dashTrail;
    [Header("Audio:")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private float volume = 0.5f;

    private Animator playerAnimator;
    private CapsuleCollider capsuleCol;
    
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    public bool dashing;
    private bool canDash = true;
    
    Vector2 input;
    float angle;

    Quaternion targetRotation;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        capsuleCol = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        GetInput();
        
        Dash();
        
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return; //why? we're doing timers in Update
        
        CalculateDirection();
        Rotate();
        Move();
    }

    private void Dash()
    {
        if (dashing && !canDash) //if the player is currently dashing
        {
            capsuleCol.enabled = false;
            playerAnimator.SetBool("isDashing", true);
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashTime)
            {
                movementSpeed /= dashSpeedIncrease;
                dashTrail.SetActive(false);
                dashing = false;
                dashTimer = 0f;
            }
        } 
        else if (!dashing && !canDash) //the dash is over, begin cooldown
        {
            capsuleCol.enabled = true;
            playerAnimator.SetBool("isDashing", false);
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldownTime)
            {
                canDash = true;
                dashCooldownTimer = 0f;
            }
        }
        
        if (canDash) 
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) // dashing
            {
                audioSource.PlayOneShot(dashSound, volume);
                dashTrail.SetActive(true);
                movementSpeed *= dashSpeedIncrease;
                canDash = false;
                dashing = true;
            }
        }
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("speed", Mathf.Abs(input.x) + Mathf.Abs(input.y));
    }

    private void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    private void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
    }
}
