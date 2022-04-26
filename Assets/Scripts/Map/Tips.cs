using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{

    public string TipText;
    public GameObject textObject;

    private void OnTriggerEnter(Collider other)
    {
        textObject.GetComponent<Text>().text = TipText;
        textObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        textObject.SetActive(false);
    }
}
