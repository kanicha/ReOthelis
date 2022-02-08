using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// ���ʊǗ��N���X
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

// ���Ǘ��N���X
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

    // ����
    public SoundVolume volume = new SoundVolume();

    // === AudioSource ===
    // BGM
    private AudioSource BGMsource;
    // SE
    private AudioSource[] SEsources = new AudioSource[16];
    // ����
    private AudioSource[] VoiceSources = new AudioSource[16];
    //�X�g�[���[����
    private AudioSource[] StoryVoiceSources = new AudioSource[16];

    // === AudioClip ===
    // BGM
    public AudioClip[] BGM;
    // SE
    public AudioClip[] SE;
    // ����
    public AudioClip[] Voice;
    public AudioClip[] Voice2P;
    public AudioClip[] _seastyVoice;
    public AudioClip[] _luiceVoice;
    public AudioClip[] _luminaVoice;
    public AudioClip[] _kurotoVoice;
    //�X�g�[���[����
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
        Debug.Log("<color=red>�T�E���h�}�l�[�W���[�^�]��</color>");
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }

    private void Awake()
    {
        // ���Ǘ��̓V�[���J�ڂł͔j�������Ȃ�
        DontDestroyOnLoad(gameObject);

        // �S�Ă�AudioSource�R���|�[�l���g��ǉ�����

        // BGM AudioSource
        BGMsource = gameObject.AddComponent<AudioSource>();
        // BGM�̓��[�v��L���ɂ���
        BGMsource.loop = true;

        // SE AudioSource
        for (int i = 0; i < SEsources.Length; i++)
        {
            SEsources[i] = gameObject.AddComponent<AudioSource>();
        }

        // ���� AudioSource
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
        // �~���[�g�ݒ�
        BGMsource.mute = volume.Mute;
        foreach (AudioSource source in SEsources)
        {
            source.mute = volume.Mute;
        }
        foreach (AudioSource source in VoiceSources)
        {
            source.mute = volume.Mute;
        }

        // �{�����[���ݒ�
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



    // ***** BGM�Đ� *****
    // BGM�Đ�
    public void PlayBGM(int index)
    {
        if (0 > index || BGM.Length <= index)
        {
            return;
        }
        // ����BGM�̏ꍇ�͉������Ȃ�
        if (BGMsource.clip == BGM[index])
        {
            return;
        }
        BGMsource.Stop();
        BGMsource.clip = BGM[index];
        BGMsource.loop = true;
        BGMsource.Play();
    }

    // BGM��~
    public void StopBGM()
    {
        BGMsource.Stop();
        BGMsource.clip = null;
    }


    // ***** SE�Đ� *****
    // SE�Đ�
    public void PlaySE(int index)
    {
        if (0 > index || SE.Length <= index)
        {
            return;
        }

        // �Đ����Ŗ���AudioSouce�Ŗ炷
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

    // SE��~
    public void StopSE()
    {
        // �S�Ă�SE�p��AudioSouce���~����
        foreach (AudioSource source in SEsources)
        {
            source.Stop();
            source.clip = null;
        }
    }


    // ***** �����Đ� *****
    // �����Đ�
    public void PlayVoice1P(VoiceType voiceType)
    {
        PlayVoice((int) voiceType, Voice);
    }
    public void PlayVoice2P(VoiceType voiceType)
    {
        PlayVoice((int) voiceType, Voice2P);
    }

    /// <summary>
    /// 1P��2P�����ʂ���֐�
    /// </summary>
    /// <param name="index">�{�C�X�̔z��</param>
    /// <param name="audioClips">�ǂ���Đ����邩</param>
    private void PlayVoice(int index, AudioClip[] audioClips)
    {
        if (0 > index || Voice.Length <= index)
        {
            return;
        }
        
        // �Đ����Ŗ���AudioSouce�Ŗ炷
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
    /// �L�����N�^�[�̔ԍ��m�蔻��֐�
    /// </summary>
    /// <param name="oneCharaNum">1p�̃L�����N�^�[�ԍ�</param>
    /// <param name="twoCharaNum">2p�̃L�����N�^�[�ԍ�</param>
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
    
    // ������~
    public void StopVoice()
    {
        // �S�Ẳ����p��AudioSouce���~����
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

        // �Đ����Ŗ���AudioSouce�Ŗ炷
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
        // �S�Ẳ����p��AudioSouce���~����
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