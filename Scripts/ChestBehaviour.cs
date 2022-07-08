using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBehaviour : MonoBehaviour
{
    Animator animator;
    public GameObject coin;
    public bool isChestPicked;
    bool isPlayerPickingChest;
    public Text text;

    private void Start() {
        animator = GetComponent<Animator>();
        isChestPicked = false;
        isPlayerPickingChest = false;

        text.enabled = false;
    }

    private void Update() {
        if((Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2.5) && !isPlayerPickingChest) {
            if(Input.GetKey(KeyCode.F) && gameObject == CompassController.target) {
                isChestPicked = true;
                isPlayerPickingChest = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("PickChest");
                animator.SetTrigger("OpenChest");
                Destroy(coin);
                Player.pickedChests++;
            } else if(Input.GetKey(KeyCode.F) && gameObject != CompassController.target) {
                text.enabled = true;
                text.text = "You can't pick that chest yet!";
                Invoke(nameof(RemoveText), 3.5f);
            }
        }
    }

    void RemoveText() {
        text.enabled = false;
    }
}
