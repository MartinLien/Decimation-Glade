using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _textElement = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private AnimationCurve _animationCurve = null;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private bool _fadeOutOnStart = false;

    private bool _toggled = false;
    private Coroutine _coroutine = null;

    private void Start()
    {
        if (_fadeOutOnStart)
        {
            _canvasGroup.alpha = 1;
            _coroutine = StartCoroutine(Toggle(false));
        }
        else
        {
            _canvasGroup.alpha = 0;
        }
    }

    public void SetText(string text)
    {
        _textElement.text = text;
    }

    public void ToggleUI(bool on)
    {
        if (on == _toggled)
            return;
        _toggled = on;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Toggle(on));
    }

    private IEnumerator Toggle(bool on)
    {
        float timer = 0;
        while (timer < _duration)
        {
            float t = on ? timer / _duration : 1 - timer / _duration;
            _canvasGroup.alpha = _animationCurve.Evaluate(t);

            timer += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = on ? 1 : 0;

        _coroutine = null;
    }
}
