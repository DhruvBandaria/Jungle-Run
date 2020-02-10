using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float autoRunSpeed = 5.0f;
    [SerializeField] private float upForce = 10.0f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckerRadius = 0.15f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Transform colGroundChecker;
    [SerializeField] private GameObject loseColider;

    private Rigidbody2D rBody2d;
    private bool onGround = false;
    private bool colGround = false;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        colGround = ColGroundChecker();
        onGround = OnGroundChecker();

        loseColider.transform.position = new Vector3(transform.position.x, loseColider.transform.position.y, loseColider.transform.position.z);

        if (onGround)
        {
            animator.SetFloat("JumpForce", -1.0f);
        }
        else
        {
            animator.SetFloat("JumpForce", 1.0f);
        }

        if (!colGround)
        {
            rBody2d.velocity = new Vector2(autoRunSpeed, rBody2d.velocity.y);
        }
        else
        {
            rBody2d.velocity = new Vector2(0.0f, rBody2d.velocity.y);
            Debug.Log("colGround = " + colGround);
        }

        if (onGround && Input.GetAxis("Jump") > 0)
        {

            //rBody2d.velocity = new Vector2(0.0f, upForce);
            rBody2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool OnGroundChecker()
    {
        return Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, whatIsGround); ;
    }

    private bool ColGroundChecker()
    {
        return Physics2D.OverlapCircle(colGroundChecker.position, groundCheckerRadius, whatIsGround); ;
    }
}
