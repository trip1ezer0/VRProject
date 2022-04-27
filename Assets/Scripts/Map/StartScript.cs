using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private bool GameStarted;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<CharacterController>().enabled = false;
        GameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !GameStarted)
        {
            FindObjectOfType<PlayerMovement>().enabled = true;
            FindObjectOfType<CharacterController>().enabled = true;
            GameStarted = true;
        }
    }
}
