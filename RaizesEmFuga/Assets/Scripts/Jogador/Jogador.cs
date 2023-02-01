using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    private Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;

    //move
    private float movement;
    public float speed = 5f;

    //jump
    private int nJump;
    public float jumpSpeed;

    public bool isGrounded;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       
    }

    
    void Update()
    {
        if (isGrounded)
        {

            nJump = 1;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();

            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && nJump > 0)
            {
                nJump--;
                Jump();
            }

        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            // anim.SetBool("walk", true); inicia a anima��o walk
            spriteRenderer.flipX = false; // manter o player na rota��o 0 quando andando para direita
        }

        if (movement < 0f)
        {
            // anim.SetBool("walk", true); inicia a anima��o walk
            spriteRenderer.flipX = true; // flipa o player em 180� quando andando para esquerda
        }

        if (movement == 0f)
        {
            // anim.SetBool("walk", false); desliga a anima��o walk
        }
    }

    void Jump()
    {
        rig.velocity = Vector2.up * jumpSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform; // faz o player andar junto com a plataforma
        }

        if (collision.gameObject.tag == "agrotoxico")
        {
           // gameController.instance.showGameOver(); mostra tela de game over
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null; // player saiu da plataforma
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded= false;
        }
    }
}
