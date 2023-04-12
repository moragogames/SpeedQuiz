using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static SoundMgr instance;
    public static SoundMgr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    [SerializeField] SoundData[] soundDatas;
    [SerializeField] AudioSource[] audioSources;

    private void Start()
    {
        audioSources = new AudioSource[10];

        for (int i = 0; i < 10; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(SFXType _sType)
    {
        SoundData _sData = GetSoundData(_sType);

        AudioSource _aSourse = GetAudioSourse();

        _aSourse.clip = _sData.audioClip;
        _aSourse.volume = _sData.volume;
        _aSourse.loop = _sData.loop;
        _aSourse.playOnAwake = _sData.playOnAwake;

        _aSourse.Play();
    }

    SoundData GetSoundData(SFXType _sType)
    {
        for (int i = 0; i < soundDatas.Length; i++)
        {
            if (soundDatas[i].sfxType.Equals(_sType))
            {
                return soundDatas[i];
            }
        }
        return null;
    }

    AudioSource GetAudioSourse()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying)
            {
                continue;
            }
            return audioSources[i];
        }
        return audioSources[0];
    }
}

public enum SFXType
{
    click,
    wrong,
    rigjt,
    menu
}


[System.Serializable]
public class SoundData
{
    public SFXType sfxType;
    public AudioClip audioClip;
    public float volume;
    public bool loop;
    public bool playOnAwake;
}
