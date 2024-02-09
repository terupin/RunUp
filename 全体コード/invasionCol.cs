using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invasionCol : MonoBehaviour
{
    public int PUYOTETOcount;
    public bool Actjump;
    public bool NowJamp;
    public bool invasionTouchFlg;

    public float PUYOTETOpositionX;
    public float PUYOTETOpositionY;
    
    //---前方レイキャスト情報---//
    public Color rayColor = Color.red; // レイの色を指定
    public float raycastDistance; // レイキャストの距離を設定
    public Vector2 rayOrigin; // レイキャストを発射する位置を指定
    public Vector2 rayDirection; // レイキャストを上向きに発射
    public Vector2 raycastOffset; // 起点のずれを調整
    public bool DeathFlg; // ゲームオーバー判定フラグ

    //---後方レイキャスト情報---//
    public Color BuckrayColor = Color.blue; // レイの色を指定
    public float BuckraycastDistance; // レイキャストの距離を設定
    public Vector2 BuckrayOrigin; // レイキャストを発射する位置を指定
    public Vector2 BuckrayDirection; // レイキャストを上向きに発射
    public Vector2 BuckraycastOffset; // 起点のずれを調整

    // Start is called before the first frame update
    void Start()
    {
        PUYOTETOcount = 0; // ぷよテトカウントの初期化
        Actjump = false; // ジャンプ可能フラグの初期化
        invasionTouchFlg = false;
        PUYOTETOpositionX = 0; // キャラクターの下になるぷよテトのポジションX変数初期化
        PUYOTETOpositionY = 0; // キャラクターの下になるぷよテトのポジションY変数初期化
        DeathFlg = false;
        NowJamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //---前方レイキャスト処理---//
        // レイキャストを発射する位置を指定
        rayOrigin = (Vector2)transform.position + raycastOffset; // レイキャストの起点は自身の位置(x軸 0.5f ,y軸 -1.0f)
        rayDirection = Vector2.up; // 上向きにレイキャストを発射

        // レイキャストの可視化
        Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, rayColor);
        // レイキャストを実行して当たり判定を取得
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, raycastDistance);

        if (hit.collider != null)
        {
            // 別のオブジェクトと当たっている場合の処理
            DeathFlg = true;
        }

        //---後方レイキャスト処理---//
        // レイキャストを発射する位置を指定
        BuckrayOrigin = (Vector2)transform.position + BuckraycastOffset; // レイキャストの起点は自身の位置+(x軸 -0.5f ,y軸 -0.5f)
        BuckrayDirection = Vector2.down; // 下向きにレイキャストを発射

        // レイキャストの可視化
        Debug.DrawRay(BuckrayOrigin, BuckrayDirection * BuckraycastDistance, BuckrayColor);
        // レイキャストを実行して当たり判定を取得
        RaycastHit2D Buckhit = Physics2D.Raycast(BuckrayOrigin, BuckrayDirection, BuckraycastDistance);

        if (Buckhit.collider)
        {
            // 別のオブジェクトと当たっている場合の処理
            NowJamp = false;
        }
        else
        {
            // 別のオブジェクトと当たっていない場合の処理
            NowJamp = true;
        }

        // 1カウントかつジャンプ中じゃない時はジャンプ可能フラグをオンにする
        if (PUYOTETOcount == 1 && !NowJamp)
        {
            Actjump = true;
        }
        else if(PUYOTETOcount == 1 && NowJamp)
        {
            // ジャンプ中に別のオブジェクトと当たっている場合の処理
            DeathFlg = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TETRIS" || collision.gameObject.tag == "Puyo")
        {
            PUYOTETOcount = 1;
            PUYOTETOpositionX = collision.gameObject.transform.position.x;  // 当たったテトぶよのｘ座標を取得し格納
            PUYOTETOpositionY = collision.gameObject.transform.position.y;  // 当たったテトぶよのｙ座標を取得し格納
        } 
    }
}
