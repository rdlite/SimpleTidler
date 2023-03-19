using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : UIPanel
{
    [SerializeField] private AnimationCurve _appearanceCurve;
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private CanvasGroup _top, _bottom;
    [SerializeField] private Button _restartButton;

    private Vector3 _defaultTopPosition, _defaultBottomPosition;

    protected override void LocalInit()
    {
        _defaultTopPosition = _top.transform.position;
        _defaultBottomPosition = _bottom.transform.position;
        _restartButton.onClick.AddListener(RestartButtonPressed);
    }

    public override void Enable()
    {
        base.Enable();
        StartCoroutine(ActivationRoutine());
    }

    private IEnumerator ActivationRoutine()
    {
        _top.interactable = false;
        _bottom.interactable = false;
        _top.alpha = 0f;
        _bottom.alpha = 0f;

        float t = 0f;

        Vector3 startTopPosition = _defaultTopPosition + Vector3.up * Screen.height / 50f;
        Vector3 startBottomPosition = _defaultBottomPosition - Vector3.up * Screen.height / 50f;
        Vector3 endTopPosition = _defaultTopPosition;
        Vector3 endBottomPosition = _defaultBottomPosition;

        while (t <= 1f)
        {
            t += Time.deltaTime;

            _top.transform.position = Vector3.Lerp(startTopPosition, endTopPosition, _appearanceCurve.Evaluate(t));
            _bottom.transform.position = Vector3.Lerp(startBottomPosition, endBottomPosition, _appearanceCurve.Evaluate(t));

            _top.alpha = _alphaCurve.Evaluate(t);
            _bottom.alpha = _alphaCurve.Evaluate(t);

            yield return null;
        }

        _top.interactable = true;
        _bottom.interactable = true;
        _top.alpha = 1f;
        _bottom.alpha = 1f;
    }

    private void RestartButtonPressed()
    {
        _globalSM.StatesData.Curtain.SetActive(true, false, () => _globalSM.Enter<LoadLevelState>());
    }
}