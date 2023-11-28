using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNhim : MonoBehaviour
{
    public float runLeft, runRight;
    private bool isRight;
    void Start()
    {
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        NhimDiChuyen();
    }

    private void NhimDiChuyen()
    {
        var nhimTransform = transform.position.x;
        
        //Kiểm tra vị trí nhím di chuyển sang phải
        if (nhimTransform < runLeft)
        {
            Flip();
        }
        
        //Kiểm tra vị trí nhím di chuyển sang trái
        if (nhimTransform > runRight)
        {
            Flip();
        }

        if (isRight)
        {
            //Di chuyển sang phải
            transform.Translate(new Vector3(Time.deltaTime * 0.7f, 0, 0));
        }
        else
        {
            //Di chuyển sang trái
            transform.Translate(new Vector3(-Time.deltaTime * 0.7f, 0, 0));
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isRight = !isRight;
    }
}
