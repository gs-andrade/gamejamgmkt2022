using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorGroup : MonoBehaviour, IUpdatable, IResetable
{
    public int TargetNumber;
    public int CurrentValue;
    public float OpeningSpeed;
    public Transform Door;
    public TextMesh LifeText;

    private EnemyGeneric[] enemies;
    private Vector2 drawPoint;
    private Vector2 initialPosition;
    public void ResetObject()
    {
        Door.position = initialPosition;
    }

    public void SetupOnStartLevel()
    {
        enemies = GetComponentsInChildren<EnemyGeneric>(true);
        drawPoint = GetComponentInChildren<DrawPoint>(true).transform.position;
        initialPosition = Door.position;
        LifeText.text = TargetNumber.ToString();
    }

    public void UpdateObj()
    {
        CurrentValue = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            CurrentValue += enemies[i].GetCurrentLife();
        }

        if (CurrentValue == TargetNumber)
        {

            if (Vector2.Distance(Door.position, drawPoint) <= 0.05f)
            {
                return;
            }

            Door.position = Vector2.MoveTowards(Door.position, drawPoint, OpeningSpeed * Time.deltaTime);


        }
        else
        {
            if (Vector2.Distance(Door.position, initialPosition) <= 0.05f)
            {
                return;
            }

            Door.position = Vector2.MoveTowards(Door.position, initialPosition, 2 * OpeningSpeed * Time.deltaTime);
        }

    }

}

