using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalPoints;
    public GameObject[] maps;
    public GameObject tutorialMap;
    public GameObject currentMap;

    public TextMeshProUGUI pointCounter;

    public bool skipTutorial = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(skipTutorial)
        {
            currentMap = Instantiate(maps[Random.Range(0, maps.Length - 1)], Vector3.zero, Quaternion.identity);
        }
        else
        {
            currentMap = Instantiate(tutorialMap, Vector3.zero, Quaternion.identity);
        }
        
        AstarPath.active.Scan();
    }

    void Update()
    {
        pointCounter.text = totalPoints.ToString("0000000");
    }

    public void AddPoints(int amount)
    {
        totalPoints += amount;
    }
}
