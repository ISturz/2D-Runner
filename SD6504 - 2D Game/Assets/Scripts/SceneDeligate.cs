using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneDeligate : MonoBehaviour
{
    public HeroScript hs;
    public void Restart()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
