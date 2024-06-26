using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public HeroScript hs;
    public Sprite[] sprites;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hs.isPaused)
        {
            pauseScreen.SetActive(true);
            GetComponent<Image>().sprite = sprites[1];
        }
        else if (hs.gameOver)
        {
            gameOverScreen.SetActive(true);
            GetComponent<Image>().sprite = sprites[1];
        }
        else
        {
            pauseScreen.SetActive(false);
            GetComponent<Image>().sprite = sprites[0];
        }
    }
}
