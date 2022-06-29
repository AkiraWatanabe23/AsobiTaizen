using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DropdownButton �������ꂽ���̏���
/// </summary>
public class DropDown : MonoBehaviour
{
    [SerializeField] Dropdown dropDown;
    //[SerializeField] Images images = Images.Help;
    [SerializeField] Image _ruleImage;
    [SerializeField] Image _moveImage;
    [SerializeField] Image _toTitleImage;
    [SerializeField] Image _helpPanel;

    void Start()
    {
        _moveImage.gameObject.SetActive(false);
        _ruleImage.gameObject.SetActive(false);
        _toTitleImage.gameObject.SetActive(false);
        _helpPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dropDown.value == 0)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("none");
        }
        //DropDownButton �� Value ��1("moving"���I������Ă���)�̎�
        else if (dropDown.value == 1)
        {
            _moveImage.gameObject.SetActive(true);
            _ruleImage.gameObject.SetActive(false); 
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("moving");
        }
        //DropDownButton �� Value ��2("rule"���I������Ă���)�̎�
        else if (dropDown.value == 2)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(true);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("rule");
        }
        //DropDownButton �� Value ��3("to title"���I������Ă���)�̎�
        else if (dropDown.value == 3)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(true);
            Debug.Log("to title");
        }
        //DropDownButton �� Value ��4("back"���I������Ă���)�̎�
        else if (dropDown.value == 4)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            _helpPanel.gameObject.SetActive(false);
            dropDown.gameObject.SetActive(false);
            Debug.Log("back");
        }
    }
}
