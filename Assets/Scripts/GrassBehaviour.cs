using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{
    public GameObject fractureGrass;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().gameObject.CompareTag("Cow")) {
            Instantiate(fractureGrass, transform.position, transform.rotation);               //Interactive With Cow
            gameController.PlantationHit();
            Destroy(this.gameObject);
        }
    }
}
