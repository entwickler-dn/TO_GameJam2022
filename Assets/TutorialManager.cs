using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutoText;
    public string[] tutorialTexts;
    public int tutoIndex = 0;
    bool changeIndex = false;
    bool finishedTutorial = false;
    void Start()
    {
        
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

        tutoText.text = tutorialTexts[tutoIndex];

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D)) && !changeIndex && tutoIndex == 0)
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
        else if ((GameManager.instance.totalPoints > 0) && !changeIndex && tutoIndex == 3)
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
    }

    void WaitACoupleOfSeconds()
    {
        changeIndex = true;
        tutoIndex++;
        changeIndex = false;
    }
}
