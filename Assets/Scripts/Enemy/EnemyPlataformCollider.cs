using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlataformCollider : MonoBehaviour
{
#if UNITY_EDITOR
    private BoxCollider2D myCollider;
    public BoxCollider2D parentCollider;
    private void OnValidate()
    {

        if(parentCollider != null) 
        {
            if (myCollider == null)
                myCollider = GetComponent<BoxCollider2D>();

            myCollider.size = parentCollider.size;
        }

        
    }
#endif
}
