using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider;
    private Animator animator;
    private Vector2 direction;
    private bool isAlive = true;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        direction = new Vector2(0f, 0f);
        speed = 1.25f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    void FixedUpdate()
    {
        if (CanMove(direction) && isAlive)
        {
            UpdateAnimator();
            rb2D.MovePosition(rb2D.position + (direction * speed) * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetTrigger("stoppedMoving");
        }
     }

    void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) && CanMove(Vector2.up))
        {
            direction = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) && CanMove(Vector2.down))
        {
            direction = Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && CanMove(Vector2.left))
        {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) && CanMove(Vector2.right))
        {
            direction = Vector2.right;
        }
    }

    void UpdateAnimator()
    {
        if (direction == Vector2.up)
        {
            animator.SetTrigger("movingUp");
        }
        else if (direction == Vector2.left)
        {
            animator.SetTrigger("movingLeft");
        }
        else if (direction == Vector2.down)
        {
            animator.SetTrigger("movingDown");
        }
        else if (direction == Vector2.right)
        {
            animator.SetTrigger("movingRight");
        }
    }

    bool CanMove(Vector2 direction)
    {
        Vector2 pos = transform.position;
        circleCollider.enabled = false; // disable to avoid colliding with itself
        RaycastHit2D hit = Physics2D.BoxCast(pos, new Vector2(0.18f, 0.18f), 0f, direction, 0.045f);
        circleCollider.enabled = true;

        if (hit.collider != null && (hit.collider.name == "LeftTeleporter" || hit.collider.name == "RightTeleporter"))
        {
            return true;
        }
        return (hit.collider == null);
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void TeleportPlayer(GameObject teleporter)
    {
        if (teleporter.name == "LeftTeleporter")
        {
            TeleportRight();
        }
        if (teleporter.name == "RightTeleporter")
        {
            TeleportLeft();
        }
    }

    void TeleportLeft()
    {
        rb2D.position = rb2D.position + new Vector2(-4.5f, 0f);
    }

    void TeleportRight()
    {
        rb2D.position = rb2D.position + new Vector2(4.5f, 0f);
    } 

    public IEnumerator Death()
    {
        isAlive = false;
        animator.SetTrigger("hasDied");
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
