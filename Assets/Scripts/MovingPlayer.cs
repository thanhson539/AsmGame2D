using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingPlayer : MonoBehaviour
{
    public int soMau = 100;
    public Slider sliderPlayer;
    float speed = 5;
    bool isLeft, isRight, isTop;
    private bool facingRight;
    private float horizontalMove;
    private float jumpHeight = 5;
    private Rigidbody2D rb;
    private Animator _animator;
    public GameObject panelGameOver;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        facingRight = true;
        soMau = PlayerPrefs.GetInt("prefHP");
        sliderPlayer.value = soMau;
    }
   
    // Update is called once per frame
    void Update()
    {
        
        MovePlayer();
    }
    
    public void Top()
    {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    
    public void PoiterDownLeft()
    {
        _animator.SetBool("isRunLeft", true);
        isLeft = true;
    }
    
    public void PoiterUpLeft()
    {
        _animator.SetBool("isRunLeft", false);
        isLeft = false;
    }
    
    public void PoiterDownRight()
    {
        _animator.SetBool("isRunRight", true);
        isRight = true;
    }
    
    public void PoiterUpRight()
    {
        _animator.SetBool("isRunRight", false);
        isRight = false;
    }

    private void MovePlayer()
    {
        if (isLeft)
        {
            horizontalMove = -speed;
        }
        else if(isRight)
        {
            horizontalMove = speed;
        }
        else
        {
            horizontalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("boss"))
        {
            soMau -= 30;
            PlayerPrefs.SetInt("prefHP", soMau);
            sliderPlayer.value = soMau;
        }

        if (soMau <= 0)
        {
            PlayerPrefs.SetInt("prefHP", 100);
            Time.timeScale = 0;
            panelGameOver.SetActive(true);
        }
    }
}
