using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// 音量管理クラス
[Serializable]
public class SoundVolume
{
    public float BGM = 1.0f;
    public float Voice = 1.0f;
    public float SE = 1.0f;
    public bool Mute = false;

    public void Init()
    {
        BGM = 1.0f;
        Voice = 1.0f;
        SE = 1.0f;
        Mute = false;
    }
}

// 音管理クラス
public class SoundManager : MonoBehaviour
{
    protected static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                if (instance == null)
                {
                    Debug.LogError("SoundManager Instance Error");
                }
            }

            return instance;
        }
    }

    // 音量
    public SoundVolume volume = new SoundVolume();

    // === AudioSource ===
    // BGM
    private AudioSource BGMsource;
    // SE
    private AudioSource[] SEsources = new AudioSource[16];
    // 音声
    private AudioSource[] VoiceSources = new AudioSource[16];
    //ストーリー音声
    private AudioSource[] StoryVoiceSources = new AudioSource[16];

    // === AudioClip ===
    // BGM
    public AudioClip[] BGM;
    // SE
    public AudioClip[] SE;
    // 音声
    public AudioClip[] Voice;
    public AudioClip[] Voice2P;
    public AudioClip[] _seastyVoice;
    public AudioClip[] _luiceVoice;
    public AudioClip[] _luminaVoice;
    public AudioClip[] _kurotoVoice;
    //ストーリー音声
    public AudioClip[] StoryVoice;
    public AudioClip[] _kurotoStoryVoice;
    public AudioClip[] _luminaStoryVoice;
    public AudioClip[] _luiceStoryVoice;
    public AudioClip[] _seastyStoryVoice;
    public AudioClip[] _commonStoryVoice;

    public enum VoiceType
    {
        Piece3Skill,
        Piece5Skill,
        PieceSpecialSkill,
        CharaSelect,
        Win,
        Scenario
    }
    
    // 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBoot()
    {
        Debug.Log("<color=red>サウンドマネージャー運転中</color>");
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }

    private void Awake()
    {
        // 音管理はシーン遷移では破棄させない
        DontDestroyOnLoad(gameObject);

        // 全てのAudioSourceコンポーネントを追加する

        // BGM AudioSource
        BGMsource = gameObject.AddComponent<AudioSource>();
        // BGMはループを有効にする
        BGMsource.loop = true;

        // SE AudioSource
        for (int i = 0; i < SEsources.Length; i++)
        {
            SEsources[i] = gameObject.AddComponent<AudioSource>();
        }

        // 音声 AudioSource
        for (int i = 0; i < VoiceSources.Length; i++)
        {
            VoiceSources[i] = gameObject.AddComponent<AudioSource>();
        }

        for (int i = 0; i < StoryVoiceSources.Length; i++)
        {
            StoryVoiceSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // ミュート設定
        BGMsource.mute = volume.Mute;
        foreach (AudioSource source in SEsources)
        {
            source.mute = volume.Mute;
        }
        foreach (AudioSource source in VoiceSources)
        {
            source.mute = volume.Mute;
        }

        // ボリューム設定
        BGMsource.volume = volume.BGM;
        foreach (AudioSource source in SEsources)
        {
            source.volume = volume.SE;
        }
        foreach (AudioSource source in VoiceSources)
        {
            source.volume = volume.Voice;
        }
    }



    // ***** BGM再生 *****
    // BGM再生
    public void PlayBGM(int index)
    {
        if (0 > index || BGM.Length <= index)
        {
            return;
        }
        // 同じBGMの場合は何もしない
        if (BGMsource.clip == BGM[index])
        {
            return;
        }
        BGMsource.Stop();
        BGMsource.clip = BGM[index];
        BGMsource.loop = true;
        BGMsource.Play();
    }

    // BGM停止
    public void StopBGM()
    {
        BGMsource.Stop();
        BGMsource.clip = null;
    }


    // ***** SE再生 *****
    // SE再生
    public void PlaySE(int index)
    {
        if (0 > index || SE.Length <= index)
        {
            return;
        }

        // 再生中で無いAudioSouceで鳴らす
        foreach (AudioSource source in SEsources)
        {
            if (false == source.isPlaying)
            {
                source.clip = SE[index];
                source.Play();
                return;
            }
        }
    }

    // SE停止
    public void StopSE()
    {
        // 全てのSE用のAudioSouceを停止する
        foreach (AudioSource source in SEsources)
        {
            source.Stop();
            source.clip = null;
        }
    }


    // ***** 音声再生 *****
    // 音声再生
    public void PlayVoice1P(VoiceType voiceType)
    {
        PlayVoice((int) voiceType, Voice);
    }
    public void PlayVoice2P(VoiceType voiceType)
    {
        PlayVoice((int) voiceType, Voice2P);
    }

    /// <summary>
    /// 1Pか2Pか分別する関数
    /// </summary>
    /// <param name="index">ボイスの配列</param>
    /// <param name="audioClips">どれを再生するか</param>
    private void PlayVoice(int index, AudioClip[] audioClips)
    {
        if (0 > index || Voice.Length <= index)
        {
            return;
        }
        
        // 再生中で無いAudioSouceで鳴らす
        foreach (AudioSource source in VoiceSources)
        {
            if (false == source.isPlaying)
            {
                source.clip = audioClips[index];
                source.Play();
                return;
            }
        }
    }

    /// <summary>
    /// キャラクターの番号確定判定関数
    /// </summary>
    /// <param name="oneCharaNum">1pのキャラクター番号</param>
    /// <param name="twoCharaNum">2pのキャラクター番号</param>
    public void CharacterConfirmVoice(CharaImageMoved.CharaType1P charaType1P)
    {
        Voice = charaType1P switch
        {
            CharaImageMoved.CharaType1P.Cow => _seastyVoice,
            CharaImageMoved.CharaType1P.Mouse => _luiceVoice,
            CharaImageMoved.CharaType1P.Rabbit => _luminaVoice,
            CharaImageMoved.CharaType1P.Tiger => _kurotoVoice,
            _ => throw new ArgumentOutOfRangeException(nameof(charaType1P), charaType1P, null)
        };
    }

    public void CharacterConfirmVoice2P(CharaImageMoved2P.CharaType2P charaType2P)
    {
        Voice2P = charaType2P switch
        {
            CharaImageMoved2P.CharaType2P.Cow => _seastyVoice,
            CharaImageMoved2P.CharaType2P.Mouse => _luiceVoice,
            CharaImageMoved2P.CharaType2P.Rabbit => _luminaVoice,
            CharaImageMoved2P.CharaType2P.Tiger => _kurotoVoice,
            _ => throw new ArgumentOutOfRangeException(nameof(charaType2P), charaType2P, null)
        };
    }
    
    // 音声停止
    public void StopVoice()
    {
        // 全ての音声用のAudioSouceを停止する
        foreach (AudioSource source in VoiceSources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public void ConfirmStoryVoice(CharaImageMoved.CharaType1P charaType1P)
    {
        StoryVoice = charaType1P switch
        {
            CharaImageMoved.CharaType1P.Cow => _seastyStoryVoice,
            CharaImageMoved.CharaType1P.Mouse => _luiceStoryVoice,
            CharaImageMoved.CharaType1P.Rabbit => _luminaStoryVoice,
            CharaImageMoved.CharaType1P.Tiger => _kurotoStoryVoice,
            _ => throw new ArgumentOutOfRangeException(nameof(charaType1P), charaType1P, null)
        };
    }

    public void PlayStoryVoice(int index)
    {
        if (0 > index || StoryVoice.Length <= index)
        {
            return;
        }

        // 再生中で無いAudioSouceで鳴らす
        foreach (AudioSource source in StoryVoiceSources)
        {
            if (false == source.isPlaying)
            {
                source.clip = StoryVoice[index];
                source.Play();
                return;
            }
        }
    }

    public void StopStoryVoice()
    {
        // 全ての音声用のAudioSouceを停止する
        foreach (AudioSource source in StoryVoiceSources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public void SetCommonStory()
    {
        StoryVoice = _commonStoryVoice;
    }
}