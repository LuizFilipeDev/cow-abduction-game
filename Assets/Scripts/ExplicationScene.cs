using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ExplicationScene : MonoBehaviour
{
    public void MainMenu() {
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        SceneManager.LoadScene("Menu");
    }
    public void Play() {
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        SceneManager.LoadScene("GamePlay");
    }
    public void Explication()
    {
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        SceneManager.LoadScene("Explication");
    }
}
