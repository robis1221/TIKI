using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventS : MonoBehaviour
{
    public int TotalNumberOfEvents;
    public UnityEvent[] unityEventArray;
    void Update()
    {
        
        for (int i = 0; i < TotalNumberOfEvents; i++)
        {
            switch (i)
            {
                case 0:
                    if (SceneManager.GetActiveScene().name == "MainScene")//&& (ObjectsToCollect.collectedObjects >= 5 || ObjectsToCollect.objects == 0))
                    {
                        unityEventArray[0].Invoke();
                    }
                    break;
                case 1:
                    if ((SceneManager.GetActiveScene().name=="Scene 4"|| SceneManager.GetActiveScene().name == "TheRoom" || SceneManager.GetActiveScene().name == "TheRoom1"||
                    SceneManager.GetActiveScene().name == "TheRoom2"|| SceneManager.GetActiveScene().name == "TheRoom3"|| SceneManager.GetActiveScene().name == "TheRoom4")
                    )//&& (ObjectsToCollect.collectedObjects >= 5|| ObjectsToCollect.objects == 0))
                    {
                        Debug.Log("yo u there");
                        unityEventArray[1].Invoke();
                    }
                    break;
                case 2:
                    //  if ((ObjectsToCollect.collectedObjects >= 5|| ObjectsToCollect.objects == 0)7)
                    // {
                    //      unityEventArray[2].Invoke();
                    // }
                    break;
                default:
                    break;
            }
           
        }
    }

        
}


