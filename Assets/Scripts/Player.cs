using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float groundRadius;

    private bool airControl;

    private bool jump;

    [SerializeField]
    private float jumpForce;

    public Rigidbody2D MyRigidbody { get; set; }

    private bool onGround;
    private bool slide;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        onGround = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            MyAnimator.SetBool("slide", true);
            slide = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            MyAnimator.SetTrigger("throw");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");

            jump = true;
        }
    }

    private void HandleMovement(float horizontal)
    {
        //If player is falling
        if (MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("landing", true);
        }
        //If player is no longer falling
        else if (MyRigidbody.velocity.y == 0)
        {
            MyAnimator.SetBool("landing", false);
        }
        //Player Jumps while on the ground
        if (onGround && jump)
        {
            onGround = false;
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }

        if (!slide)
        {
            // Player movement      OBS!!  make so player cannot move so fast to the sides while in the air.
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        // Player slide,   OBS!! one can not move while slide only use the add force from movement
        if (slide && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            if (MyRigidbody.velocity.x == 0)
            {
                MyAnimator.SetBool("slide", false);
                slide = false;
            }
        }

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public override void ThrowKunai(int value)
    {
        if (!onGround && value == 1 || onGround && value == 0)
        {
            base.ThrowKunai(value);
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Change the weight of the animation layers, between ground and air layer
    /// </summary>
    private void HandleLayers()
    {
        if (!onGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    /// <summary>
    /// Method check to see if the ground points collides with ground obj in set layer mask
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}