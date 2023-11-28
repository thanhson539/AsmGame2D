using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NhanVat : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float tocDo = 5f;
    public float lucNhay = 7f;
    public String nextScreenName;
    public String backScreenName;
    public bool isDie;
    
    public Text txtCoin;
    [FormerlySerializedAs("txtHP")] public Text txtHp;
    public Text txtPlayerName;
    private int _tien;
    private int _mau = 100;

    public GameObject Pannels;
    public GameObject Pannels2;

    public AudioSource audiAnTien;
    public AudioSource audiBossCan1;
    public AudioSource audiBossCan2;
    public AudioSource audiDie;
    public AudioSource audiNhacNen;
    private bool _isPannels2NotNull;

    void Start()
    {
        _isPannels2NotNull = Pannels2 != null;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        KhoiDong();
        Pannels.SetActive(false);
        Pannels2 = GameObject.FindGameObjectWithTag("panel2");
        if (Pannels2 != null)
        {
            Pannels2.SetActive(false);
        }

        audiNhacNen.Play();
        audiAnTien.Stop();
        audiDie.Stop();
        audiBossCan1.Stop();
        audiBossCan2.Stop();
    }

    void Update()
    {
        move();
        PlayerNameAnim();
    }
    
    private void PlayerNameAnim()
    {
        Vector3 vector3 = transform.position;
        vector3.y += 1f;
        txtPlayerName.transform.position = vector3;
    }
    private void KhoiDong()
    {
        var oldName = PlayerPrefs.GetString("PlayerName");
        var oldHp = PlayerPrefs.GetInt("prefHP");
        var oldCoin = PlayerPrefs.GetInt("prefCoin");
        if (oldName != null)
        {
            txtPlayerName.text = oldName;
        }

        //Cập nhật hp cho nhân vật
        if (oldHp > 0)
        {
            _mau = oldHp;
            txtHp.text = "HP:" + _mau;
        }
        else
        {
            txtHp.text = "HP:" + 100;
        }

        //Cập nhật điểm cho nhân vật
        if (oldCoin > 0)
        {
            _tien = oldCoin;
            txtCoin.text = "Coin: " + _tien;
        }
        else
        {
            txtCoin.text = "Coin: " + 0;
        }
    }
    void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(moveHorizontal * tocDo, rb.velocity.y);
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunLeft", true);
        }
        
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunLeft", false);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunRight", true);
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("isRunRight", false);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, lucNhay);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Xử lý chuyển màn hình khi đi qua NextBackScreen
        if (other.GameObject().CompareTag("nextScreen"))
        {
            SceneManager.LoadScene(nextScreenName);
        }
        if (other.GameObject().CompareTag("backScreen"))
        {
            SceneManager.LoadScene(backScreenName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Xử lý va chạm với tien
        if (other.GameObject().CompareTag("coin"))
        {
            audiAnTien.Play();
            TongTien(+100);
            Destroy(other.GameObject(), 0.5f);
        }    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Xử lý va chạm với boss
        if (other.gameObject.CompareTag("boss") && !isDie)
        {
            // Bật hiệu ứng thu nhỏ trước khi chết
            // animator.SetBool("isDie", true);
            audiBossCan1.Play();
            TongMau(-30);
        }

        if (other.gameObject.CompareTag("my_nhan"))
        {
            if (Pannels2 != null)
            {
                Pannels2.SetActive(true);
            }
            audiNhacNen.Stop();
            audiAnTien.Stop();
            audiDie.Stop();
            audiBossCan1.Stop();
            audiBossCan2.Stop();
            stopGame();
        }
    }

    private void TongMau(int count)
    {
        _mau += count;
        if (_mau <= 0)
        {
            txtHp.text = "HP: " + 0;
            audiDie.Play();
            StartCoroutine(stopGame());
        }
        else
        {
            txtHp.text = "HP: " + _mau;
        }
        PlayerPrefs.SetInt("prefHP", _mau);
        
    }
    
    private void TongTien(int count)
    {
        _tien += count;
        txtCoin.text = "Coin: " + _tien;
        PlayerPrefs.SetInt("prefCoin", _tien);
    }
    
    //Detroy Player và stop Game
    private IEnumerator stopGame()
    {
        // Chờ 2 giây
        yield return new WaitForSeconds(0.1f);
        audiDie.Play();
        Pannels.SetActive(true);
        //Sau khi chờ 2 giây thì thực hiện:
        Destroy(gameObject); // 1:Hủy đối tượng nhân vật
        Time.timeScale = 0; // 2: Dừng game
        txtPlayerName.text = "";
    }
}
