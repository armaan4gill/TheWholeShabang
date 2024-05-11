using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLines : MonoBehaviour
{
    public int sceneNum;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            // Load the next scene by name 
            SceneManager.LoadScene(6);
        }
    }
}
