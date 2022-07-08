using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : MonoBehaviour
{
    Animator animator;
    public GameObject book;
    public GameObject questPanel;
    public Text questText;
    public static bool didPlayerPickUpQuest;

    void Start()
    {
        animator = GetComponent<Animator>();

        book.SetActive(false);
        questPanel.SetActive(false);
        didPlayerPickUpQuest = false;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) {
            questPanel.SetActive(true);
            if(Player.isNPCQuestFinished) {
                questText.text = "Thank You! Thank you! Thank you thousand times!";
                animator.SetBool("IsDancing", true);
            } else {
                questText.text = "Hey Summoner! Can you help me find my lost Book please?";
                book.SetActive(true);
                didPlayerPickUpQuest = true;
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Player")) {
            questPanel.SetActive(false);
        }
    }
}
