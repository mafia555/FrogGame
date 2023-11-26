using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public bool flipRight = true;
    public Animator animator;
    public Rigidbody2D rb;
    SpriteRenderer sprite;

    void Start() {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
    Walk();
    Reflect();
    Jump();
    CheckingGround();
    }

    public Vector2 moveVector;
    public int speed = 3;

    void Walk() {
    moveVector.x = Input.GetAxisRaw("Horizontal");
    rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    animator.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }

    public bool faceRight = true;
    void Reflect() {
    if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
    transform.localScale *= new Vector2(-1, 1);
    faceRight = !faceRight;
        }
    }

    public bool onGround;
    public float jumpForce;
    private bool jumpControl;
    private int jumpIteration = 0;
    public int jumpValueIteration = 60;

    void Jump() {
        if (Input.GetKey(KeyCode.Space))
        {
           if (onGround) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl) {
            if (jumpIteration++ < jumpValueIteration) {
                rb.AddForce(Vector2.up  * jumpForce / jumpIteration);
            }
        }
        else { jumpIteration = 0; }
    }


    
    public LayerMask Ground;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    void CheckingGround() {
    onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
    animator.SetBool("onGround", onGround);
    }
}
