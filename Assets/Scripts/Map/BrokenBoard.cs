using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBoard : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
