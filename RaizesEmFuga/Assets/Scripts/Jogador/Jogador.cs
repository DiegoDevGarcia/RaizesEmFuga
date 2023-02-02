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
    private bool FlipX;

    //jump
    private int nJump;
    public float jumpSpeed;
    public bool isGrounded;

    //characters
    [HideInInspector] public bool controlarPlayer = true;
    [HideInInspector] public Animator anim;

    //Batata Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 2000f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 0.5f;
    [SerializeField] TrailRenderer tr;



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

        if(isDashing)
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
            if(Input.GetKeyDown(KeyCode.E) && canDash)
            {
                StartCoroutine(Dash());
            }
            
        }
    }

    void Jump()
    {
        rig.velocity = Vector2.up * jumpSpeed;

    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rig.gravityScale;
        rig.gravityScale = 0f;
        rig.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (!controlarPlayer)
        {
            return;
        }
        if (isDashing)
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
            if(FlipX)
            {
                Flip(); // manter o player na rotação 0 quando andando para direita
            }
             
        }

        else if (movement < 0f)
        {
            anim.SetBool("walk", true); // inicia a animação walk
            if(!FlipX)
            {
                Flip(); // flipa o player em 180º quando andando para esquerda
            }
            
        }

        else if (movement == 0f)
        {
            anim.SetBool("walk", false); // desliga a animação walk
        }
    }

    private void Flip()
    {
        FlipX = !FlipX;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x,transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform; // faz o player andar junto com a plataforma
        }

        if (collision.gameObject.layer == 9)
        {
           // gameController.instance.showGameOver(); mostra tela de game over
            Destroy(gameObject);
        }

        if(gameObject.tag == "Batata" && isDashing)
        {
            
                if (collision.gameObject.tag == "hardWall")
                {
                    Destroy(collision.gameObject);
                }
            
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
        if(collision.gameObject.layer == 8)
        {
            isGrounded= true;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
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
