using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Intro.hint = 20;
        Intro.sceneName = sceneName;
    }
}
