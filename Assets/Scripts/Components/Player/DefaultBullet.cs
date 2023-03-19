using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _damage = 12f;

    private Vector3 _movementDirection;
    private float _destroyTimer = 5f;
    private bool _isDestroyed;

    public void Init(Transform target, UpgradesController upgradesController)
    {
        _trailRenderer.emitting = true;
        _movementDirection = (target.transform.position - transform.position).normalized;
        _damage += upgradesController.GetUpgradeValue(UpgradeType.Damage);
    }

    private void Update()
    {
        if (_isDestroyed)
        {
            return;
        }

        _destroyTimer -= Time.deltaTime;

        transform.position += _movementDirection * _movementSpeed * Time.deltaTime;

        if (_destroyTimer <= 0f)
        {
            _isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isDestroyed && collision.TryGetComponent(out BaseEnemy enemy))
        {
            _isDestroyed = true;
            _trailRenderer.transform.SetParent(null);
            _trailRenderer.autodestruct = true;

            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}