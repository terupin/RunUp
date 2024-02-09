using UnityEngine;
using UnityEngine.InputSystem;

public class Resalt_Game : MonoBehaviour
{
    private GameObject obj;  
    private player Player;
    private Animator Anim;

    Vector3 pos;

    private PlayerInput inputsystem;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();  //アニメーターの取得

        obj = GameObject.Find("Player(Clone)");   //プレイヤーの検索
        Player = obj.GetComponent<player>();  //プレイヤーオブジェクトの取得


        inputsystem=GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Finish_DeathAnim)
        {
            //カメラの位置を取得
            pos.x= Camera.main.transform.position.x;
            pos.y= Camera.main.transform.position.y;

            this.transform.position = pos;

            Anim.SetTrigger("After_Death");

            //ボタン押したら、シーンを再度読み込む
            if (inputsystem.actions["Decision"].WasPressedThisFrame())
            {
                FallOBJ.nowposY = 0.0f;
                FallOBJ.highest = 0.0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }
}
