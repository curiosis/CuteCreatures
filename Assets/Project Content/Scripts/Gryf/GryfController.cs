using UnityEngine;

public class GryfController : MonoBehaviour
{
    [Header("Ruch")]
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float runSpeed = 8f;
    [SerializeField]
    private float acceleration = 2f;
    [SerializeField]
    private float deceleration = 4f;
    [SerializeField]
    private float rotationSpeed = 50f;
    [SerializeField]
    private float turnRadiusFactor = 0.5f;

    [Header("Lot")]
    [SerializeField]
    private float liftSpeed = 5f;
    [SerializeField]
    private float glideSpeed = 10f;
    [SerializeField]
    private float descentSpeed = 5f;
    [SerializeField]
    private float stabilizationTime = 2f;

    private CharacterController controller;
    private Animator animator;
    private float currentSpeed = 0f;
    private bool isFlying = false;
    private bool isGrounded = true;
    private float stabilizationTimer = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleFlight();
    }

    private void HandleMovement()
    {
        if (isFlying) return;

        float moveDirection = Input.GetAxis("Vertical");
        float turnDirection = Input.GetAxis("Horizontal");

        if (moveDirection != 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) * moveDirection, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        if (turnDirection != 0 && currentSpeed > 0.1f)
        {
            float turnAmount = turnDirection * rotationSpeed * Time.deltaTime;
            float turnRadius = currentSpeed * turnRadiusFactor;

            transform.Rotate(0, turnAmount, 0);

            Vector3 offset = transform.forward * currentSpeed * Time.deltaTime + transform.right * turnAmount * Mathf.Deg2Rad * turnRadius;
            controller.Move(offset);
        }
        else
        {
            Vector3 move = transform.forward * currentSpeed * Time.deltaTime;
            controller.Move(move);
        }

        if (animator)
        {
            animator.SetFloat("Speed", Mathf.Abs(currentSpeed / runSpeed));

            if (currentSpeed > 0.1f)
            {
                animator.speed = Mathf.Clamp(currentSpeed / walkSpeed, 0.5f, 2f);
            }
            else
            {
                animator.speed = 1.0f;
            }
        }

        isGrounded = controller.isGrounded;
    }

    private void HandleFlight()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFlying && currentSpeed >= runSpeed * 0.5f)
        {
            isFlying = true;
            stabilizationTimer = 0f;

            if (animator)
            {
                animator.SetBool("IsFlying", true);
                animator.speed = 1f;
            }
        }

        if (isFlying)
        {
            Vector3 flightMovement = Vector3.zero;

            if (Input.GetKey(KeyCode.Space))
            {
                flightMovement = Vector3.up * liftSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                flightMovement = Vector3.down * descentSpeed * Time.deltaTime;
            }
            else
            {
                stabilizationTimer += Time.deltaTime;
                if (stabilizationTimer >= stabilizationTime)
                {
                    flightMovement = transform.forward * glideSpeed * Time.deltaTime;
                }
            }

            controller.Move(flightMovement);

            if (isGrounded)
            {
                isFlying = false;

                if (animator)
                {
                    animator.SetBool("IsFlying", false);
                }
            }
        }
    }
}
