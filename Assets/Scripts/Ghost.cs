using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject scatterModeTarget;

    protected PolygonCollider2D polyCollider;
    protected Rigidbody2D rb2D;
    private Animator animator;

    protected int timerToEnterMaze, timerToChangePhase = 20;
    protected Vector2 currentDirection = Vector2.left;
    protected Vector2 previousDirection = new Vector2(0f, 0f);
    protected Vector2 target;
       

    // Start is called before the first frame update
    void Start()
    {
        timerToEnterMaze = 0;
        polyCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateAnimator();
        rb2D.MovePosition(rb2D.position + (currentDirection) * Time.fixedDeltaTime);
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

    protected void UpdateDirection(Vector2 newDirection)
    {
        previousDirection = currentDirection;
        currentDirection = newDirection;
    }

    protected bool CanMove(Vector2 direction)
    {
        Vector2 pos = transform.position;
        polyCollider.enabled = false; // disable to avoid colliding with itself
        RaycastHit2D hit = Physics2D.BoxCast(pos, new Vector2(0.18f, 0.18f), 0f, direction, 0.05f);
        polyCollider.enabled = true;
        if (hit.collider != null)
        {
            return hit.collider.isTrigger;
        }
        else
        {
            return (hit.collider == null);
        }
    }

    protected bool IsDifferentFromPreviousDirection(Vector2 newDirection)
    {
        return newDirection != previousDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PacMan")
        {
            gameManager.PlayerHasDied();
        }
    }
}