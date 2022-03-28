using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform MirrorCam;
    public Transform PlayerCam;
    public GameObject mirrorObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Mirror"))
        {
            mirrorObject.SetActive(!mirrorObject.activeSelf);
        }

        CalculateRotation();
    }

    public void CalculateRotation()
    {
        Vector3 dir = (PlayerCam.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        rot.eulerAngles = transform.eulerAngles - rot.eulerAngles;

        MirrorCam.localRotation = rot;
        Quaternion temp = rot;
        temp.eulerAngles = new Vector3(MirrorCam.localRotation.x, MirrorCam.localRotation.y, 0f);
        MirrorCam.localRotation = temp;
    }
}
