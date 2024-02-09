using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundTouch : MonoBehaviour
{
    public bool OnGround; // 地面に触れているかの判定
    public float NewGroundY; // 新しい地点の座標格納変数
    public float OldGroundY; // 新しい地点の座標から離れた時にその値を格納する変数

    // Start is called before the first frame update
    void Start()
    {
        OnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player 2")
        {
            OnGround = false;
            OldGroundY = NewGroundY;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name != "Player 2")
        {
            OnGround = true;
            NewGroundY = collision.gameObject.transform.position.y;
        }
    }
}
