using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public Rigidbody2D Rb;
    public Vector2 Force;
    public float AliveTime;
    private void OnEnable()
    {
        Rb.AddForce(Force);
        Debug.Log("aqui");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.gameObject.GetComponent<CharacterInstance>();

        if (character != null)
        {
            character.TakeDamage(transform.position);
        }
    }
    private void FixedUpdate()
    {
        AliveTime -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.S))
            Rb.AddForce(Force);

        if (AliveTime <= 0)
            Destroy(gameObject);
    }
}
