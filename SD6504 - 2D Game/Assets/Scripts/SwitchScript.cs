using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public AudioClip lasersDisabled;
    public Sprite offSprite;
    public Sprite onSprite;
    public GameObject laserWall;
    public GameObject laserWallSwitch;


    private bool switchOn = true;

    void Update()
    {
        if (GetComponent<Renderer>().isVisible == false && switchOn == false)
        {
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GetComponent<SpriteRenderer>().sprite == onSprite)
                SoundManager.instance.PlaySound(lasersDisabled);
            switchOn = false;
            GetComponent<SpriteRenderer>().sprite = offSprite;
            laserWall.SetActive(false);
        }
    }
}
