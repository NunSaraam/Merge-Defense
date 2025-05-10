using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            RotateOnTurnPoint(collision);
        }

        private void RotateOnTurnPoint(Collider2D collision)
        {
            if (collision.CompareTag("Left"))
                Rotate(45f);
            else if (collision.CompareTag("Right"))
                Rotate(-45f);
        }

        private void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle) * transform.rotation;
        }
    }
}