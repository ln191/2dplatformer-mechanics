using UnityEngine;

public abstract class Character : MonoBehaviour
{


    [SerializeField]
    protected float movementSpeed = 5;

    protected bool facingRight;

    [SerializeField]
    private Transform kunaipos;

    [SerializeField]
    private GameObject kunai;
    public Animator MyAnimator { get; set; }
    public bool Attack { get; set; }

    // Use this for initialization
    public virtual void Start()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    /// <summary>
    /// Throws a Kunai in the way the player is facing
    /// </summary>
    /// <param name="value"></param>
    public virtual void ThrowKunai(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(kunai, kunaipos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(kunai, kunaipos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.left);
        }
    }

    /// <summary>
    /// Changes the direction of the Object
    /// </summary>
    public void ChangeDirection()
    {
        facingRight = !facingRight;

        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}