using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float alpha;
    private float timer;

    private GameObject invasion;
    private invasionCol inCol_cs;
    // Start is called before the first frame update
    void Start()
    {
        sprite=GetComponent<SpriteRenderer>();
        alpha = 1.0f;
        timer = 0.0f;

        invasion = GameObject.Find("invasion");
        inCol_cs=invasion.GetComponent<invasionCol>();
    }

    // Update is called once per frame
    void Update()
    {
        //アルファ値を変動
        timer+= Time.deltaTime;
        alpha = Mathf.Sin(timer * 7.0f) * 0.2f + 0.65f;
        sprite.color= new Vector4(0.3333334f, 1.0f, 0.4487348f, alpha);

        //ゲームオーバーで破棄
        if(inCol_cs.DeathFlg)
        {
            Destroy(this);  
        }
    }
}
