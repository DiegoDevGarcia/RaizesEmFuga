using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private TargetJoint2D tj;
    private BoxCollider2D bx;

    public float speedPlatformX; // valor = 0 plataforma não ira se movimentar para esquerda nem direita
    public float speedPlatformY; // valor = 0 plataforma não ira se movimentar para cima nem para baixo

    public bool fallPlatform; // habilita e desabilita a queda da plataforma
    public float fallingTime; // tempo de cair a plataforma

    private void Start()
    {
        tj = GetComponent<TargetJoint2D>();
        bx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformRightLeft();
        MovePlatformUpDown();
    }

    private void MovePlatformRightLeft()
    {
        Vector3 movementX = new Vector3(1, 0, 0);
        transform.position += movementX * Time.deltaTime * speedPlatformX;
    }

    private void MovePlatformUpDown()
    {
        Vector3 movementY = new Vector3(0, 1, 0);
        transform.position += movementY * Time.deltaTime * speedPlatformY;
    }

    private void FallingPlatform()
    {
        tj.enabled = false;
        bx.isTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Parede") //inverte a direção da plataforma
        {
            speedPlatformX = -speedPlatformX;
            speedPlatformY = -speedPlatformY;
        }

        if(fallPlatform)
        {
            if (collision.gameObject.tag == "Player") // invoca o metodo FallingPlatform e destroi de acordo com o tempo fallingTime
            {
                Invoke("FallingPlatform", fallingTime);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //destroi a plataforma 
        {
            Destroy(gameObject);
        }
    }
}
