using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour
{

    public Material greenLaser;
    public Material redLaser;
    public GameObject portal;
    public string laser;
    public string trigger;

    private LineRenderer lineRenderer;
    private bool cooldown;
    private GameObject prev;
    private GameObject curr;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // If gripping controller shine laser at target
        if (Input.GetAxis(laser) > 0f)
        {
            lineRenderer.enabled = true;
            // Raycast is touching a portal availabe surface
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    lineRenderer.material = greenLaser;
                    lineRenderer.SetPosition(1, new Vector3(0f, 0f, hit.distance));
                }
                else
                {
                    lineRenderer.material = redLaser;
                    lineRenderer.SetPosition(1, new Vector3(0f, 0f, hit.distance));
                }
            }
            else
            {
                lineRenderer.material = redLaser;
                lineRenderer.SetPosition(1, new Vector3(0f, 0f, hit.distance));
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // If pointed at portal available surface, shoot portal
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            float angle;
            Quaternion rot;
            Vector3 normal;
            Vector3 point;

            if (hit.collider.gameObject.layer == 6)
            {
                // Left controller index trigger
                if (Input.GetAxis(trigger) > 0f && !cooldown)
                {
                    //Debug.Log(hit.point);
                    if (hit.point.y % 10f < 1f)
                    {
                        point = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
                    }
                    else if (hit.point.y % 10f > 8f)
                    {
                        if (hit.point.y % 10f > 9f)
                        {
                            point = new Vector3(hit.point.x, hit.point.y - 2f, hit.point.z);
                        }
                        else
                        {
                            point = new Vector3(hit.point.x, hit.point.y - 1f, hit.point.z);
                        }
                    }
                    else
                    {
                        point = hit.point;
                    }

                    normal = hit.normal;
                    angle = Mathf.Acos(Vector3.Dot(normal, portal.transform.forward) / (Vector3.Magnitude(normal) * Vector3.Magnitude(portal.transform.forward)));
                    angle *= Mathf.Rad2Deg;

                    if (normal.x < 0f)
                    {
                        angle = -angle;
                    }

                    rot = Quaternion.Euler(0f, angle, 0f);
                    curr = Instantiate(portal, point, rot);
                    Destroy(prev);
                    StartCoroutine(Cooldown());
                    prev = curr;
                }
            }
        }

    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(.5f);
        cooldown = false;
    }
}
