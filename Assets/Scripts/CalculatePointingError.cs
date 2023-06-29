using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePointingError : MonoBehaviour
{
    
    public bool isStart;
    public List<GameObject> targetList;

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
