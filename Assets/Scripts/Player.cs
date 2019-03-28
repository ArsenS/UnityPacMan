using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private CircleCollider2D circleCollider;
    private Animator animator;
    private Vector2 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        speed = 1.0f;
        direction = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }

    void FixedUpdate()
    {

    }

    void GetDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetTrigger("movingUp");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetTrigger("movingDown");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetTrigger("movingLeft");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetTrigger("movingRight");
        }
    }
}
