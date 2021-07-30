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

    // === AudioClip ===
    // BGM
    public AudioClip[] BGM;
    // SE
    public AudioClip[] SE;
    // ����
    public AudioClip[] Voice;

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
            /*if (false == source.isPlaying)
            {*/
                source.clip = SE[index];
                source.Play();
                return;
            /*}*/
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
    public void PlayVoice(int index)
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
                source.clip = Voice[index];
                source.Play();
                return;
            }
        }
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
}