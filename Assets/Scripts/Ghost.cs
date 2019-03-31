using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    private GameObject scatterModeTarget;

    private PolygonCollider2D polyCollider;
    protected Rigidbody2D rb2D;
    private Animator animator;

    protected float timeToEnterMaze = 0f;
    protected bool isActive = false;
    protected float moveTime = 1.0f;
    protected Vector2 currentDirection;
    protected Vector2 previousDirection = Vector2.zero;
    protected Vector2 target;

    public float speed = 1.0f;
       

    // Start is called before the first frame update
    protected virtual void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    { 
        if (isActive && CanMove(currentDirection))
        {
            UpdateAnimator();
            rb2D.MovePosition(rb2D.position + (currentDirection * 1.0f) * Time.fixedDeltaTime);
        }
    }
    

    protected void UpdateDirection(Vector2 newDirection)
    {
        if (currentDirection != Vector2.zero)
        {
            previousDirection = currentDirection;
        }
        currentDirection = newDirection;
    }

    void UpdateAnimator()
    {
        if (currentDirection == Vector2.up)
        {
            animator.SetTrigger("movingUp");
        }
        else if (currentDirection == Vector2.left)
        {
            animator.SetTrigger("movingLeft");
        }
        else if (currentDirection == Vector2.down)
        {
            animator.SetTrigger("movingDown");
        }
        else if (currentDirection == Vector2.right)
        {
            animator.SetTrigger("movingRight");
        }
    }
    
    protected bool CanMove(Vector2 direction)
    {
        Vector2 pos = transform.position;
        //polyCollider.enabled = false; // disable to avoid colliding with itself
        RaycastHit2D hit = Physics2D.BoxCast(pos, new Vector2(0.17f, 0.17f), 0f, direction, 0.05f);
        //polyCollider.enabled = true;
        if (hit.collider != null)
        {
            return hit.collider.isTrigger;
        }
        else
        {
            return (hit.collider == null);
        }
    }

    protected void EnterMaze()
    {
        isActive = true;
        rb2D.position += Vector2.up * 0.4f;
    }

    protected void StopMoving()
    {
        UpdateDirection(Vector2.zero);
    }

    protected bool CanChangeDirection()
    {
        bool canChange = false;
        if (IsValidNewDirection(Vector2.up) && CanMove(Vector2.up))
        {
            canChange = true;
        }
        if (IsValidNewDirection(Vector2.left) && CanMove(Vector2.left))
        {
            canChange = true;
        }
        if (IsValidNewDirection(Vector2.down) && CanMove(Vector2.down))
        {
            canChange = true;
        }
        if (IsValidNewDirection(Vector2.right) && CanMove(Vector2.right))
        {
            canChange = true;
        }
 
        return canChange;
    }

    protected bool IsValidNewDirection(Vector2 newDirection)
    {
        return (newDirection != previousDirection) && (newDirection != currentDirection);
    }

    protected Vector2 GetPosition()
    {
        return rb2D.position;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PacMan")
        {
            gameManager.PlayerHasDied();
        }
    }
}