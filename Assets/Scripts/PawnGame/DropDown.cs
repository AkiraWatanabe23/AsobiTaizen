using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DropdownButton �������ꂽ(HelpPanel���J���ꂽ)���̏���
/// </summary>
public class DropDown : MonoBehaviour
{
    /// <summary> Dropdown�{�^�� </summary>
    public Dropdown dropDown;
    /// <summary> ���[�������� Image </summary>
    public Image _ruleImage;
    /// <summary> ��̓����Ɋւ��� Image </summary>
    public Image _moveImage;
    /// <summary> �^�C�g���J�ڑI���� Image </summary>
    public Image _toTitleImage;
    /// <summary> Dropdown�{�^�������������ɕ\�������ჿ�l�� Panel </summary>
    public Image _helpPanel;

    void Start()
    {
        //�n�߂́A�S�Ă�HelpImage ��\�����Ȃ���Ԃɂ��Ă���
        _moveImage.gameObject.SetActive(false);
        _ruleImage.gameObject.SetActive(false);
        _toTitleImage.gameObject.SetActive(false);
        _helpPanel.gameObject.SetActive(false);
    }

    public void ChangeImage() //Update�ł��ƁA�����Ď��s�������Ƃ��㏑������Ă��܂�(���s����Ă��Ȃ��悤�Ɍ�����)!!
    {
        //DropDownButton �� Value ��0("help"���I������Ă��� ... �����I������Ă��Ȃ�)�̎�
        if (dropDown.value == 0)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("none");
            //�S�Ă�HelpImage ��\�����Ȃ�
        }
        //DropDownButton �� Value ��1("moving"���I������Ă���)�̎�
        else if (dropDown.value == 1)
        {
            _moveImage.gameObject.SetActive(true);
            _ruleImage.gameObject.SetActive(false); 
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("moving");
            //PieceMoveImage ��\������
        }
        //DropDownButton �� Value ��2("rule"���I������Ă���)�̎�
        else if (dropDown.value == 2)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(true);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("rule");
            //RuleImage ��\������
        }
        //DropDownButton �� Value ��3("to title"���I������Ă���)�̎�
        else if (dropDown.value == 3)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(true);
            Debug.Log("to title");
            //ToTitleImage ��\������
        }
        //DropDownButton �� Value ��4("back"���I������Ă���)�̎�
        else if (dropDown.value == 4)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            _helpPanel.gameObject.SetActive(false);
            Debug.Log("back");
            //�S�Ă�HelpImage ��\�����Ȃ�(�Q�[���ɖ߂�)
        }
    }
}
