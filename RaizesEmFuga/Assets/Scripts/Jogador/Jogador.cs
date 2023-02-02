using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    private Rigidbody2D rig;
    private SpriteRenderer Character;
    public GameObject PlatformObj;

    //move
    private float movement;
    public float speed = 5f;

    //jump
    private int nJump;
    public float jumpSpeed;
    public bool isGrounded;

    //characters
    [HideInInspector] public bool controlarPlayer = true;
    [HideInInspector] public Animator anim;

    //Batata
    private float DashForce = 20;
    private bool onDash; 


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        Character = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (!controlarPlayer)
        {
            return;
        }

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

        if (gameObject.tag == "Cenoura")
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                PlatformObj.gameObject.SetActive(true);
            }
            
        }

        if(gameObject.tag == "Batata")
        {
            PlatformObj.gameObject.SetActive(false);
            if(Input.GetKeyDown(KeyCode.E))
            {
                onDash = true;
                Dash();
            }   
            
        }
    }

    void Jump()
    {
        rig.velocity = Vector2.up * jumpSpeed;

    }

    private void FixedUpdate()
    {
        if (!controlarPlayer)
        {
            return;
        }

        Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            anim.SetBool("walk", true); // inicia a animação walk
            Character.flipX = false; // manter o player na rotação 0 quando andando para direita
        }

        if (movement < 0f)
        {
            anim.SetBool("walk", true); // inicia a animação walk
            Character.flipX = true; // flipa o player em 180º quando andando para esquerda
        }

        if (movement == 0f)
        {
            anim.SetBool("walk", false); // desliga a animação walk
        }
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

        if(gameObject.tag == "Batata")
        {
            if(onDash)
            {
                if (collision.gameObject.tag == "hardWall")
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void Dash()
    {
        
        rig.velocity = Vector2.right * 2 * DashForce;
        
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
        if(collision.gameObject.layer == 8)
        {
            isGrounded= true;
            anim.SetBool("jump", false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded= false;
            anim.SetBool("jump", true);
        }
    }
}
