using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearControl : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
