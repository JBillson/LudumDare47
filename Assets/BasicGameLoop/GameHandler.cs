using System;
using BasicGameLoop;
using FpsController;
using UnityEngine;
using UnityEngine.Serialization;

public class GameHandler : MonoBehaviour
{
    public Action GameWon;

    private Transform winPoint;
    private Transform playerPos;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
        winPoint = FindObjectOfType<WinPoint>().transform;
        GameWon += GameOver;
    }

    private void Update()
    {
        if (CheckDistToPlayer())
        {
            GameWon?.Invoke();
        }
    }

    private bool CheckDistToPlayer()
    {
        var dist = Vector3.Distance(winPoint.position, playerPos.position);

        return dist <= 1;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}