using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
    public AudioClip fuel;
    public HeroScript hs;
    public GameObject fuelCan;
    public GameObject hero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(fuel);
            hs.AddHealth();
            hs.IncreaseScore();
            fuelCan.SetActive(false);
        }
    }
}
