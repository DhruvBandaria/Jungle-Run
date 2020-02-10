using Cinemachine;
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
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private AudioClip alienSound;

    private Rigidbody2D rBody2d;
    private bool onGround = false;
    private bool colGround = false;
    private Animator animator;
    private bool isPlayedFirstAnimation;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        rBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isPlayedFirstAnimation = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPlayedFirstAnimation)
        {
            counter++;
            animator.SetFloat("Speed", 0f);
            if (counter == 60)
            {
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 150;
            }
            if (counter == 220)
            {
                cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                AudioSource.PlayClipAtPoint(alienSound, Camera.main.transform.position);

            }
            if (counter == 260)
            {
                enemy1.GetComponent<EnemyController>().startFlying();
                enemy2.GetComponent<EnemyController>().startFlying();
            }
            if (counter == 300)
            {
                enemy1.GetComponent<EnemyController>().startFlying();
                enemy2.GetComponent<EnemyController>().startFlying();
                isPlayedFirstAnimation = true;
            }
        }
        else
        {
            enemy1.transform.position = new Vector3(transform.position.x - 5, transform.position.y + 3, enemy1.transform.position.z);
            enemy2.transform.position = new Vector3(transform.position.x - 7, transform.position.y + 2, enemy1.transform.position.z);
            animator.SetFloat("Speed", 10f);
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

                rBody2d.velocity = new Vector2(0.0f, upForce);
                //rBody2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            }
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
