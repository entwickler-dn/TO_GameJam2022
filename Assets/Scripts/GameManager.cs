using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalPoints;
    public GameObject[] maps;
    public GameObject tutorialMap;
    public GameObject currentMap;

    public TextMeshProUGUI pointCounter;
    public bool canAddPoints = true;

    public AudioSource gameMusic;

    public GameObject deathPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        deathPanel.SetActive(false);

        gameMusic.volume = MainMenu.gameVolume;

        currentMap = Instantiate(maps[Random.Range(0, maps.Length - 1)], Vector3.zero, Quaternion.identity);

        AstarPath.active.Scan();
    }

    void Update()
    {
        pointCounter.text = totalPoints.ToString("0000000");

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health <= 0)
        {
            deathPanel.SetActive(true);
        }

        if(deathPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("main menu");
            }
        }
    }

    public void AddPoints(int amount)
    {
        canAddPoints = false;
        totalPoints += amount;
    }
}
