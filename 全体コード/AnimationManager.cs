using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //private Animator anim; // �A�j���[�^�[

    public bool animFlg; // �A�j���[�V�����t���O 

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>(); // �A�j���[�^�[�̎擾
        animFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(animFlg)
        //{
        //    anim.SetTrigger("Jump");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animFlg = true;
    }

}
