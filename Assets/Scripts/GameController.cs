using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

[System.Serializable]
public class Levels{
    public float spawnDelay = 5f; //on
    public float speedMovementCow = 3f;  //on
    public bool cowAlert;  // off
}
public class GameController : MonoBehaviour
{
    public float currentSpawnDelay = 5f;
    public bool limitLevel;                   //Level limit not to bug the game
    public bool spawnable = false;
    public TMPro.TextMeshProUGUI plantationNumbersInSceneText;
    public TMPro.TextMeshProUGUI cowNumbersGetText;
    public TMPro.TextMeshProUGUI levelNumbersText;
    public GameObject cow;
    public Transform[] spawnPoint;
    public GameObject[] UiController;
    public Levels[] level;

    public static int cowNumbersGet = 0;       //Cow Numbers In Scene

    private float currentTimeSpawn = 0;
    private int randomSpawnValue;
    private int currentLevel = 0;
    private float percentGrass = 100;
    [HideInInspector]
    public int plantationNumberInScene = 80;   //Plantation Numbers In Scene
    private float saveTimeScale;
    private bool isPause = true;
    private bool disableTransition = false;

    private void Start()
    {
        Time.timeScale = 1;
        Transition.inicial = false;
        saveTimeScale = Time.timeScale;
        UiControllerSystem(3);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && !isPause)
        {                                                              //Pause Game
            isPause = true;
            UiControllerSystem(4);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isPause && UiController[0].activeSelf == true) {
            UiControllerSystem(0);
            isPause = false;
        }
        if (currentLevel == 5) {
            UiControllerSystem(2);                                   //WinGame
        }else if(cowNumbersGet == 15) {
            currentLevel++;                                       //Level Up System
            cowNumbersGet = 0;
        }
        randomSpawnValue = Random.Range(0, spawnPoint.Length);

        if (spawnable) {
            currentTimeSpawn += Time.deltaTime;
            if (currentTimeSpawn > currentSpawnDelay)
            {
                Instantiate(cow, spawnPoint[randomSpawnValue].position, spawnPoint[randomSpawnValue].rotation);         //Spawn Cow For Life Time
                CountLevels(currentLevel, cow.GetComponent<CowBehaviour>());   //limit Level
                currentTimeSpawn = 0;              
            }
        }

        //GrassHitPercent //CowNumbersInScene //LevelNumber
        plantationNumbersInSceneText.text = percentGrass.ToString();
        cowNumbersGetText.text = cowNumbersGet.ToString() + " / 15";                   //Show System in Screen
        levelNumbersText.text = currentLevel.ToString();

        UpdatePercentGrass();    //Update Grass

        if (!disableTransition) {
            if (Transition.inicial)
            {
                UiController[0].SetActive(true);
                UiController[1].SetActive(false);
                UiController[2].SetActive(false);
                UiController[3].SetActive(false);
                isPause = false;
                disableTransition = true;
                return;
            }
            else if (!Transition.inicial)
            {
                UiController[0].SetActive(false);
                UiController[1].SetActive(false);
                UiController[2].SetActive(false);
                UiController[3].SetActive(true);
                isPause = true;
            }
        }
    }   
    public void UiControllerSystem(int uiNumber)
    {
        switch (uiNumber)
        {
            case 0:
                UiController[0].SetActive(true);
                UiController[1].SetActive(false);
                UiController[2].SetActive(false);
                UiController[3].SetActive(false);
                UiController[4].SetActive(false);
                Time.timeScale = 1;
                break;
            case 1:           //Ui Controller
                UiController[0].SetActive(false);
                UiController[2].SetActive(false);
                UiController[3].SetActive(false);
                UiController[4].SetActive(false);
                UiController[1].SetActive(true);
                Time.timeScale = 0;
                break;
            case 2:           //Ui Controller
                UiController[0].SetActive(false);
                UiController[1].SetActive(false);
                UiController[3].SetActive(false);
                UiController[4].SetActive(false);
                UiController[2].SetActive(true);
                Time.timeScale = 0;
                break;
            case 3:           //Ui Controller
                UiController[0].SetActive(false);
                UiController[1].SetActive(false);
                UiController[2].SetActive(false);
                UiController[4].SetActive(false);
                UiController[3].SetActive(true);
                Time.timeScale = 0;
                break;
            case 4:           //Ui Controller
                UiController[0].SetActive(true);
                UiController[1].SetActive(false);
                UiController[2].SetActive(false);
                UiController[3].SetActive(false);
                UiController[4].SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
    //PlantationHit
    public void PlantationHit() {
        Debug.Log(plantationNumberInScene);
        plantationNumberInScene -= 1;                    //Plantation decrease
    }
    public void CountLevels(int countLevel, CowBehaviour cow) {
        currentSpawnDelay = level[countLevel].spawnDelay;
        cow.currentSpeed = level[countLevel].speedMovementCow;          //Level System, Change Properties
    }
    //                  Button Options In Screen                    //   Start
    public void TryAgainOrGameplay() {
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        FindObjectOfType<AudioManager>().Play("Theme");
        Time.timeScale = saveTimeScale;
        cowNumbersGet = 0;
        plantationNumberInScene = 80;
        SceneManager.LoadScene("GamePlay");
    }
    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        Application.Quit();
    }
    //                  Button Options In Screen                    //   End
    public void UpdatePercentGrass() {
        switch (plantationNumberInScene)
        {
            case 80:
                percentGrass = 100;
                break;
            case 75:
                percentGrass = 90;
                break;
            case 70:
                percentGrass = 85;
                break;
            case 65:
                percentGrass = 80;
                break;                                                  //Percent Behaviour Grass in Gameplay
            case 60:                                                   //Convert Solid Number To Percent Number
                percentGrass = 75;
                break;
            case 55:
                percentGrass = 70;
                break;
            case 50:
                percentGrass = 65;
                break;
            case 45:
                percentGrass = 60;
                break;
            case 40:
                percentGrass = 55;
                break;
            case 35:
                percentGrass = 50;
                break;
            case 30:
                percentGrass = 45;
                break;
            case 25:
                percentGrass = 40;
                break;
            case 20:
                percentGrass = 35;
                break;
            case 15:
                percentGrass = 30;
                break;
            case 10:
                percentGrass = 25;
                break;
            case 5:
                percentGrass = 20;
                break;
            case 0:
                percentGrass = 0;
                UiControllerSystem(1);
                break;
            default:
                break;
        }
    }
}
