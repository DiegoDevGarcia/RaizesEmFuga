using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    public Transform[] pontosDePatrulha;

    public float velocidadeMovimento;

    public int patrulhaDestino;

    private SpriteRenderer inimigo;



    private void Awake()
    {
        inimigo = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(patrulhaDestino == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontosDePatrulha[0].position, velocidadeMovimento * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, pontosDePatrulha[0].position) < 0.2f)
            {
                inimigo.flipX = false;
                patrulhaDestino = 1;
            }
        }

        if (patrulhaDestino == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontosDePatrulha[1].position, velocidadeMovimento * Time.deltaTime);

            if (Vector2.Distance(transform.position, pontosDePatrulha[1].position) < 0.2f)
            {
                inimigo.flipX = true;
                patrulhaDestino = 0;
            }
        }
    }
}
