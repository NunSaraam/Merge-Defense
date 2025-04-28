using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Left":
                Quaternion leftRotation = Quaternion.Euler(0f, 0f, 45f);
                this.transform.rotation = leftRotation * this.transform.rotation;
                break;
            case "Right":
                Quaternion rightRotation = Quaternion.Euler(0f, 0f, -45f);
                this.transform.rotation = rightRotation * this.transform.rotation;
                break;
        }
    }
}
