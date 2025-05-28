using UnityEngine;

namespace TowerDefense.Game
{
    public class EnemyDestroyer : MonoBehaviour
    {
        [SerializeField] private int maxDeath = 3;
        private int currentDeaths;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;

            Destroy(collision.gameObject);
            currentDeaths++;
            Debug.Log("Enemy destroyed. Count: " + currentDeaths);

            if (currentDeaths >= maxDeath)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}