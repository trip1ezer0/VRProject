using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y - 4f, Door.transform.position.z);
            Door.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
