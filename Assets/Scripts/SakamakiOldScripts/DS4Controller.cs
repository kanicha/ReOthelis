using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS4Controller : MonoBehaviour
{
    /// <summary>
    /// �����{�^���e��
    /// </summary>
    // �ۃ{�^��
    public string DS4o = "Fire_2";
    // �o�c�{�^��
    public string DS4x = "Fire_1";
    // �l�p�{�^��
    public string DS4sq = "Fire_0";
    // �O�p�{�^��
    public string DS4tri = "Fire_3";
    // L1�{�^��
    public string DS4L1 = "Fire_L1";
    // R1�{�^��
    public string DS4R1 = "Fire_R1";
    // Option�{�^��
    public string DS4Option = "Fire_Option";

    /// <summary>
    /// �\���L�[
    /// </summary>
    // �\���L�[�E.��
    public string DS4Horizontal = "Horizontal D-Pad";
    //public float fDS4Horizontal = Input.GetAxisRaw("Horizontal D-Pad");
    // �\���L�[��.��
    public string DS4Vertical = "Vertical D-Pad";
    //public float fDS4Vertical = Input.GetAxisRaw("Vertical D-Pad");

    /// <summary>
    /// �X�e�B�b�N
    /// </summary>
    // ���X�e�B�b�N�E.��
    public string DS4LStickHorizontal = "Horizontal Stick-L";
    // ���X�e�B�b�N��.��
    public string DS4LStickVertical = "Vertical Stick-L";
    // �E�X�e�B�b�N�E.��
    public string DS4RStickHorizontal = "Horizontal Stick-R";
    // �E�X�e�B�b�N��,��
    public string DS4RStickVertical = "Vertical Stick-R";
}
