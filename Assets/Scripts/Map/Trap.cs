using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private Scene activeScene;

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.transform.position = new Vector3(0f, 8f, 0f);
            //other.transform.eulerAngles = new Vector3(0f, 0f, 0f);

            SceneManager.LoadScene(activeScene.name);
            //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0f, 0f, 0f);
        }
        else if (other.CompareTag("BrokenBoard"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
