using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource audioSource;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityModifier;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AnimationClip walkClip;

    public bool gameOver;
    public bool gameStarts = false;
    public bool onGround = false;

    private int jumpCounter=0;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
        StartCoroutine(gameStartAnim());
    }

    void Update()
    {
        if (gameStarts==true)
        {
            Jump();
            Dash();
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCounter < 2)
        {
            jumpCounter++;
    
            if (jumpCounter == 1)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
            }
            else
            {
                playerRb.AddForce(Vector3.up * jumpForce / 1.5f, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig2");
            }

            dirtParticle.Stop();
            audioSource.PlayOneShot(jumpSound, 2.0f);
            onGround = true;
        }
    }
    
    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift)&&!onGround)
        {
            playerAnim.speed = 2.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)&&!onGround)
        {
            playerAnim.speed = 1f;
        }
    }

    IEnumerator gameStartAnim()
    {
        playerAnim.speed = 0.3f;
        yield return new WaitForSeconds(3);
        playerAnim.speed = 1f;
        gameStarts = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            jumpCounter = 0;
            onGround = false;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound, 2.0f);
        }

    }
}
