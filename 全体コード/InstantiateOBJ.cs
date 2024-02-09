using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InstantiateOBJ : MonoBehaviour
{
    //ぷよテト格納配列
    public GameObject[] puyoteto;

    //ぷよテト生成オブジェクト
    private GameObject nowPuyoTeto;
    private TetPuyo_Range nowRange;
    private Tet_Rotate nowRotate;
    private InstantiateSkelton nowSkelton;

    //ハードドロップ後のぷよテト
    private GameObject prePuyoTeto;

    //次におけるぷよテト
    private GameObject nextPuyoTeto;
    private GameObject nextUI;
    private TetPuyo_Range nextRange;
    private Tet_Rotate nextRotate;
    private InstantiateSkelton nextSkelton;

    //x座標補正変数
    private float offsetX;
    //生成位置の元の座標
    private Vector3 BaseLocalPosition;
    private Vector3 BasePosition;

    //プレイヤーオブジェクト
    private GameObject Player;
    private player player_cs;
    // Start is called before the first frame update
    void Start()
    {
        //ネクスト表示系
        nextUI = GameObject.Find("ui_hold");
        nextPuyoTeto = Instantiate(puyoteto[9], nextUI.transform.position, Quaternion.identity);
        nextPuyoTeto.gameObject.transform.parent = nextUI.gameObject.transform;
        nextRange = nextPuyoTeto.GetComponent<TetPuyo_Range>();
        nextRotate= nextPuyoTeto.GetComponent<Tet_Rotate>();
        nextSkelton = nextPuyoTeto.GetComponent<InstantiateSkelton>();
        nextRange.enabled = false;
        nextRotate.enabled = false;
        nextSkelton.enabled = false;

        //最初に出すぷよテト
        nowPuyoTeto = Instantiate(puyoteto[11], gameObject.transform.position, Quaternion.identity);
        nowRange = nowPuyoTeto.GetComponent<TetPuyo_Range>();
        nowRotate = nowPuyoTeto.GetComponent<Tet_Rotate>();
        nowSkelton = nowPuyoTeto.GetComponent<InstantiateSkelton>();
        nowRange.enabled = true;
        nowRotate.enabled = true;
        nowSkelton.enabled = true;

        //ハードドロップ後のオブジェクト格納
        prePuyoTeto = nowPuyoTeto;

        //元ある生成位置の座標取得
        BaseLocalPosition = gameObject.transform.localPosition;

        //プレイヤーオブジェクト検索
        Player = GameObject.Find("Player(Clone)");
        player_cs = Player.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        //死亡したらスクリプト削除して動かないようにする
        if (player_cs.inColscript.DeathFlg)
        {
            Destroy(nowRange);
            Destroy(nowRotate);
            Destroy(nowSkelton);
        }
        else
        {
            //ぷよテトが下に落ちたら
            BasePosition = gameObject.transform.position;
            if (nowSkelton.hardDropFlag)
            {
                //置いた後に格納
                prePuyoTeto = nowPuyoTeto;
                prePuyoTeto.gameObject.AddComponent<InvisibleDestroy>();

                //生成位置を補正する
                float posx = OffsetPosition();

                //座標補正
                Vector3 offsetPos = new Vector3(posx, BasePosition.y, 0.0f);

                //ネクスト表示をうごかせるようにする
                ChangeNow(offsetPos);

                //ネクスト表示生成
                InstatiateNext();

                //元あった位置に戻す
                gameObject.transform.localPosition = BaseLocalPosition;
            }

            //今動かしているぷよテトは生成位置のY座標の位置に常置く
            Vector3 nowpos = nowPuyoTeto.transform.position;
            nowPuyoTeto.transform.position = new Vector3(nowpos.x, BasePosition.y, 0.0f);
        }
    }

    private float OffsetPosition()
    {
        offsetX = 0.5435f * 2.0f;
        //補正位置計算
        float PosX = prePuyoTeto.transform.position.x + offsetX;
        if (PosX > BasePosition.x)
        {
            offsetX = BasePosition.x - prePuyoTeto.gameObject.transform.position.x;
            offsetX = Mathf.Ceil(offsetX / 0.5435f);
            offsetX = offsetX * 0.5435f;
            PosX = prePuyoTeto.transform.position.x + offsetX;
        }
        return PosX;
    }

    private void ChangeNow(Vector3 pos)
    {
        //ネクスト表示の親子関係解除
        nextPuyoTeto.transform.parent = null;
        //ネクスト表示の位置を移動させる
        nextPuyoTeto.transform.position = pos;
        //すべてをオンにした状態で今のオブジェクトに格納
        nowPuyoTeto = nextPuyoTeto;
        //使えないようにしていたコンポーネントを起動させる
        nowRange = nowPuyoTeto.GetComponent<TetPuyo_Range>();
        nowRotate = nowPuyoTeto.GetComponent<Tet_Rotate>();
        nowSkelton = nowPuyoTeto.GetComponent<InstantiateSkelton>();
        nowRange.enabled = true;
        nowRotate.enabled = true;
        nowSkelton.enabled = true;
    }

    private void InstatiateNext()
    {
        //ぷよテトの判定
        int puyoORteto = Random.Range(0, 3);
        //ぷよテトの種類判定
        int index;
        switch (puyoORteto)
        {
            //ぷよぷよ
            case 0:
                index = Random.Range(0, 5);
                nextPuyoTeto = Instantiate(puyoteto[index], nextUI.transform.position, Quaternion.identity);
                break;
            //テトリス
            case 1:
            case 2:
                index = Random.Range(5, 12);
                nextPuyoTeto = Instantiate(puyoteto[index], nextUI.transform.position, Quaternion.identity);
                break;
        }
        nextPuyoTeto.gameObject.transform.parent = nextUI.gameObject.transform;
        nextRange = nextPuyoTeto.GetComponent<TetPuyo_Range>();
        nextRotate = nextPuyoTeto.GetComponent<Tet_Rotate>();
        nextSkelton = nextPuyoTeto.GetComponent<InstantiateSkelton>();
        nextRange.enabled = false;
        nextRotate.enabled = false;
        nextSkelton.enabled = false;
    }
}
