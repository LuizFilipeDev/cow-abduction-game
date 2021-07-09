using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Color color;
    private bool isContinue = false;
    private Graphic[] images;
    private float currentTime = 0;
    private float timeToDisable = 20f;

    public static bool inicial = false;
    void Start()
    {
        images = GetComponentsInChildren<Graphic>();
    }
    void Update()
    {
        if (isContinue) {
            for(int x = 0;x < images.Length; x++) {
                images[x].color = color;
                Time.timeScale = 1;
                currentTime += Time.deltaTime;
                if(currentTime > timeToDisable) {
                    inicial = true;
                    currentTime = 0;
                }
            }
        }
    }
    public void Continue() {
        isContinue = true;
        FindObjectOfType<AudioManager>().Play("ClickMouse");
        Time.timeScale = 1;
    }
}
