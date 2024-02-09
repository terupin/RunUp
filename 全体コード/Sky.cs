using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private float Length;  //オブジェクト画像の大きさ
    private float Start_Y; //最初のオブジェクト画像のY座標
    private float Start_Under_Y;  //最初のオブジェクトの右下のY座標
    private float Up_Percent = 2.7f;  //空の上げ幅
    private float Half_Length;  //背景画像のサイズの２分の一

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Start_Y = this.transform.position.y; //背景画像の最初のY座標
        Length = this.GetComponent<SpriteRenderer>().bounds.size.y;  //空のY軸の画像サイズ
        Half_Length = Length / 2.0f;  //画像サイズの二分の一の大きさ
        Start_Under_Y = Start_Y - Half_Length;  //背景画像の右下のY座標
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = this.transform.position;  //プレイヤーのポジション
        Vector3 CameraRetMix = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));  //左下頂点のY座標の取得
        Vector3 CameraRetMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));  //左上頂点のY座標の取得
        Vector3 CameraPos = Camera.main.transform.position; //カメラのポジション

        //カメラの下のY座標が空の下のY座標より大きい場合
        if (Start_Under_Y < CameraRetMix.y)
        {
            float Camera_Up = CameraRetMix.y - Start_Under_Y;  //カメラの上がり幅
            float Sky_Up = (Pos.y - Half_Length) - Start_Under_Y; //空の上がり幅

            Sky_Up = (Camera_Up - 1) / Up_Percent;            //空の上がり幅の調整

            Pos.y = Start_Y + Sky_Up;
        }

        //空の一番上に着いたら画像がそのままついてくる
        if (CameraRetMax.y > Pos.y + Half_Length)
        {
            Pos.y = CameraRetMax.y - Half_Length;
        }


        this.transform.position = Pos;
    }
}