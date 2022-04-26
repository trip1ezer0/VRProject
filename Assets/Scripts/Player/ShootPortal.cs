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
        RaycastHit mirrorHit;

        // If gripping controller shine laser at target
        if (Input.GetAxis(laser) > 0f)
        {
            lineRenderer.enabled = true;
            // Raycast is touching a portal availabe surface
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == 7)
                {
                    Vector3 reflection = Vector3.Reflect(transform.TransformDirection(Vector3.forward).normalized, hit.normal.normalized);
                    if (Physics.Raycast(hit.point, reflection, out mirrorHit, Mathf.Infinity))
                    {
                        if (mirrorHit.collider.gameObject.layer == 6)
                        {
                            lineRenderer.material = greenLaser;
                            lineRenderer.SetPosition(0, hit.point);
                            lineRenderer.SetPosition(1, mirrorHit.point);
                        }
                        else
                        {
                            lineRenderer.material = redLaser;
                            lineRenderer.SetPosition(0, hit.point);
                            lineRenderer.SetPosition(1, mirrorHit.point);
                        }
                    }
                }
                else if (hit.collider.gameObject.layer == 6)
                {
                    lineRenderer.material = greenLaser;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    lineRenderer.material = redLaser;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                }
            }
            else
            {
                lineRenderer.material = redLaser;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
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

            if (hit.collider.tag == "Mirror")
            {
                Vector3 reflection = Vector3.Reflect(transform.TransformDirection(Vector3.forward), hit.normal);
                if (Physics.Raycast(hit.point, reflection, out mirrorHit, Mathf.Infinity))
                {
                    if (mirrorHit.collider.gameObject.layer == 6)
                    {
                        // Left controller index trigger
                        if (Input.GetAxis(trigger) > 0f && !cooldown)
                        {
                            //Debug.Log(hit.point);
                            if (mirrorHit.point.y % 10f < 1f)
                            {
                                point = new Vector3(mirrorHit.point.x, mirrorHit.point.y + 1f, mirrorHit.point.z);
                            }
                            else if (hit.point.y % 10f > 8f)
                            {
                                if (hit.point.y % 10f > 9f)
                                {
                                    point = new Vector3(mirrorHit.point.x, mirrorHit.point.y - 2f, mirrorHit.point.z);
                                }
                                else
                                {
                                    point = new Vector3(mirrorHit.point.x, mirrorHit.point.y - 1f, mirrorHit.point.z);
                                }
                            }
                            else
                            {
                                point = mirrorHit.point;
                            }

                            normal = mirrorHit.normal;
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

            else if (hit.collider.gameObject.layer == 6)
            {
                // Left controller index trigger
                if (Input.GetAxis(trigger) > 0f && !cooldown)
                {
                    //Debug.Log(hit.point);
                    /*if (hit.point.y % 10f < 1f)
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
                    }*/

                    point = new Vector3(hit.point.x, hit.transform.position.y, hit.point.z);

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
                    GetComponent<AudioSource>().Play();
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
