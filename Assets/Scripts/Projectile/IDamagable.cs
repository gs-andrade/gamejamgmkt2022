using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void TakeDamage(DamageSettings damageSettings);

    bool IsAlive();

    Vector2 GetPosition();

    Transform GetTransform();
}
