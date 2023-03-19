using System;
using UnityEngine;

public class TriggerCatcher : MonoBehaviour
{
    public event Action<BaseEnemy> OnEnemyCatched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            OnEnemyCatched?.Invoke(enemy);
        }
    }
}