using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour // MonoBehaviour ���p��
{
    public void OnClickStartButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
        //�{�^���������ꂽ��A�w�肵���V�[����(string�^)�̃V�[���ɑJ�ڂ���
    }
}
