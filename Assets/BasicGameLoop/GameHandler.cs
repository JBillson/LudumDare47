using System;
using BasicGameLoop;
using FpsController;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameHandler : MonoBehaviour
{
    public Action GameWon;
    public Action GameLoss;

    private Transform winPoint;
    private Transform playerPos;

    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private GameObject LoseCanvas;

    [SerializeField] private float DungeonTime;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
        winPoint = FindObjectOfType<WinPoint>().transform;
        GameWon += DungeonComplete;
        GameLoss += GameOver;
    }


    private void Update()
    {
        DungeonTime -= Time.deltaTime;
        
        if (CheckDistToPlayer())
        {
            GameWon?.Invoke();
        }

        if (DungeonTime <= 0)
        {
            GameLoss?.Invoke();
        }
    }

    private bool CheckDistToPlayer()
    {
        var dist = Vector3.Distance(winPoint.position, playerPos.position);
        Debug.Log(dist);
        Debug.Log(winPoint.gameObject.name);
        return dist <= 3;
    }

    private void DungeonComplete()
    {
        WinCanvas.SetActive(true);
    }
    
    private void GameOver()
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
}