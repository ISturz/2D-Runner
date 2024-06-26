using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public AudioClip zap;
    public HeroScript hs;
    public GameObject lasers;
    public GameObject hero;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!hs.isInvincible)
                SoundManager.instance.PlaySound(zap);
            hs.TakeDamage(10);
        }
    }
}
