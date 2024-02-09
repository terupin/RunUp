using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Final : MonoBehaviour
{
    private int Layer_Count;  //流れる背景画像の数
    private float Length;  //子オブジェクトの背景画像の大きさ

    public float Scroll_Speed;  //スクロールスピード
    public bool Sky = false;  //背景が空か否か

    private Transform ParentTransform;  //親オブジェクトのTransform

    //プレイヤーのデータを取得
    private GameObject Player;
    private player player_cs;

    // Start is called before the first frame update
    void Start()
    {
        Layer_Count = this.transform.childCount;  //子オブジェクトの数
        ParentTransform = this.transform;  //親のポジションを格納
        Length = ParentTransform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;  //背景画像サイズ

        Player = GameObject.Find("Player(Clone)");
        player_cs = Player.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが倒れたら消す
        if(player_cs.inColscript.DeathFlg)
        {
            Destroy(this);
        }

        //左端のX軸の取得
        Vector3 CameraRectMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));//カメラの左下の座標

        if (Player)
        //背景画像ループ
        {
            //子オブジェクトの数だけfor分を回す
            foreach (Transform Child in ParentTransform)
            {
                Vector3 Pos = Child.transform.position;  //子オブジェクトのポジション

                if (!Sky)  //空以外はスクロール
                {
                    //スクロール処理
                    Pos.x -= Scroll_Speed * Time.deltaTime;

                    //画像が消えたら右に移動させる
                    if (Pos.x + Length / 2 < CameraRectMin.x)
                    {
                        Pos.x += Length * Layer_Count;
                    }

                }
                else  //空の場合
                {
                    Pos.x = Camera.main.gameObject.transform.position.x;  //カメラのX座標に移動
                }
                Child.transform.position = Pos;
            }
        }
    }
}
