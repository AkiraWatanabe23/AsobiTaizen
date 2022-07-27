using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DropdownButton が押された(HelpPanelが開かれた)時の処理
/// </summary>
public class DropDown : MonoBehaviour
{
    /// <summary> Dropdownボタン </summary>
    public Dropdown dropDown;
    /// <summary> ルール説明の Image </summary>
    public Image _ruleImage;
    /// <summary> 駒の動きに関する Image </summary>
    public Image _moveImage;
    /// <summary> タイトル遷移選択の Image </summary>
    public Image _toTitleImage;
    /// <summary> Dropdownボタンを押した時に表示される低α値の Panel </summary>
    public Image _helpPanel;

    void Start()
    {
        //始めは、全てのHelpImage を表示しない状態にしておく
        _moveImage.gameObject.SetActive(false);
        _ruleImage.gameObject.SetActive(false);
        _toTitleImage.gameObject.SetActive(false);
        _helpPanel.gameObject.SetActive(false);
    }

    public void ChangeImage() //Updateでやると、書いて実行したことが上書きされてしまう(実行されていないように見える)!!
    {
        //DropDownButton の Value が0("help"が選択されている ... 何も選択されていない)の時
        if (dropDown.value == 0)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("none");
            //全てのHelpImage を表示しない
        }
        //DropDownButton の Value が1("moving"が選択されている)の時
        else if (dropDown.value == 1)
        {
            _moveImage.gameObject.SetActive(true);
            _ruleImage.gameObject.SetActive(false); 
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("moving");
            //PieceMoveImage を表示する
        }
        //DropDownButton の Value が2("rule"が選択されている)の時
        else if (dropDown.value == 2)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(true);
            _toTitleImage.gameObject.SetActive(false);
            Debug.Log("rule");
            //RuleImage を表示する
        }
        //DropDownButton の Value が3("to title"が選択されている)の時
        else if (dropDown.value == 3)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(true);
            Debug.Log("to title");
            //ToTitleImage を表示する
        }
        //DropDownButton の Value が4("back"が選択されている)の時
        else if (dropDown.value == 4)
        {
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _toTitleImage.gameObject.SetActive(false);
            _helpPanel.gameObject.SetActive(false);
            Debug.Log("back");
            //全てのHelpImage を表示しない(ゲームに戻る)
        }
    }
}
