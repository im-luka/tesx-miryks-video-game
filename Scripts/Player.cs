using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;

    //Finished objectives
    public static int pickedChests;

    //Player Health Props
    public static bool isDead = false;
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerHealth healthBar;

    //Pausing the Game
    bool isGamePaused;
    public GameObject pausePanel;

    //Death Screen
    public CanvasGroup deathPanel;
    float deathTimer;
    public AudioSource losingSource;
    public AudioClip losingClip;
    bool isGameLost;

    //Winning the game
    public CanvasGroup winPanel;
    float winTimer;
    public AudioSource winningSource;
    public AudioClip winningClip;
    bool isGameWon;

    //Quest Canvas
    public RawImage[] questsInCanvas;
    public Texture completedQuestImage;

    //Woods
    public static bool isPlayerInTheWoods;

    //Light
    public Light gameLight;

    //NPC Quest
    public static bool isNPCQuestFinished;

    //Audio
    public AudioSource gameAmbient;
    public AudioSource woodsAmbient;
    public AudioSource pickUpSource;
    public AudioClip pickUpClip;

    void Start()
    {
        animator = GetComponent<Animator>();

        pickedChests = 0;
        isDead = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        isGamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        isPlayerInTheWoods = false;
        deathPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);

        isNPCQuestFinished = false;

        if(!gameAmbient.isPlaying) {
            gameAmbient.Play();
        }

        isGameWon = false;
        isGameLost = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!isGamePaused){
                isGamePaused = true;
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            } else {
                isGamePaused = false;
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }
        }

        if(pickedChests == GoalOfTheGame.numberOfChestsInTheGame) {
            winPanel.gameObject.SetActive(true);
            winTimer += Time.deltaTime;
            winPanel.alpha = winTimer / 1f;

            if(!isGameWon) {
                winningSource.PlayOneShot(winningClip);
                isGameWon = true;
            }
        }

        if(pickedChests > 0) {
            questsInCanvas[pickedChests - 1].texture = completedQuestImage;
        }
    }

    void FixedUpdate()
    {
        if(currentHealth <= 0) {
            PlayerDead();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Plane")) {
            isPlayerInTheWoods = true;
            gameLight.intensity = 0.1f;

            if(!woodsAmbient.isPlaying) {
                woodsAmbient.Play();
                gameAmbient.Stop();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Plane")) {
            isPlayerInTheWoods = false;
            gameLight.intensity = 1f;

            if(!gameAmbient.isPlaying) {
                gameAmbient.Play();
                woodsAmbient.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("HealthPotion")) {
            if(currentHealth < 100) {
                if(currentHealth <= 80) {
                    currentHealth += 20;
                    healthBar.SetHealth(currentHealth);
                    Destroy(other.gameObject);
                } else if(currentHealth > 80 && currentHealth < 100) {
                    currentHealth = 100;
                    healthBar.SetHealth(currentHealth);
                    Destroy(other.gameObject);
                }
                pickUpSource.PlayOneShot(pickUpClip);
            }
        }

        if(other.gameObject.CompareTag("Water")) {
            currentHealth = 0;
            PlayerDead();
        }

        if(other.gameObject.CompareTag("Book") && NPCQuest.didPlayerPickUpQuest) {
            pickUpSource.PlayOneShot(pickUpClip);
            isNPCQuestFinished = true;
            Destroy(other.gameObject);
        }
    }

    public void TakingDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void PlayerDead() {
        animator.SetBool("IsDead", true);
        isDead = true;
        Invoke(nameof(Dying), 3.5f);
    }

    void Dying() {
        if(!isGameLost) {
            losingSource.PlayOneShot(losingClip);
            isGameLost = true;
        }
        deathPanel.gameObject.SetActive(true);
        deathTimer += Time.deltaTime;
        deathPanel.alpha = deathTimer / 1f;
    }
}
