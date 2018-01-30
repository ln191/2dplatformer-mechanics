using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private IEnemyState currentState;
    public GameObject Target { get; set; }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.Execute();

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        float xDir = Target.transform.position.x - transform.position.x;

        if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
        {
            ChangeDirection();
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);
        transform.Translate(GetDiredtion() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDiredtion()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }
}