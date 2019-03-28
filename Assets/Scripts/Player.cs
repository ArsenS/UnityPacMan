using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider;
    private Animator animator;
    private Vector2 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        speed = 2.0f;
        direction = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    void FixedUpdate()
    {
        if (CanMove(direction))
        {
            rb2D.MovePosition(rb2D.position + (direction * speed) * Time.fixedDeltaTime);
        }
        print(rb2D.IsSleeping());
    }

    void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) && CanMove(Vector2.up))
        {
            direction = Vector2.up;
            animator.SetTrigger("movingUp");
        }
        if (Input.GetKey(KeyCode.DownArrow) && CanMove(Vector2.down))
        {
            direction = Vector2.down;
            animator.SetTrigger("movingDown");
        }
        if (Input.GetKey(KeyCode.LeftArrow) && CanMove(Vector2.left))
        {
            direction = Vector2.left;
            animator.SetTrigger("movingLeft");
        }
        if (Input.GetKey(KeyCode.RightArrow) && CanMove(Vector2.right))
        {
            direction = Vector2.right;
            animator.SetTrigger("movingRight");
        }
    }

    bool CanMove(Vector2 direction)
    {
        Vector2 pos = transform.position;
        circleCollider.enabled = false; // disable to avoid colliding with itself
        RaycastHit2D hit = Physics2D.BoxCast(pos, new Vector2(0.18f, 0.18f), 0f, direction, 0.05f);
        circleCollider.enabled = true;
        return (hit.collider == null);
    }
}
