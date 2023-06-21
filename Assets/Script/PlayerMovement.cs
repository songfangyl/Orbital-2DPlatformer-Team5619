using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;
    private BoxCollider2D coll;
    private Bounds normalBound;
    private Bounds crouchBound;
    private Vector2 mousePos;
    private float directionX = 0f;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 standingSize;
    [SerializeField] private Vector2 crouchingSize;

    private enum MovementState {idle, running, jumping, falling};
    


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        normalBound = coll.bounds;
    }
    
    // Update is called once per frame
    private void Update()
    {
        //horizontalMove();
        jump();
        sprint();
        animationUpdate();
        crouch();
    }




    // keeping for animation
    public bool isMoving() 
    {
        return directionX != 0;
    }

    public void directionChange(float dir) 
    {
        directionX = dir;
    }

    public float airSpeedY() 
    {
        return rb.velocity.y;
    }
    
    public bool isGround() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void animationUpdate() 
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (mousePos.x >= rb.position.x) {
            sp.flipX = false;
        } else {
            sp.flipX = true;
        }
    }
    




    // need to remove
    private void horizontalMove() {
        directionX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
    }

    private void sprint() {
        if  (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            moveSpeed *= 2f;
        }
        if  (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
            moveSpeed /= 2f;
        }
    }

    private void jump() {
        if (Input.GetButtonDown("Jump") && isGround())
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }
    }

    private void crouch() {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
            coll.size = crouchingSize;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
            coll.size = standingSize;
        }
    }
}
