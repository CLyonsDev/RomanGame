using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    private Rigidbody characterRigidBody;

    public Transform[] FootLocs;
    public LayerMask GroundRayLayermask;
    private float groundRayLength = 0.05f;

    public PhysicMaterial GroundedPhysMat;
    public PhysicMaterial AirbornPhysMat;

    private Vector3 InputVec = Vector3.zero;

    private float speed = 0;
    private float walkSpeed = 3f;
    private float runSpeed = 5.75f;
    private float exhaustedSpeed = 2.25f;

    private Vector3 current;
    private Vector3 previous;
    public Vector3 rbVelocity;

    private bool canMove = true;
    private bool jumpEnabled = true;
    public bool CanJump = true;
    public bool isRunning = false;
    public bool canRun = true;

    bool canCheckGround = true;
    float groundCheckDelay = 0.1f;

    public float maxStamina = 135;
    public float stamina = 0;
    private float staminaDrainRun = 20; //Per second
    private float staminaGainIdle = 12; //Per second
    private float staminaRequiredBeforeRunAgain = 30;

    private float jumpStaminaRequirement = 20;

    public Color staminaBase;
    public Color staminaDrained;

    [SerializeField]
    private Image staminaPanel;

    void Start () {
        characterRigidBody = GetComponent<Rigidbody>();
        current = transform.position;

        staminaPanel.color = staminaBase;

        speed = walkSpeed;
        stamina = maxStamina;
	}

    void Update()
    {
        if (IsGrounded())
        {
            if(stamina >= jumpStaminaRequirement && canRun)
                CanJump = true;

            GetComponentInChildren<CapsuleCollider>().material = GroundedPhysMat;

            if (jumpEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Space) && CanJump)
                {
                    Jump();
                    StartCoroutine(GroundCheckDelay());
                    CanJump = false;
                }
            }
        }
        else
        {
            CanJump = false;
            GetComponentInChildren<CapsuleCollider>().material = AirbornPhysMat;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            //Sprint
            isRunning = true;
            speed = runSpeed;
        }
        else if ((Input.GetKeyUp(KeyCode.LeftShift) || !canRun) && isRunning)
        {
            isRunning = false;
            if (stamina <= 0)
                speed = exhaustedSpeed;
            else
                speed = walkSpeed;
        }
        if (isRunning && rbVelocity.magnitude > 1f)
        {
                stamina -= (staminaDrainRun * Time.deltaTime);

            if(stamina <= 0 && canRun)
            {
                canRun = false;
            }
        }

            if(stamina < 0)
            {
                stamina = 0;
                staminaPanel.color = staminaDrained;
                canRun = false;
                CanJump = false;
            }
            else if(stamina < maxStamina)
            {
                stamina += (staminaGainIdle * Time.deltaTime);

                if (stamina >= staminaRequiredBeforeRunAgain && !canRun)
                {
                    canRun = true;
                    CanJump = true;
                    staminaPanel.color = staminaBase;
                    speed = walkSpeed;
                }
            }
            else if(stamina > maxStamina)
            {
                stamina = maxStamina;
            }

        staminaPanel.fillAmount = (stamina / maxStamina);
    }
	
	void FixedUpdate () {

        if (canMove)
        {
            InputVec.x = Input.GetAxis("Horizontal");
            InputVec.z = Input.GetAxis("Vertical");

            float slowdownFactor = (Mathf.Abs(InputVec.x) + Mathf.Abs(InputVec.z)) / 2;

            Vector3 fb = transform.forward * InputVec.z;
            Vector3 lr = transform.right * InputVec.x;

            Vector3 dir = fb + lr;

            characterRigidBody.MovePosition(characterRigidBody.position + (dir * speed) * Time.fixedDeltaTime);
        }     

        previous = current;
        current = transform.position;

        if (current != previous)
        { 
            rbVelocity = (current - previous) / Time.fixedDeltaTime;
        }
        else
        {
            rbVelocity = Vector3.zero;
        }
        
	}

    private void Jump()
    {
        stamina -= jumpStaminaRequirement;
        characterRigidBody.AddForce(Vector3.up * 5.5f, ForceMode.Impulse);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
        jumpEnabled = canMove;
    }

    public bool IsGrounded()
    {
        bool grounded = false;

        for (int i = 0; i < FootLocs.Length; i++)
        {
            Ray ray = new Ray(FootLocs[i].position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, groundRayLength, GroundRayLayermask) && canCheckGround)
            {
                //We are touching the ground.
                grounded = true;
                break;
            }
        }

        return grounded;
    }

    private IEnumerator GroundCheckDelay()
    {
        canCheckGround = false;
        yield return new WaitForSeconds(groundCheckDelay);
        canCheckGround = true;
    }
}
