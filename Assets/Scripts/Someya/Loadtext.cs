using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Loadtext : MonoBehaviour
{
    [SerializeField]
    private Text maintext;
    // CSV�t�@�C��
    [SerializeField]
    private TextAsset csvFile;
    // CSV�̒��g�����郊�X�g
    List<string[]> csvDatas = new List<string[]>();
    //�ꕶ���ꕶ���̕\�����鑬��
    [SerializeField]
    float novelSpeed;
    private void Start()
    {
        // Resouces����CSV�ǂݍ���
        csvFile = Resources.Load("Datas/oseris_01") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        // reader.Peaek��-1�ɂȂ�܂�
        while (reader.Peek() != -1)
        {
            // ��s���ǂݍ���
            string line = reader.ReadLine();
            // , ��؂�Ń��X�g�ɒǉ�
            csvDatas.Add(line.Split(','));
        }
    }
}
