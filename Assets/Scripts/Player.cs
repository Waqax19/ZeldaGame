using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    private Animator animator;
    private Rigidbody rb;
    private float smoothTurn = 0.1f;
    private float velocity;
    private Transform cachedTransform;
    private bool isGrounded;
    private bool isAttacking;

    public GameObject player;

    public int healthPlayer = 3;



    void Start()
    {
        animator = GetComponent<Animator>();
        cachedTransform = transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
        HandleStopAttackInput();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A and D
        float vertical = Input.GetAxisRaw("Vertical"); // W and S
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(cachedTransform.eulerAngles.y, targetAngle, ref velocity, smoothTurn);

            rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);

            
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("IsWalking", false);
        animator.SetBool("isJumping", true);
        isGrounded = false;

        // You might want to set a timer to reset the isGrounded and IsJumping parameters
        StartCoroutine(ResetJumpParameters());
    }

    IEnumerator ResetJumpParameters()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the time based on your animation length
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("terrain"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("terrain"))
        {
            isGrounded = false;
        }
    }

    private void Attack()
    {
        
            animator.SetBool("IsAttack", true);
            isAttacking = true;

    }

   

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {

            Attack();

        }
        
    }

    public void  resetAttack()
    {
        isAttacking = false;
        Debug.Log("Resetting attack animation ..");

        player.GetComponent<Animator>().Rebind();

        animator.SetBool("IsAttack", false);
        
    }

    private void HandleStopAttackInput()
    {
        if(Input.GetMouseButtonDown(1) && isAttacking)
        {
            resetAttack();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        healthPlayer -= damageAmount;

        Debug.Log("Player took damage. Current health is : " + healthPlayer);
    }

}
