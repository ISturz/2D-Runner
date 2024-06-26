using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHero : MonoBehaviour
{

    public GameObject heroCopy;
    public float cameraOrigin;

    // Start is called before the first frame update
    void Start()
    {
        cameraOrigin = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCopy.transform.position.x > cameraOrigin)
        {
            if (heroCopy.transform.position.x > transform.position.x)
            {
                transform.position = new Vector2(heroCopy.transform.position.x, -2.05f);
            }
        }
    }
}
