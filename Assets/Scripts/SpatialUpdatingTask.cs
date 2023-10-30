using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Oculus.Platform.Models;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class SpatialUpdatingTask : MonoBehaviour
{

    // refs:
    // https://blog.gladiogames.com/all-posts/unityrandom-vs-systemrandom-explained-examples
    // https://math.stackexchange.com/questions/143932/calculate-point-given-x-y-angle-and-distance

    // 1. task start
    // 2. show one green pole (randomly selected)
    //    - calculate the following angles for yellow and red poles.
    // 3. participant reaches the green pole, record reaction time
    //    - show yellow
    // 4. participant reaches the yellow pole, record reaction time
    //    - show red
    // 5. participant reaches the red pole, record reaction time
    //    - everything disappear
    //    - show raycasting
    //    - pointing to the green pole (origin), record reaction time, position error, angular error
    // 6. repeat 8 times (8 poles)
    // 7. testing SMM with (need more pseudo code)
    //    - pointing towards landmarks
    //    - statements
    //    - self location
    // 

    public int trial;
    public bool isStudyStart;
    public List<GameObject> targetList;
    public List<float> turnAnglesList = new List<float>() {-135.0f, -101.25f, -67.5f, -33.75f, 33.75f, 67.5f, 101.25f, 135f};
    public float greenYellowPathLength;
    public float yellowRedPathLength;
    public GameObject greenPole;
    public GameObject yellowPole;
    public GameObject redPole;
    float x;
    float y;
    float z;

    [Header("Participants")]
    public int seed = 123; // Need to consider conditions if we want to replicate...
    public int participantNum = 0;

    // Start is called before the first frame update
    void Start()
    {

        //Create a seed
        seed = 123 + participantNum;
        //Set a seed in the Random Generator
        // UnityEngine.Random.InitState(seed);
        /* System.Random version
         * System.Random random = new System.Random(seed);
         */

        Helpers.Shuffle(targetList);
        Helpers.Shuffle(turnAnglesList);
        foreach(GameObject target in targetList) target.SetActive(false);
        trial = 0;
        isStudyStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trial < targetList.Count)
        {
            if(Input.GetKeyDown("space"))
            {
                // create a path with three poles
                targetList[trial].SetActive(true);
                greenPole = targetList[trial];
                greenYellowPathLength = UnityEngine.Random.Range(1.4f, 2.0f);
                yellowRedPathLength =  UnityEngine.Random.Range(1.4f, 2.0f);
                if (greenPole.transform.position.x > 0)
                {
                    x = greenPole.transform.position.x - Mathf.Abs(greenYellowPathLength * Mathf.Cos(Helpers.DegreeToRadian(turnAnglesList[trial])));
                }
                // else if (greenPole.transform.position.x == 0)
                // {
                //     x = 0;
                // }
                else
                {
                    x = greenPole.transform.position.x + Mathf.Abs(greenYellowPathLength * Mathf.Cos(Helpers.DegreeToRadian(turnAnglesList[trial])));
                }

                if (greenPole.transform.position.z > 0)
                {
                    z = greenPole.transform.position.z - Mathf.Abs(greenYellowPathLength * Mathf.Cos(Helpers.DegreeToRadian(turnAnglesList[trial])));
                }
                // else if (greenPole.transform.position.z == 0)
                // {
                //     z = 0;
                // }
                else
                {
                    z = greenPole.transform.position.z + Mathf.Abs(greenYellowPathLength * Mathf.Cos(Helpers.DegreeToRadian(turnAnglesList[trial])));
                }
             
                y = yellowPole.transform.position.y;

                yellowPole.transform.position = new Vector3(x, y, z);


                trial++;
            }
        }
        else
        {
            // end condition
            
        }
    }
}


public static class Helpers
{
    public static void Shuffle<T>(this IList<T> list)
    {
        // https://forum.unity.com/threads/randomize-array-in-c.86871/
        // https://stackoverflow.com/questions/273313/randomize-a-listt
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int n = 0; n < list.Count; n++)
        {
            T tmp = list[n];
            int r = UnityEngine.Random.Range(n, list.Count);
            list[n] = list[r];
            list[r] = tmp;
        }
    }

    public static float DegreeToRadian(float deg)
    {
        return deg * Mathf.PI / 180;
    }
}