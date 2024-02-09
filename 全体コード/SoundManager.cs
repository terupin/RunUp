using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 一度再生してから、次再生できるまでの間隔(秒)
    [SerializeField]
    private float SE_playableDistance = 0.2f;
    private int Add_BGM_SoundSorce = 1;
    private int Add_SE_Sound = 2;

    [System.Serializable]
    //SEデータ
    public class Sound_SE_Data
    {
        public string SE_name;
        public AudioClip SE_audioClip;
        public float SE_playedTime; // 前回再生した時間
    }

    [SerializeField]
    private Sound_SE_Data[] sound_SE_Datas;

    [System.Serializable]
    //BGMデータ
    public class Sound_BGM_Data
    {
        public string BGM_name;
        public AudioClip BGM_audioClip;
        public float BGM_playedTime; // 前回再生した時間
    }

    [SerializeField]
    private Sound_BGM_Data[] sound_BGM_Datas;

    // AudioSorce(スピーカー)を同時に鳴らしたい音の数だけ用意
    private AudioSource[] audioSourceList;
    private AudioSource[] SE_audiosourceList;
 
    // 別名(SE_name)(BGM_name)をキーとした管理用Dictionary
    private Dictionary<string, Sound_SE_Data> sound_SE_Dictionary = new Dictionary<string, Sound_SE_Data>();
    private Dictionary<string, Sound_BGM_Data> sound_BGM_Dictionary = new Dictionary<string, Sound_BGM_Data>();

    private void Awake()
    {
        // AudioSorce(スピーカー)を同時に鳴らしたい音の数だけ用意
        audioSourceList = new AudioSource[Add_BGM_SoundSorce];
        audioSourceList[0] = gameObject.AddComponent<AudioSource>();
        SE_audiosourceList = new AudioSource[Add_SE_Sound];
        SE_audiosourceList[0] = gameObject.AddComponent<AudioSource>();
        SE_audiosourceList[1] = gameObject.AddComponent<AudioSource>();

        // sound_SE_Dictionaryにセット
        foreach (var soundData in sound_SE_Datas)
        {
            sound_SE_Dictionary.Add(soundData.SE_name, soundData);
        }
    
        // sound_BGM_Dictionaryにセット
        foreach (var soundData in sound_BGM_Datas)
        {
            sound_BGM_Dictionary.Add(soundData.BGM_name, soundData);
        }
    
    }

    // 未使用のAudioSourceの取得　全て使用中の場合はnullを返却
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }
    
        return null; // 未使用のAudioSouceは見つかりませんでした
    }

    // 指定されたAudioClipを未使用のAudioSorceで再生
    public void SEPlay(AudioClip SEclip)
    {
        var SEaudioSource = SE_audiosourceList[0]; // 1つのAudioSourceを使用
        if (SEaudioSource == null) return; // 再生できませんでした
        SEaudioSource.clip = SEclip;
        SEaudioSource.Play();
    }

    // 指定されたAudioClip2を未使用のAudioSorceで再生
    public void SEPlay2(AudioClip SEclip)
    {
        var SEaudioSource = SE_audiosourceList[1]; // もう1つのAudioSourceを使用
        if (SEaudioSource == null) return; // 再生できませんでした
        SEaudioSource.clip = SEclip;
        SEaudioSource.Play();
    }

    // 指定された別名で登録されたAudioClipをフェードアウトしながら再生
    private IEnumerator PlaySEWithFadeOut(AudioClip SEclip, float fadeDuration)
    {
        var SEaudioSource = SE_audiosourceList[0];
        if (SEaudioSource == null) yield break; // 再生できませんでした

        SEaudioSource.clip = SEclip;
        SEaudioSource.volume = 1; // 音量を0から開始

        SEaudioSource.Play();

        float startVolume = SEaudioSource.volume;
        float startTime = Time.time;

        // フェードアウトの処理
        while (SEaudioSource.volume > 0)
        {
            float t = (Time.time - startTime) / fadeDuration;
            SEaudioSource.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }

        SEaudioSource.Stop();
        SEaudioSource.volume = startVolume; // 音量を元に戻す
    }

    // BGM再生
    public void BGMPlay(AudioClip BGMclip)
    {
        var BGMaudioSource = audioSourceList[0]; // 1つのAudioSourceを使用
        if (BGMaudioSource == null) return; // 再生できませんでした

        // 既にBGMが再生中かどうかを確認
        foreach (var audioSource in audioSourceList)
        {
            if (audioSource.isPlaying && audioSource.clip != null && audioSource.clip == BGMclip)
            {
                return; // 既に同じBGMが再生中なので何もしない
            }
        }
        // // 既存のBGMを停止
        foreach (var audioSource in audioSourceList)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        BGMaudioSource.clip = BGMclip;
        BGMaudioSource.Play();
    }

    // 指定された別名で登録されたSEのAudioClipを再生
    public void SE_Play(string SEname)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // 管理用Dictionaryから、別名で検索
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // まだ再生するには早い
            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // 次回用に今回の再生時間の保持
            SEPlay(SEsoundData.SE_audioClip); // 見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{SEname}");
        }
    }

    // 指定された別名で登録されたSEのAudioClip2を再生
    public void SE_Play2(string SEname)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // 管理用Dictionaryから、別名で検索
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // まだ再生するには早い
            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // 次回用に今回の再生時間の保持
            SEPlay2(SEsoundData.SE_audioClip); // 見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{SEname}");
        }
    }

    // 指定された別名で登録されたAudioClipをフェードアウトしながら再生
    public void SE_PlayWithFadeOut(string SEname, float fadeDuration)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // 管理用Dictionaryから、別名で検索
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // まだ再生するには早い

            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // 次回用に今回の再生時間の保持
            StartCoroutine(PlaySEWithFadeOut(SEsoundData.SE_audioClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{SEname}");
        }
    }

    public void BGM_Play(string BGMname)
    {
        if (sound_BGM_Dictionary.TryGetValue(BGMname, out var BGMsoundData)) // 管理用Dictionaryから、別名で検索
        {
            BGMPlay(BGMsoundData.BGM_audioClip); // 見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{BGMname}");
        }
    }
}
