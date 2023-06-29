using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class CalculatePointingError : MonoBehaviour
{
    
    public bool isStart;
    public List<GameObject> targetList;
    public GameObject _leftController;
    public GameObject hmd;
    public Vector3 head2Object;
    public Vector3 pointingVec;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            Helpers.Shuffle(targetList);
            for (int i = 0; i < targetList.Count; i++)
            {
                print(targetList[i].name);
            }
            isStart = false;


            
        }

        // write a helper fucntion, double check it is correct.
        head2Object = (targetList[0].transform.position - hmd.transform.position);
        head2Object = Vector3.ProjectOnPlane(head2Object, Vector3.up);
        Debug.DrawRay(hmd.transform.position, head2Object, Color.red, 0.1f);

        pointingVec = Vector3.ProjectOnPlane(_leftController.transform.forward, Vector3.up);
        Debug.DrawRay(_leftController.transform.position, pointingVec, Color.green, 0.1f);

        Debug.Log(Vector3.Angle(head2Object, pointingVec));
        
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
            int r = Random.Range(n, list.Count);
            list[n] = list[r];
            list[r] = tmp;
        }
    }
}
