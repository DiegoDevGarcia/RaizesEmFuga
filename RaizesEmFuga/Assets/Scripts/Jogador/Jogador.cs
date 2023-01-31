using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public PlayerData playerData;

    private Vector2 movement;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    private float moveSpeed = 1.5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        movement = Vector2.zero;
        rb.velocity = movement;
    }
    void Start()
    {
        spriteRenderer.color =playerData.color;
    }

    
    void Update()
    {
        InputMovement();
    }
    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    void InputMovement()
    {
        movement.x = Input.GetAxis("Horizontal");
    }
}
