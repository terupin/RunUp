    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puyo_Rotate : MonoBehaviour
{
    //中心点
    [SerializeField]
    private Vector3 center = Vector3.zero;


    private GameObject playerinput;
    private PlayerInput Inputsys;

    //回転軸
    [SerializeField]
    private Vector3 axis = Vector3.up;


    //向きの更新
    [SerializeField]
    private bool updateRotation = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerinput = GameObject.Find("Input");
        Inputsys=playerinput.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        var tr = transform;
        var angleAxis = Quaternion.AngleAxis(0, Vector3.up);
        var pos = tr.position;

        //回転
        if (Inputsys.actions["Rotate_Right"].WasPressedThisFrame())
        {
            angleAxis = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        }
        if (Inputsys.actions["Rotate_Left"].WasPressedThisFrame())
        {
            angleAxis = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
        }

        pos -= center;
        pos = angleAxis * pos;
        pos += center;

        tr.position = pos;

        if (updateRotation)
        {
            tr.rotation = tr.rotation * angleAxis;
        }

    }
}

