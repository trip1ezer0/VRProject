using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool left;

    private GameObject oppositePortal;

    // Start is called before the first frame update
    void Start()
    {
        if (left)
        {
            oppositePortal = GameObject.FindGameObjectWithTag("RightPortal");
            //Debug.Log("Leftportals opposite portal is: " + oppositePortal);
        }
        else
        {
            oppositePortal = GameObject.FindGameObjectWithTag("LeftPortal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oppositePortal == null)
        {
            if (left)
            {
                oppositePortal = GameObject.FindGameObjectWithTag("RightPortal");
                //Debug.Log("Leftportals opposite portal is: " + oppositePortal);
            }
            else
            {
                oppositePortal = GameObject.FindGameObjectWithTag("LeftPortal");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = oppositePortal.GetComponentInChildren<Transform>().transform.position;
            player.GetComponent<CharacterController>().enabled = true;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, player.transform.eulerAngles.z);
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, oppositePortal.transform.eulerAngles.y, player.transform.eulerAngles.z);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        GetComponent<BoxCollider>().enabled = false;
        oppositePortal.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider>().enabled = true;
        oppositePortal.GetComponent<BoxCollider>().enabled = true;
    }
}
