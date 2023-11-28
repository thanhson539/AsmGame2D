using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChim : MonoBehaviour
{
    public float flyLeft, flyRight;
    public bool isRight, isDown;
    void Start()
    {
        isDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        chimBay();
    }

    private void chimBay()
    {
        var chimBay = transform.position.x;
        
        //Kiểm tra vị trí nhím di chuyển sang phải
        if (chimBay < flyLeft)
        {
            Flip();
        }
        
        //Kiểm tra vị trí nhím di chuyển sang trái
        if (chimBay > flyRight)
        {
            Flip();
        }

        if (isRight)
        {
            //Di chuyển sang phải
            transform.Translate(new Vector3(Time.deltaTime * 0.5f, 0, 0));
        }
        else
        {
            //Di chuyển sang trái
            transform.Translate(new Vector3(-Time.deltaTime * 0.5f, 0, 0));
        }
        
        if (isDown)
        {
            //Di chuyển sang phải
            transform.Translate(new Vector3(0, Time.deltaTime * 0.5f, 0));
        }
        else
        {
            //Di chuyển sang trái
            transform.Translate(new Vector3(0, -Time.deltaTime * 0.5f, 0));
        }
        
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isRight = !isRight;
        isDown = !isDown;
    }
}
