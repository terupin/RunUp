using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class InstantiateSkelton : MonoBehaviour
{
    public GameObject skelton;                      //透明なオブジェクト
    public GameObject Input;                         //プレイヤーインプット格納オブジェクト
    private PlayerInput playerinput;                //プレイヤーインプット
    public TetPuyo_Range tetpuyo_cs;           //ぷよテト移動スクリプト
    private Tet_Rotate tetRot_cs;                    //ぷよテト回転スクリプト
    public bool hardDropFlag;                       //ハードドロップフラグ


    //サウンド系
    private GameObject SoundManager;
    private SoundList sound;
    // Start is called before the first frame update
    void Start()
    {
        skelton = Instantiate(skelton, new Vector3(gameObject.transform.position.x, -10.0f, gameObject.transform.position.z), Quaternion.identity);
        Input = GameObject.Find("Player(Clone)");   //Inputオブジェクト検索
        playerinput = Input.GetComponent<PlayerInput>();    //PlayerInput検索
        //スクリプト検索
        tetpuyo_cs = GetComponent<TetPuyo_Range>();
        tetRot_cs = GetComponent<Tet_Rotate>();
        hardDropFlag = false;

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        //ハードドロップしたら
        if (playerinput.actions["HardDrop"].WasPressedThisFrame() || tetpuyo_cs.Left_Collision == true)
        {
            if (gameObject.tag == "Puyo")
            {
                sound.SEPlay(SoundList.SELIST.PUYO_HARDDROP);
            }
            else if (gameObject.tag == "TETRIS")
            {
                sound.SEPlay(SoundList.SELIST.TETO_HARDDROP);
            }


            foreach (BoxCollider2D col in gameObject.GetComponentsInChildren<BoxCollider2D>())
            {
                col.enabled = true;
            }

            FallOBJ.nowposY = skelton.transform.position.y;
            // ハードドロップSE再生フラグを立てる
            SoundTest.HardDropSEflag = true;

            //透明なオブジェクトに移動
            gameObject.transform.position = skelton.transform.position;

            //現在の位置を保存
            FallOBJ.nowposY = gameObject.transform.position.y;

            hardDropFlag = true;
            //透明なオブジェクト削除
            Destroy(skelton);
            //スクリプト削除
            Destroy(tetpuyo_cs);
            Destroy(this);
            Destroy(tetRot_cs);

        }
    }
}
