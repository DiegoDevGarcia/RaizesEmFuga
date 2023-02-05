using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    //move
    private float movement;
    public float speed = 5f;
    private bool FlipX;
    public AudioSource walkAudio;
    public AudioClip[] CharacterAudio;

    //jump
    private int nJump;
    public float jumpSpeed;
    public bool isGrounded;
    public AudioSource jumpAudio;
    public AudioSource doubleJumpAudio;

    //characters
    [HideInInspector] public bool controlarPlayer = true;
    [HideInInspector] public Animator anim;
    public Rigidbody2D rig;
    private SpriteRenderer Character;

    // private  int CurrentHealth = 3; *Possivel sistema de vida

    //Cenoura Eye
    private bool isVisible = true;
    public GameObject PlatformObj;
    public AudioSource showPlatformAudio;

    //Batata Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 2000f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 0.5f;
    [SerializeField] TrailRenderer tr;
    public AudioSource dashAudio;



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        Character = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        PlatformObj.gameObject.SetActive(false);
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
            if(gameObject.tag == "Cenoura")
            {
                nJump = 1;
            } else
            {
                nJump = 0;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
                jumpAudio.Play();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && nJump > 0)
            {
                nJump--;
                Jump();
                doubleJumpAudio.Play();
            }
        }

        //Cenoura Skill
        if (gameObject.tag == "Cenoura")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                seePlatform();
            }
            
        }

        //Batata Skill
        if(gameObject.tag == "Batata")
        {
            PlatformObj.gameObject.SetActive(false); 
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
            
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            this.transform.parent = collision.transform; // faz o player andar junto com a plataforma
        }

        if (collision.gameObject.layer == 9) // layer 9 = killZone
        {
            GameController.instance.showGameOver(); //mostra tela de game over
            Destroy(gameObject);
            return;

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
        if(collision.gameObject.layer == 8) // layer 8 = Ground
        {
            isGrounded= true;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.layer == 9) // layer 9 = killZone
        {
            GameController.instance.showGameOver();
            Destroy(gameObject);
            return;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) // layer 8 = ground
        {
            isGrounded= false;
            anim.SetBool("jump", true);
        }
    }


    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            anim.SetBool("walk", true); // inicia a animação walk
            FootAudio();
            if (FlipX)
            {
                Flip(); // manter o player na rotação 0 quando andando para direita
            }

        }

        else if (movement < 0f)
        {
            anim.SetBool("walk", true); // inicia a animação walk
            FootAudio();
            if (!FlipX)
            {
                Flip(); // flipa o player em 180º quando andando para esquerda
            }


        }

        else if (movement == 0f)
        {
            anim.SetBool("walk", false); // desliga a animação walk
        }
    }

    private void FootAudio()
    {
       // int randomAudio = Random.Range(0, CharacterAudio.Length);
      //  CharacterAudio[randomAudio] = GetComponent<AudioClip>();
        //audioSource.Play();
        
    }

    private void Flip()
    {
        FlipX = !FlipX;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }


    void Jump()
    {
        rig.velocity = Vector2.up * jumpSpeed;
        jumpAudio.Play();
    }

    //Cenoura Skill
    private void seePlatform()
    {
        PlatformObj.gameObject.SetActive(isVisible);
        showPlatformAudio.Play();
        isVisible = !isVisible;
    }

    //Batata Skill
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashAudio.Play();
        anim.SetBool("dash", true);
        float originalGravity = rig.gravityScale;
        rig.gravityScale = 0f;
        rig.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        anim.SetBool("dash", false);
        canDash = true;
    }

    // Mata o player;
    public void hitted()
    {
       // if (CurrentHealth == 0)
       // {
            GameController.instance.showGameOver();
            Destroy(gameObject);
            return;
        //}
        /*rig.AddForce(Vector2.left * 1000, ForceMode2D.Impulse);
        CurrentHealth--;*/
    }
}
