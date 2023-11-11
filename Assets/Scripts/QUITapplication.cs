using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QUITapplication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Escaped");
        }
    }
}
