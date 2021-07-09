using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiBehaviour : MonoBehaviour
{
    void Update()
    {
        Debug.Log(Time.timeScale);
        if(Time.timeScale == 0) {           //TimeScale Fix To Load Scene Again
            Time.timeScale = 1;
        }
    }
    public void StartGame() {
        SceneManager.LoadScene("GamePlay");
    }
}
