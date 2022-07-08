using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Animator animator;
    [HideInInspector] public static bool isWeaponEquiped = false;
    public GameObject swordOnBack;
    public GameObject swordInHand;
    public static bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordInHand.SetActive(false);
        canAttack = true;
    }

    void Update()
    {
        SwordEquipping();
        AttackingEnemy();
    }

    void AttackingEnemy() {
        if(Input.GetKey(KeyCode.Mouse0)) {
            if(isWeaponEquiped && canAttack) {
                canAttack = false;
                int animationNum = Random.Range(1, 4);
                if(animationNum == 1) animator.SetTrigger("AttackLeft01");
                else if(animationNum == 2) animator.SetTrigger("AttackLeft02");
                else if(animationNum == 3) animator.SetTrigger("AttackLeft03");
                StartCoroutine(ResetCooldown(1f));
            }
        } 

        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            if(isWeaponEquiped && canAttack) {
                canAttack = false;
                int animationNum = Random.Range(1, 3);
                if(animationNum == 1) animator.SetTrigger("AttackRight01");
                else if(animationNum == 2) animator.SetTrigger("AttackRight02");
                StartCoroutine(ResetCooldown(3f));
            }
        } 
    }

    IEnumerator ResetCooldown(float attackCooldown) {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void SwordEquipping() {
        if(Input.GetKey(KeyCode.C)) {
            if(!isWeaponEquiped) {
                animator.SetTrigger("EquipSword");
                isWeaponEquiped = true;
            }
            Invoke(nameof(Equiping), 0.75f);
        }

        if(Input.GetKey(KeyCode.V)) {
            if(isWeaponEquiped) {
                animator.SetTrigger("UnequipSword");
                isWeaponEquiped = false;
            }
            Invoke(nameof(Unequiping), 1f);
        }
    }

    void Equiping() {
        swordOnBack.SetActive(false);
        swordInHand.SetActive(true);
    }

    void Unequiping() {
        swordOnBack.SetActive(true);
        swordInHand.SetActive(false);
    }
}
