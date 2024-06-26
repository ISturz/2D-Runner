using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Objects
    public AudioClip shoot;
    public HeroScript hs;
    public GameObject hero;
    public GameObject gun;
    public Transform bulletObj;
    private Animator anim;
    private Vector3 target;

    // States
    public bool firing;
    private bool playerInRange;
    public bool readyToFire;
    private float gunAngle;
    private float fireTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        firing = false;
        playerInRange = false;
        readyToFire = true;
        fireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hs.isPaused || hs.gameOver) return;
        if (GetComponent<Renderer>().isVisible)
            playerInRange = true;
        else
            playerInRange = false;

        if (playerInRange)
        {
            float yDiff = gun.transform.position.y - hero.transform.position.y;
            float xDiff = gun.transform.position.x - hero.transform.position.x;
            gunAngle = (180 / Mathf.PI) * Mathf.Atan(yDiff / xDiff);
            gunAngle = xDiff < 0 ? 90 : gunAngle;
            
            gun.transform.rotation = Quaternion.Euler(0, 0, gunAngle);

            if (!hs.sceneEntered || gunAngle >= 90) return;

            if (GameObject.FindGameObjectsWithTag("Bullet").Length == 0)
            {
                if (Time.time > fireTime)
                {
                    fireTime = Time.time + 3;
                    anim.SetTrigger("Fire");
                }
            }
        }
    }

    public void CreateBullet()
    {
        
        Transform firePoint = transform.Find("FirePoint");
        Transform newBullet = Instantiate(bulletObj, firePoint.position, Quaternion.Euler(0, 0, gunAngle));
        Vector3 fireDir = firePoint.position - hero.transform.position;
        fireDir.x = fireDir.x < 0 ? 0 : fireDir.x;

        newBullet.GetComponent<Bullet>().Setup(this, fireDir);
        SoundManager.instance.PlaySound(shoot);
    }

}
