using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip hit;
    private Gun gs;
    private HeroScript hs;
    private Vector3 fireDir;

    public void Setup(Gun gs, Vector3 fireDir)
    {
        this.gs = gs;
        this.fireDir = fireDir;
        hs = GameObject.Find("Hero").GetComponent<HeroScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hs.isPaused || hs.gameOver) return;
        transform.position -= fireDir.normalized * 0.05f;
        if (!GetComponent<Renderer>().isVisible)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Object" || (col.gameObject.tag == "Player" && hs.isInvincible == false))
        {
            Destroy(this.gameObject); 
        }
        if (col.gameObject.tag == "Player")
        {
            if (!hs.isInvincible)
                SoundManager.instance.PlaySound(hit);
            hs.TakeDamage(20);
        }
    }
}
