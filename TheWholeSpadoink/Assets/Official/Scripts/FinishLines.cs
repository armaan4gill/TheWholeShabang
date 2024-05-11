using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Gill, Armaan
 * 05/10/2024
 * Actiavtes next scene when crossing finish line
 */
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
