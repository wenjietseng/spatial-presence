using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculatePointingError : MonoBehaviour
{
    public bool isDebug;
    public OVRInput.Controller _leftController;
    public OVRInput.Controller _rightController;
    public List<GameObject> targetList;
    public TMP_Text taskInstruction;
    public bool isStartTask;
    private int taskNum;
    private string env;
    [SerializeField] private float t0;
    [SerializeField] private float t1; // the time when participants are done accessing VR/RW SMM
    [SerializeField] private float t2; // the time when participants confirm the assigned object direction
    private bool taskIsRunning;
    private bool pointingStart;
    private bool isPause;

    [Header("Calculating Pointing Angular Error")]
    public Vector3 controllerToObjectVec;
    public Vector3 pointingVector;
    public GameObject rayPointer;
    public float angularError;

    
    void Start()
    {
        // Shuffle the order of pointing task
        Helpers.Shuffle(targetList);
        taskNum = 0;    

    }

    // Update is called once per frame
    void Update()
    {
        if (taskNum < targetList.Count)
        {
            if (isStartTask)
            {
                isStartTask = false; // a task is initaited
                taskIsRunning = true;
                t0 = 0.0f;
                t1 = -1.0f;
                t2 = -1.0f;

                if (targetList[taskNum].tag == "VirtualObjects") env = "VR";
                else env = "Real-World";

                // ask participants to access VR/RW SMM.
                taskInstruction.text = "Please think about the " + env + " environment and\n" + "push the trigger when you are ready";
            }
            else
            {
                if (taskIsRunning)
                {
                    t0 += Time.deltaTime;
                    
                    if (t1 <= 0.0f)
                    {
                        if (OVRInput.GetDown(OVRInput.Button.One, _rightController))
                        {
                            // record t1 when SMM is ready
                            t1 = t0;
                            taskInstruction.text = "Please point toward the center of " + targetList[taskNum].name + " and\n" + "push the trigger to confirm";
                            pointingStart = true;
                        }
                    }
                    else
                    {
                        if (pointingStart)
                        {
                            ComputeAngularError();
                            if (OVRInput.GetDown(OVRInput.Button.One, _rightController))
                            {
                                // record t2 when pointing toward the target and confirm
                                t2 = t0;
                                // calculate pointing error                                
                                Debug.Log(angularError); // record data
                                pointingStart = false;
                                StartCoroutine(ShortBreakForPointingTask(3.0f));
                            }
                        }
                    }
                }
                else
                {
                    if (isPause)
                    {
                        t0 -= Time.deltaTime;
                        taskInstruction.text = t0.ToString();
                    }
                }

            }
        }


        if (isDebug)
        {
        //     Debug.Log(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, _leftController) + "," + 
        //             OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, _rightController)); 
        }

              
    }
    
    public IEnumerator ShortBreakForPointingTask(float duration)
    {
        taskIsRunning = false;
        isPause = true;
        t0 = 3.0f;
        taskNum += 1;
        yield return new WaitForSeconds(duration);
        isPause = false;
        isStartTask = true;
    }

    void ComputeAngularError()
    {
        controllerToObjectVec = (targetList[taskNum].transform.position - OVRInput.GetLocalControllerPosition(_rightController));
        Vector3 a = Vector3.ProjectOnPlane(controllerToObjectVec, Vector3.up);
        Vector3 b = Vector3.ProjectOnPlane(rayPointer.transform.forward, Vector3.up);
        angularError = Vector3.Angle(a, b);
        if (isDebug)
        {
            Debug.DrawRay(OVRInput.GetLocalControllerPosition(_rightController), a, Color.red, 0.1f);
            Debug.DrawRay(OVRInput.GetLocalControllerPosition(_rightController), b*10, Color.green, 0.1f);
            // the ray is under ControllerRayInteractor>Visuals>ControllerRay>Cube
            Debug.DrawRay(OVRInput.GetLocalControllerPosition(_rightController), b*10, Color.blue, 0.1f);            
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
