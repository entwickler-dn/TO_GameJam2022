using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutoText;
    public string[] tutorialTexts;
    public int tutoIndex = 0;
    bool changeIndex = false;
    bool finishedTutorial = false;

    public int totalPoints;
    public GameObject tutorialMap;
    public GameObject currentMap;

    public TextMeshProUGUI pointCounter;
    public bool canAddPoints = true;

    public AudioSource gameMusic;

    public GameObject deathPanel;

    void Awake()
    {
    }

    void Start()
    {
        gameMusic.volume = MainMenu.gameVolume;

        deathPanel.SetActive(false);

        currentMap = Instantiate(tutorialMap, Vector3.zero, Quaternion.identity);

        AstarPath.active.Scan();
    }

    public void AddPoints(int amount)
    {
        canAddPoints = false;
        totalPoints += amount;
    }

    void Update()
    {
        //switch(tutoIndex)
        //{
        //    case 0:
        //        tutoText.text = tutorialTexts[0];
        //        break;
        //    case 1:
        //        tutoIndex = 1;
        //        break;
        //    case 2:
        //        tutoIndex = 2;
        //        break;
        //    case 3:
        //        tutoIndex = 3;
        //        break;
        //    case 4:
        //        tutoIndex = 4;
        //        break;
        //}

        pointCounter.text = totalPoints.ToString("0000000");

        tutoText.text = tutorialTexts[tutoIndex];

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health <= 0)
        {
            deathPanel.SetActive(true);
        }

        if (deathPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("main menu");
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D)) && !changeIndex && tutoIndex == 0)
        {
            changeIndex = true;
            tutoIndex++;
            changeIndex = false;
        }

        else if ((GameObject.FindGameObjectWithTag("EnemyKnockedOut") != null) && !changeIndex && tutoIndex == 1)
        {
            changeIndex = true;
            tutoIndex++;
            changeIndex = false;
        }

        else if((GameObject.FindGameObjectWithTag("EnemyKnockedOut") != null)  && !changeIndex && tutoIndex == 2)
        {
            if ((GameObject.FindGameObjectWithTag("EnemyKnockedOut").GetComponent<EnemyHealth>().isGrabbed))
            {
                changeIndex = true;
                tutoIndex++;
                changeIndex = false;
            }
        }

        //else if ((!GameObject.FindGameObjectWithTag("EnemyKnockedOut").GetComponent<EnemyHealth>().isGrabbed && (GameObject.FindGameObjectWithTag("EnemyKnockedOut") == null ||
        //    GameObject.FindGameObjectWithTag("Enemy") == null)) && !changeIndex && tutoIndex == 3)
        else if ((totalPoints > 0) && !changeIndex && tutoIndex == 3)
        {
            changeIndex = true;
            tutoIndex++;
            changeIndex = false;
        }

        else if (!finishedTutorial && !changeIndex && tutoIndex == 4)
        {
            Invoke("WaitACoupleOfSeconds", 2f);
            finishedTutorial = true;
        }

        if(finishedTutorial)
        {
            //MainMenu.skipTutorial = true;
            Invoke("LoadMainGame", 2.5f);
        }
    }

    void WaitACoupleOfSeconds()
    {
        changeIndex = true;
        tutoIndex++;
        changeIndex = false;
    }

    void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
