using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeLoad : MonoBehaviour // MonoBehaviour Çåpè≥
{
    [SerializeField] string _sceneName;
    Image _fadePanel;
    float _fadeTime = 0f;

    void Start()
    {
        _fadePanel = GameObject.Find("Fade").GetComponent<Image>();
        _fadePanel.raycastTarget = false;
        _fadeTime = 0f;
    }

    public void LoadScene()
    {
        StartCoroutine(FadeOut(1.5f));
        Invoke("Load", 1.5f);
    }

    IEnumerator FadeOut(float interval)
    {
        Color color = _fadePanel.color;
        color.a = 0f;
        _fadePanel.color = color;

        do
        {
            yield return null;
            _fadeTime += Time.unscaledDeltaTime;
            color.a = _fadeTime / interval;
            _fadePanel.color = color;

            if (color.a >= 1f)
            {
                color.a = 1f;
            }
            _fadePanel.color = color;
        }
        while (_fadeTime <= interval);
    }

    void Load()
    {
        SceneManager.LoadScene(_sceneName);
    }
}