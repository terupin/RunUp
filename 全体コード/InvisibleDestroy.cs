using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleDestroy : MonoBehaviour
{
    private GameObject maincamera;
    private float DeletePos;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //�J�������痣�ꂽ��폜
        DeletePos = transform.position.x - maincamera.transform.position.x;

        if(DeletePos<-10.0f)
        {
            Destroy(gameObject);
        }
    }
}
