using DAS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{

    private GameObject cachedPrefabPlayer;

    private BAPlayerSpawn spawnAnimation = new BAPlayerSpawn();

    public ControllerKey GetKey()
    {
        return ControllerKey.PlayerController;
    }

    public void PreLoad()
    {
        var playerPrefab = Resources.Load<GameObject>("Prefab/PlayerPrefab");

        cachedPrefabPlayer = Instantiate(playerPrefab, transform);
        cachedPrefabPlayer.SetActive(false);

        spawnAnimation.PreLoad(cachedPrefabPlayer.transform);

    }
    public void Setup()
    {
        AnimationUtil.AddAnytimeAnimation(spawnAnimation);
    }

    public void Refresh()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Setup();
        }
    }

}