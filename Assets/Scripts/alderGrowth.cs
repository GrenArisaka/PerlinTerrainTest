using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alderGrowth : MonoBehaviour
{
    public bool isGrowthEnabled = false;
    public GameObject alder;
    public GameObject raycastSource;
    public float spreadRadius = 5.0f;
    public float growthSpeed = 1.0f;
    public float spreadSpeed = 1.0f;
    public int newTreeAmount = 10; // mathf.random * this var, every five seconds.
    public float nextActionTime = 0.0f;
    public float Growthperiod = 10.0f;
    public int Deathperiod = 3;
    int dAmount = 0;
   
    // Tree growth eq:
    /*

        - get random position, raycast down and if it hits terrain, instantiate a tree. Wait.
     
         
         */



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool nowdie = false;

        // Growth Counter
        if (Time.time > nextActionTime)
        {
            
            nextActionTime += Growthperiod;
            // execute block of code here
            if (dAmount >= Deathperiod)
            {
                Destroy(gameObject);
            }
            if (isGrowthEnabled) {
                bool isGround = false;
                Vector3 rayCastPos = gameObject.transform.position +  new Vector3((float)Random.Range(-10,10)/10*spreadRadius, 0, (float)Random.Range(-10, 10) / 10 * spreadRadius);
                RaycastHit hit;
                Debug.Log("RAYCASTING AT" + rayCastPos + " Direction of " + transform.TransformDirection(Vector3.down));
                if (Physics.Raycast(rayCastPos,transform.TransformDirection(Vector3.down),out hit)) {
                    Debug.Log("Raycast HIT");
                    Instantiate(alder, hit.point,Quaternion.identity);
                    
                    
                }

                dAmount++;
            }
        }
        
        // Death Counter
    }
}
