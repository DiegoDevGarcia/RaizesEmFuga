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
    public float jumpForce;
    private bool isJumping;
    private bool doubleJumping;
    private bool isBlowing = false;


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
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            // anim.SetBool("walk", true); inicia a animação walk
            spriteRenderer.flipX = false; // manter o player na rotação 0 quando andando para direita
        }

        if (movement < 0f)
        {
            // anim.SetBool("walk", true); inicia a animação walk
            spriteRenderer.flipX = true; // flipa o player em 180º quando andando para esquerda
        }

        if (movement == 0f)
        {
            // anim.SetBool("walk", false); desliga a animação walk
        }
    }

        void Jump()
        { 
    
            if (Input.GetButtonDown("Jump") && !isBlowing)
            {
                if (!isJumping)
                {
                    rig.AddForce(new Vector2(0f, jumpForce * 2), ForceMode2D.Impulse);
                    doubleJumping = true;
                }
                else
                {
                    if (doubleJumping)
                    {
                        rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        doubleJumping = false;
                        //anim.SetTrigger("doubleJump"); inicia Animação doubleJump
                    }
                }

            }
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false; // jogador não está pulando
            // anim.SetBool("jump", false); desliga a animação de jump
        }

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
        if (collision.gameObject.layer == 8)
        {
            isJumping = true; // jogador está pulando
           // anim.SetBool("jump", true); inicia a animação jump
        }

        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = null; // player saiu da plataforma
        }
    }
}
