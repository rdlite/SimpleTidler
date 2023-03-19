using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour
{
    [SerializeField] private Image _curtain;
    [SerializeField] private float _changeDuration = .5f;

    private ICoroutineRunner _coroutineRunner;

    public void Init(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void SetActive(bool value, bool instantly, Action callback)
    {
        if (instantly)
        {
            _curtain.gameObject.SetActive(value);
            callback?.Invoke();
        }
        else
        {
            if (value)
            {
                _coroutineRunner.StartCoroutine(ActivationRoutine(callback));
            }
            else
            {
                _coroutineRunner.StartCoroutine(DeactivationRoutine(callback));
            }
        }
    }

    private IEnumerator ActivationRoutine(Action callback)
    {
        _curtain.gameObject.SetActive(true);
        _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, 0f);

        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime / _changeDuration;

            _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, t);

            yield return null;
        }

        callback?.Invoke();
    }

    private IEnumerator DeactivationRoutine(Action callback)
    {
        _curtain.gameObject.SetActive(true);
        _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, 1f);

        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime / _changeDuration;

            _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, 1f - t);

            yield return null;
        }

        _curtain.gameObject.SetActive(false);

        callback?.Invoke();
    }
}