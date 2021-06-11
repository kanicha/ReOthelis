using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDraw : MonoBehaviour
{
	private const byte _WIDTH = 10;
	private const byte _HEIGHT = 10;
	private string[,] map = new string[_HEIGHT, _WIDTH]
	{
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","�Z","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" },
		{ "��", "��", "��","��","��","��", "��","��", "��", "��" }
	};

	const char black = '��';
	const char white = '�Z';
	const char wall = '��';
	const char empty = '��';

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//��c�Œu���ꂽ�ꍇ�A���������������̏��ԂŌ������s��

	//����Œu���ꂽ�ꍇ�A����������E�����̏��ԂŌ������s��
	
	//��������ꍇ�A���������������̏��ԂŌ������s��

	//Player 1 = ���v���C���[
	//Player 2 = ���v���C���[
	//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
	
	//���v���C���[�̏ꍇ�A���̋��D�悵�Č������s��
	
	//�S����������
	private void MinoCheck(int x, int y)
    {
		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				//�u���ꂽ��̃}�X�͌������Ȃ�
				if (i == 0 && j == 0)
                {
					continue;
                }
				//�ǂɓ��������A�܂��͒����Ă��܂��ꍇ�͌������Ȃ�
				if (y + i < 0 || x + j < 0 || y + i > 9 || x + j > 9)
                {
					continue;
                }
				//�����̋����𑫂��Ă���
				for (int s = 2; s < 9; s++)
				{
					if (x + j * s >= 0 && x + j * s < 9 && y + i * s >= 0 && y + i * s < 9)
					{
						//�󂫂ɓ��������ꍇ�A���̕����̌������I��������
						if (map[y + i * s,x + j * s] == "��")
						{
							break; 
						}
						//�����F�̋�ɓ��������ꍇ�A���̃}�X�ɂ�����܂ł̃}�X�̋���Ђ�����Ԃ�
						if (map[y + i * s,x + j * s] == "��")
						{
							for (int n = 1; n < s; n++)
                            {
								map[y + i * n,x + j * n] = "��";
							}
						}
					}
				}
            }
        }
    }

	

	
}
