using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicote : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyGeneric>();

        if(enemy != null)
        {
            enemy.TakeDamage(transform.position);
        }
    }
}
