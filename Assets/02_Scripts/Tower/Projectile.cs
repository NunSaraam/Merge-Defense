using UnityEngine;
using TowerDefense.Enemy;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed = 12f;
    private float damage;

    public void Init(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            if (target.TryGetComponent(out EnemyHealth health))
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}