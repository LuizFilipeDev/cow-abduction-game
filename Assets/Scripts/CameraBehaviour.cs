using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public float maxZ, minZ, maxX, minX;
    public float speedMovement = 20f;

    void Update(){
        CamMovement();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.gameObject.CompareTag("Cow"))
                {
                        FindObjectOfType<AudioManager>().Play("CowAbduction");
                    CowBehaviour currentCow = hit.collider.gameObject.GetComponent<CowBehaviour>();                //Mouse Hit Cow
                    if (!currentCow.abductionCow) {
                        GameController.cowNumbersGet++;
                    }
                    currentCow.abductionCow = true;
                }
            }
        }
    }
    //Move camera with the keys
    public void CamMovement() {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += speedMovement * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= speedMovement * Time.deltaTime;                  //Camera Movement
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speedMovement * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speedMovement * Time.deltaTime;
        }
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}
