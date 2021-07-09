using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyForTime : MonoBehaviour
{
    public float timeToDestory = 9f;
    public float currentTime = 0;

    void Update(){
        currentTime += Time.deltaTime;
        if(currentTime > timeToDestory) {
            Destroy(this.gameObject);                  //Destroy Any Objects
        }
    }
}
