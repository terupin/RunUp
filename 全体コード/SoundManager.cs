using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ��x�Đ����Ă���A���Đ��ł���܂ł̊Ԋu(�b)
    [SerializeField]
    private float SE_playableDistance = 0.2f;
    private int Add_BGM_SoundSorce = 1;
    private int Add_SE_Sound = 2;

    [System.Serializable]
    //SE�f�[�^
    public class Sound_SE_Data
    {
        public string SE_name;
        public AudioClip SE_audioClip;
        public float SE_playedTime; // �O��Đ���������
    }

    [SerializeField]
    private Sound_SE_Data[] sound_SE_Datas;

    [System.Serializable]
    //BGM�f�[�^
    public class Sound_BGM_Data
    {
        public string BGM_name;
        public AudioClip BGM_audioClip;
        public float BGM_playedTime; // �O��Đ���������
    }

    [SerializeField]
    private Sound_BGM_Data[] sound_BGM_Datas;

    // AudioSorce(�X�s�[�J�[)�𓯎��ɖ炵�������̐������p��
    private AudioSource[] audioSourceList;
    private AudioSource[] SE_audiosourceList;
 
    // �ʖ�(SE_name)(BGM_name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, Sound_SE_Data> sound_SE_Dictionary = new Dictionary<string, Sound_SE_Data>();
    private Dictionary<string, Sound_BGM_Data> sound_BGM_Dictionary = new Dictionary<string, Sound_BGM_Data>();

    private void Awake()
    {
        // AudioSorce(�X�s�[�J�[)�𓯎��ɖ炵�������̐������p��
        audioSourceList = new AudioSource[Add_BGM_SoundSorce];
        audioSourceList[0] = gameObject.AddComponent<AudioSource>();
        SE_audiosourceList = new AudioSource[Add_SE_Sound];
        SE_audiosourceList[0] = gameObject.AddComponent<AudioSource>();
        SE_audiosourceList[1] = gameObject.AddComponent<AudioSource>();

        // sound_SE_Dictionary�ɃZ�b�g
        foreach (var soundData in sound_SE_Datas)
        {
            sound_SE_Dictionary.Add(soundData.SE_name, soundData);
        }
    
        // sound_BGM_Dictionary�ɃZ�b�g
        foreach (var soundData in sound_BGM_Datas)
        {
            sound_BGM_Dictionary.Add(soundData.BGM_name, soundData);
        }
    
    }

    // ���g�p��AudioSource�̎擾�@�S�Ďg�p���̏ꍇ��null��ԋp
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }
    
        return null; // ���g�p��AudioSouce�͌�����܂���ł���
    }

    // �w�肳�ꂽAudioClip�𖢎g�p��AudioSorce�ōĐ�
    public void SEPlay(AudioClip SEclip)
    {
        var SEaudioSource = SE_audiosourceList[0]; // 1��AudioSource���g�p
        if (SEaudioSource == null) return; // �Đ��ł��܂���ł���
        SEaudioSource.clip = SEclip;
        SEaudioSource.Play();
    }

    // �w�肳�ꂽAudioClip2�𖢎g�p��AudioSorce�ōĐ�
    public void SEPlay2(AudioClip SEclip)
    {
        var SEaudioSource = SE_audiosourceList[1]; // ����1��AudioSource���g�p
        if (SEaudioSource == null) return; // �Đ��ł��܂���ł���
        SEaudioSource.clip = SEclip;
        SEaudioSource.Play();
    }

    // �w�肳�ꂽ�ʖ��œo�^���ꂽAudioClip���t�F�[�h�A�E�g���Ȃ���Đ�
    private IEnumerator PlaySEWithFadeOut(AudioClip SEclip, float fadeDuration)
    {
        var SEaudioSource = SE_audiosourceList[0];
        if (SEaudioSource == null) yield break; // �Đ��ł��܂���ł���

        SEaudioSource.clip = SEclip;
        SEaudioSource.volume = 1; // ���ʂ�0����J�n

        SEaudioSource.Play();

        float startVolume = SEaudioSource.volume;
        float startTime = Time.time;

        // �t�F�[�h�A�E�g�̏���
        while (SEaudioSource.volume > 0)
        {
            float t = (Time.time - startTime) / fadeDuration;
            SEaudioSource.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }

        SEaudioSource.Stop();
        SEaudioSource.volume = startVolume; // ���ʂ����ɖ߂�
    }

    // BGM�Đ�
    public void BGMPlay(AudioClip BGMclip)
    {
        var BGMaudioSource = audioSourceList[0]; // 1��AudioSource���g�p
        if (BGMaudioSource == null) return; // �Đ��ł��܂���ł���

        // ����BGM���Đ������ǂ������m�F
        foreach (var audioSource in audioSourceList)
        {
            if (audioSource.isPlaying && audioSource.clip != null && audioSource.clip == BGMclip)
            {
                return; // ���ɓ���BGM���Đ����Ȃ̂ŉ������Ȃ�
            }
        }
        // // ������BGM���~
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

    // �w�肳�ꂽ�ʖ��œo�^���ꂽSE��AudioClip���Đ�
    public void SE_Play(string SEname)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // �Ǘ��pDictionary����A�ʖ��Ō���
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // �܂��Đ�����ɂ͑���
            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // ����p�ɍ���̍Đ����Ԃ̕ێ�
            SEPlay(SEsoundData.SE_audioClip); // ����������A�Đ�
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{SEname}");
        }
    }

    // �w�肳�ꂽ�ʖ��œo�^���ꂽSE��AudioClip2���Đ�
    public void SE_Play2(string SEname)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // �Ǘ��pDictionary����A�ʖ��Ō���
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // �܂��Đ�����ɂ͑���
            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // ����p�ɍ���̍Đ����Ԃ̕ێ�
            SEPlay2(SEsoundData.SE_audioClip); // ����������A�Đ�
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{SEname}");
        }
    }

    // �w�肳�ꂽ�ʖ��œo�^���ꂽAudioClip���t�F�[�h�A�E�g���Ȃ���Đ�
    public void SE_PlayWithFadeOut(string SEname, float fadeDuration)
    {
        if (sound_SE_Dictionary.TryGetValue(SEname, out var SEsoundData)) // �Ǘ��pDictionary����A�ʖ��Ō���
        {
            if (Time.realtimeSinceStartup - SEsoundData.SE_playedTime < SE_playableDistance) return; // �܂��Đ�����ɂ͑���

            SEsoundData.SE_playedTime = Time.realtimeSinceStartup; // ����p�ɍ���̍Đ����Ԃ̕ێ�
            StartCoroutine(PlaySEWithFadeOut(SEsoundData.SE_audioClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{SEname}");
        }
    }

    public void BGM_Play(string BGMname)
    {
        if (sound_BGM_Dictionary.TryGetValue(BGMname, out var BGMsoundData)) // �Ǘ��pDictionary����A�ʖ��Ō���
        {
            BGMPlay(BGMsoundData.BGM_audioClip); // ����������A�Đ�
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{BGMname}");
        }
    }
}
