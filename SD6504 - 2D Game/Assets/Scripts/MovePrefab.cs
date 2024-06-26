using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePrefab : MonoBehaviour
{
    public GameObject prefabToMove;
    public GameObject currentPrefab;
    public float incrementX;

    public GameObject box;
    public GameObject fuelCan;
    public GameObject laser;
    public GameObject laserWallSwitch;

    private bool encounteredSwitch = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 temp = currentPrefab.transform.position;
            temp.x = temp.x + incrementX;
            prefabToMove.transform.position = temp;

            // Allows the switch and laser wall to always appear in the 2nd prefab the first time this trigger is called.
            if (encounteredSwitch == false)
            {
                encounteredSwitch = true;
            }
            else
            {
                bool active = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
                if (laserWallSwitch != null)
                {
                    laser.SetActive(active);
                    laserWallSwitch.SetActive(active);
                    box.SetActive(active);
                }
                else
                {
                    laser.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                    box.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                }
                fuelCan.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                box.GetComponent<PolygonCollider2D>().enabled = box.activeSelf;
            }
        }

    }
}
