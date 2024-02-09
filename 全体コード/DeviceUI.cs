using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class DeviceUI : MonoBehaviour
{
    //ゲームオブジェクト
    private GameObject Hard;
    private GameObject Move;
    private GameObject Rotate;

    //各オブジェクトスプライトレンダラー
    private SpriteRenderer Hsprite;
    private SpriteRenderer Msprite;
    private SpriteRenderer Rsprite;

    //各種スプライト格納変数
    public Sprite[] sprite;

    // Start is called before the first frame update
    void Start()
    {
        Hard = GameObject.Find("hard_con");
        Move = GameObject.Find("move_con");
        Rotate = GameObject.Find("rotate_con");

        Hsprite=Hard.GetComponent<SpriteRenderer>();
        Msprite=Move.GetComponent<SpriteRenderer>();
        Rsprite=Rotate.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //デバイスに応じてUI変更
        if (Gamepad.current == null)
        {
            Hsprite.sprite = sprite[1];
            Msprite.sprite = sprite[3];
            Rsprite.sprite = sprite[6];
        }
        else
        {
            if (Gamepad.current.ToString() == "XInputControllerWindows:/XInputControllerWindows")
            {
                Hsprite.sprite = sprite[0];
                Msprite.sprite = sprite[2];
                Rsprite.sprite = sprite[5];
            }
            else if (Gamepad.current.ToString() == "DualSenseGamepadHID:/DualSenseGamepadHID")
            {
                Hsprite.sprite = sprite[0];
                Msprite.sprite = sprite[2];
                Rsprite.sprite = sprite[4];
            }
        }
    }
}
