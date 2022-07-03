using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void OnClickStartButton(string scenename)
    {
        SceneManager.LoadScene(scenename); //ボタンが押されたら、指定したシーン名(string型)のシーンに遷移する
    }
}
