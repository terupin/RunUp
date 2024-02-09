using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    //音楽
    public AudioClip[] BGM;
    public AudioClip[] SE;

    //オーディオソース
    private AudioSource BGMPlayer;
    private AudioSource SEPlayer;
    private AudioSource SEPlayer2;

    //BGM種類
    public enum BGMLIST
    {
        TITLE,
        GAME,
        RESULT
    }

    //SE種類
    public enum SELIST
    {
        DECISION,
        JUMP,
        MOVE,
        PUYO_HARDDROP,
        TETO_HARDDROP,
        FALL,
        WALL
    }

    private float fall_volume=0.35f;
    // Start is called before the first frame update
    void Start()
    {
        //FPS固定化
        Application.targetFrameRate = 60;

        //コンポーネント追加＆取得
        BGMPlayer=gameObject.AddComponent<AudioSource>();
        SEPlayer=gameObject.AddComponent<AudioSource>();
        SEPlayer2=gameObject.AddComponent<AudioSource>();

        //BGMループ再生
        BGMPlayer.loop=true;
        BGMPlay(BGMLIST.TITLE);


    }

    public void BGMPitch(float pitch)
    {
        BGMPlayer.pitch = pitch;
    }
    //BGM再生関数
    public void BGMPlay(BGMLIST bgm)
    {
        //音量調整
        switch (bgm)
        {
            case BGMLIST.TITLE:
                BGMPlayer.volume = 0.25f;
                break;
            case BGMLIST.GAME:
                BGMPlayer.volume = 0.15f;
                break;
            case BGMLIST.RESULT:
                BGMPlayer.volume = 0.5f;
                break;
        }
        BGMPlayer.pitch = 1.0f;
        BGMPlayer.clip = BGM[bgm.GetHashCode()];
        BGMPlayer.Play();
    }

    //SE再生関数
    public void SEPlay(SELIST se) 
    {
        //音量調整
        switch (se)
        {
            case SELIST.DECISION:
                SEPlayer.volume = 0.75f;
                break;
            case SELIST.JUMP:
                SEPlayer.volume = 0.25f;
                break;
            case SELIST.MOVE:
                SEPlayer.volume = 0.55f;
                break;
            case SELIST.PUYO_HARDDROP:
                SEPlayer.volume = 0.85f;
                break;
            case SELIST.TETO_HARDDROP:
                SEPlayer.volume = 1.0f;
                break;
            case SELIST.FALL:
                SEPlayer.volume = 0.35f;
                break;
            case SELIST.WALL:
                SEPlayer.volume = 0.45f;
                break;
        }
        SEPlayer.PlayOneShot(SE[se.GetHashCode()]);
    }

    public void SEPlay2(SELIST se)
    {
        //音量調整
        switch (se)
        {
            case SELIST.DECISION:
                SEPlayer2.volume = 0.75f;
                break;
            case SELIST.JUMP:
                SEPlayer2.volume = 0.25f;
                break;
            case SELIST.MOVE:
                SEPlayer2.volume = 0.55f;
                break;
            case SELIST.PUYO_HARDDROP:
                SEPlayer2.volume = 0.85f;
                break;
            case SELIST.TETO_HARDDROP:
                SEPlayer2.volume = 1.0f;
                break;
            case SELIST.FALL:
                SEPlayer2.volume = 0.35f;
                break;
            case SELIST.WALL:
                SEPlayer2.volume = 0.45f;
                break;
        }
        SEPlayer2.PlayOneShot(SE[se.GetHashCode()]);
    }

    public void SE_Fall_volume()
    {
        SEPlayer2.volume = fall_volume;
        fall_volume -= 0.0035f;
    }
}
