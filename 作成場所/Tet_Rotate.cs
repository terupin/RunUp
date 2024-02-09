using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tet_Rotate : MonoBehaviour
{
    // mino‰ñ“]
    public Vector3 rotationPoint;

    private GameObject playerinput;
    private PlayerInput Inputsys;
    private player player_cs;

    public enum ROTATE_ANGLE
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
    }
    public ROTATE_ANGLE angle;
    public ROTATE_ANGLE preangle;

    private GameObject SoundManager;
    private SoundList sound;
    // Start is called before the first frame update
    void Start()
    {
        playerinput = GameObject.Find("Player(Clone)");
        Inputsys=playerinput.GetComponent<PlayerInput>();
        player_cs = playerinput.GetComponent<player>();

        SoundManager = GameObject.Find("SoundManagerTest");
        sound = SoundManager.GetComponent<SoundList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_cs.inColscript.DeathFlg)
        {
            Destroy(this);
        }
        else
        {
            preangle = angle;
            if (Inputsys.actions["Rotate_Left"].WasPressedThisFrame())
            {
                sound.SEPlay(SoundList.SELIST.MOVE);
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (angle == ROTATE_ANGLE.UP)
                {
                    angle = ROTATE_ANGLE.LEFT;
                }
                else
                {
                    angle--;
                }
            }

            if (Inputsys.actions["Rotate_Right"].WasPressedThisFrame())
            {
                sound.SEPlay(SoundList.SELIST.MOVE);
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                if (angle == ROTATE_ANGLE.LEFT)
                {
                    angle = ROTATE_ANGLE.UP;
                }
                else
                {
                    angle++;
                }
            }
        }
    }
}
