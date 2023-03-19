using TMPro;
using UnityEngine;

public class MoneyPopup : MonoBehaviour
{
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _lifetime = 1f;
    [SerializeField] private float _movementUpwardSpeed = 1f;

    private float _maxLifetime;

    public void Init(int moneyByKill)
    {
        _text.text = $"{moneyByKill}$";
    }

    private void Awake()
    {
        _maxLifetime = _lifetime;
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;

        transform.position += Vector3.up * Time.deltaTime * _movementUpwardSpeed;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _alphaCurve.Evaluate(1f - _lifetime / _maxLifetime));

        if (_lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}