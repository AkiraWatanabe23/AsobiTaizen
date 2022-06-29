using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DropdownButton が押された時の処理
/// </summary>
public class DropDown : MonoBehaviour
{
    [SerializeField] Dropdown dropDown;
    //[SerializeField] Images images = Images.Help;
    [SerializeField] Image _ruleImage;
    [SerializeField] Image _moveImage;
    [SerializeField] Image _helpPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //DropDownButton の Value が0("to title"が選択されている)の時
        if (dropDown.value == 0)
        {
            //Images images = Images.Help;
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);

        }
        //DropDownButton の Value が1("back"が選択されている)の時
        else if (dropDown.value == 1)
        {
            //Images images = Images.Rule;
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(false);
            _helpPanel.gameObject.SetActive(false);
        }
        //DropDownButton の Value が2("help"が選択されている)の時
        else if (dropDown.value == 2)
        {
            //Images images = Images.ToTitle;
            _moveImage.gameObject.SetActive(true);
            _ruleImage.gameObject.SetActive(false);
        }
        //DropDownButton の Value が3("rule"が選択されている)の時
        else if (dropDown.value == 3)
        {
            //Images images = Images.Back;
            _moveImage.gameObject.SetActive(false);
            _ruleImage.gameObject.SetActive(true);
        }
    }

    //public enum Images
    //{
    //    /// <summary> HelpPage </summary>
    //    Help,
    //    /// <summary> RulePage </summary>
    //    Rule,
    //    /// <summary> ToTitlePage </summary>
    //    ToTitle,
    //    /// <summary> BackButton </summary>
    //    Back,
    //}
}
