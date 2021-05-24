using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static int hint;
    
    public static string sceneName;

    private void Update()
    {
        if (hint == 20)
        {
            hint += 1;
            ChangeScene(sceneName);
        }
    }
    
    
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));

    }

    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(level);
        
    }
}
