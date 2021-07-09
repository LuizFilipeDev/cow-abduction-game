using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowBehaviour : MonoBehaviour
{
    public float currentSpeed = 3f; //Speed Cow
    public float timeMinRandom;    //minimum time for the cow to reach the goal
    public float timeMaxRandom;    //maximum time for the cow to reach the goal
    public float limitWalkMaxX;     //map limit for the walking cow // max
    public float limitWalkMinX;     //map limit for the walking cow // min
    public float limitWalkMaxZ;     //map limit for the walking cow // max
    public float limitWalkMinZ;     //map limit for the walking cow // min
    public bool cowAlert;
    public bool abductionCow = false;

    private Animator anim;
    private Rigidbody rigid;
    private NavMeshAgent agent;
    private Vector3 randomPosition;
    private float currentTime = 0;
    private float timeToNewMovement;
    private Vector3 fixPositionAnimation;

    void Start(){
        anim = this.GetComponentInChildren<Animator>();
        rigid = this.GetComponent<Rigidbody>();                                                    //Get Components Automatic
        agent = this.GetComponent<NavMeshAgent>();

        randomPosition = new Vector3(Random.Range(limitWalkMinX, limitWalkMaxX), 0, Random.Range(limitWalkMinZ, limitWalkMaxZ));
        fixPositionAnimation = randomPosition;
        agent.SetDestination(randomPosition);                                                      //Inicial Ai
        timeToNewMovement = Random.Range(timeMinRandom, timeMaxRandom);
        anim.SetBool("isWalk", true);
    }
    void Update(){

        agent.speed = currentSpeed;

        //Random Cow Target Position
        randomPosition = new Vector3(Random.Range(limitWalkMinX, limitWalkMaxX), 0, Random.Range(limitWalkMinZ, limitWalkMaxZ));

        if (!abductionCow) {
            //Time to Reposition Cow Target
            RepositionCowTarget();
            //Cow On Alert
            CowWarning();
        }
        else if(abductionCow){
            agent.enabled = false;
            Vector3 currentPosition = transform.position;
            currentPosition.y = currentPosition.y += 10f * Time.deltaTime;   //Abduction System
            anim.SetBool("Abduction", true);
            transform.position = currentPosition;

            if(transform.position.y > 20f) {
                Destroy(this.gameObject);                                  //Absuction OK
            }
        }
    }
    //Cow Walk in Scenario With Limitation
    public void RandomPosition(Vector3 randomPositionCow) {
        agent.SetDestination(randomPositionCow);
        timeToNewMovement = Random.Range(timeMinRandom, timeMaxRandom);
    }
    //Time to Reposition Cow Target
    public void RepositionCowTarget() {
        currentTime = currentTime += Time.deltaTime;
        if (currentTime > timeToNewMovement)
        {
            fixPositionAnimation = randomPosition;
            RandomPosition(randomPosition);
            currentTime = 0;
        }
        if (fixPositionAnimation.x == transform.position.x)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
    }
    //Cow On Alert State
    public void CowWarning() {
        if (cowAlert){
            timeToNewMovement = 10f;
            agent.speed = 2f;
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", true);
        }else if (!cowAlert) {
            anim.SetBool("isRun", false);
        }
    }
}
