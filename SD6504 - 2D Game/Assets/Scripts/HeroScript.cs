using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HeroScript : MonoBehaviour
{
    // Speed/Force Parameters
    public float jumpForce;
    public float runSpeed;

    public AudioClip death;
    public AudioClip gameover;
    public AudioClip jump;
    public GameObject heroObj;
    public Joystick js;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //Distance Variables
    public float startPosition;

    // States
    public bool gameOver;
    private bool isDead;
    public bool isInvincible;
    public bool onCrate;
    private bool onGround;
    public bool isPaused;
    public bool sceneEntered;
    private bool usingJoystick;

    // Distance, Health, Lives & Score
    public Text distanceText;
    public Text healthText;
    public Text livesText;
    public Text scoreText;
    private float distance;
    public int health;
    public int lives;
    private float score;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        anim = GetComponent<Animator>();
        rb = heroObj.GetComponent<Rigidbody2D>();
        sr = heroObj.GetComponent<SpriteRenderer>();
        isDead = false;
        isInvincible = false;
        onGround = true;
        sceneEntered = false;
        usingJoystick = Application.platform == RuntimePlatform.Android ? true : false;
        GameObject.FindGameObjectWithTag("Joystick").GetComponent<Canvas>().enabled = usingJoystick;


        distanceText.text = $"{distance}m";
        healthText.text = $"{health}";
        livesText.text = $"{lives}";
        scoreText.text = $"{score}";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Convert.ToInt32(isPaused);
            isPaused = isPaused == true ? false : true;
        }

        if (EnterScene() && !isDead && !isPaused)
        {
            float movement = usingJoystick ? Math.Abs(js.Horizontal) * runSpeed : runSpeed;
            if (Input.GetKeyDown(KeyCode.UpArrow) || js.Vertical > 0.5f)
            {
                if (onGround == true)
                {
                    onGround = false;
                    // Prevents a bug where the player could double jump by clipping the edge of a box collider
                    if (rb.velocity.y < 0.1)
                    {
                        SoundManager.instance.PlaySound(jump);
                        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                        anim.SetTrigger("Jump");
                    }
                }
            }            

            if (Input.GetKey(KeyCode.RightArrow) || js.Horizontal > 0)
            {
                Vector3 position = heroObj.transform.position;
                position.x += movement;
                heroObj.transform.position = position;

                //Makes Hero look right when right arrow pressed
                Vector3 scale = heroObj.transform.localScale;
                scale.x = 1f;
                heroObj.transform.localScale = scale;

                float heroXPos = heroObj.transform.position.x;
                distance = heroXPos - startPosition > distance ? heroXPos - startPosition : distance;

                distanceText.text = $"{Math.Round(distance/5, 1)}m";

                anim.SetBool("isRunning", true);

            }
            else if (Input.GetKey(KeyCode.LeftArrow) || js.Horizontal < 0)
            {
                Vector3 position = heroObj.transform.position;
                position.x -= movement;
                heroObj.transform.position = position;

                //Makes Hero look left when left arrow pressed
                Vector3 scale = heroObj.transform.localScale;
                scale.x = -1f;
                heroObj.transform.localScale = scale;

                anim.SetBool("isRunning", true);
            }
            else
                anim.SetBool("isRunning", false);
        }
        score += Time.deltaTime;
        scoreText.text = $"{Math.Round(score)}";
    }

    private bool EnterScene()
    {
        if (sceneEntered == false)
        {
            Vector3 position = heroObj.transform.position;
            position.x += runSpeed / 2;
            heroObj.transform.position = position;
            anim.SetBool("isRunning", true);

            if (heroObj.transform.position.x >= startPosition)
                sceneEntered = true;
            
            return false;
        }
        else
            return true;        
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        onGround = true;
    }

    public void TakeDamage(int damageTaken)
    {
        if (isInvincible) return;
        health = health - damageTaken < 0 ? 0 : health - damageTaken;
        if (health == 0)
            LoseLife();
        else
            StartCoroutine(Invincible(1.2f));
        healthText.text = $"{health}";
        DecreaseScore();
    }

    public IEnumerator Invincible(float time)
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(1, 2, true);

        if (time == 1.2f)
        {
            sr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.3f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.9f);
        }
        else if (time == 6.0f)
        {
            SoundManager.instance.PlaySound(death);
            sr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.3f);
            sr.color = Color.white;
            yield return new WaitForSeconds(2.7f);
            anim.SetTrigger("Respawn");
            isDead = false;
            health = 100;
            healthText.text = $"{health}";
            for (int i = 0; i < 4; i++)
            {
                sr.color = new Color(1, 0, 0, 0.0f);
                yield return new WaitForSeconds(time / 20f);
                sr.color = Color.white;
                yield return new WaitForSeconds(time / 20f);
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().StopMusic();
            SoundManager.instance.PlaySound(gameover);
            gameOver = true;
            yield return new WaitForSeconds(2.0f);
            Time.timeScale = 0;
        }
        Physics2D.IgnoreLayerCollision(1, 2, false);
        isInvincible = false;
    }

    public void AddHealth()
    {
        health = health <= 95 ? health + 5 : 100;
        healthText.text = $"{health}";
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void DecreaseScore()
    {
        score = score - 1 > 0 ? score - 1 : 0;
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = $"{lives}";
        anim.SetTrigger("Dead");
        isDead = true;
        if (lives > 0)
            StartCoroutine(Invincible(6.0f));
        else
            StartCoroutine(Invincible(0));
    }
}
