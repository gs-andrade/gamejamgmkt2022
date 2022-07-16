using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void TakeDamage(Vector2 damagerDirection);

    bool IsAlive();

    Vector2 GetPosition();

    Transform GetTransform();
}
