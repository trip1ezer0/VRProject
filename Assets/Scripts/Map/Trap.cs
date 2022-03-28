using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(0f, 0f, 0f);
            other.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
