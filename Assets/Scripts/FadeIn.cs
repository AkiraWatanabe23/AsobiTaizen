using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image _fadePanel;
    float _fadeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _fadeTime = 0f;
        _fadePanel.raycastTarget = false;
        StartCoroutine(Fade(1.5f));
    }

    IEnumerator Fade(float interval)
    {
        Color color = _fadePanel.color;
        color.a = 1f;
        _fadePanel.color = color;

        do
        {
            yield return null;
            _fadeTime += Time.unscaledDeltaTime;
            color.a = 1f - (_fadeTime / interval);
            _fadePanel.color = color;

            if (color.a <= 0f)
            {
                color.a = 0f;
            }
            _fadePanel.color = color;
        }
        while (_fadeTime <= interval);
    }
}
