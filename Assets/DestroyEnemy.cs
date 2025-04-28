using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private int deathCount = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Àû µÚÁü");
            deathCount -= 1;
            if (deathCount <= 0)
            {
                Application.Quit();
            }
        }
    }
}
