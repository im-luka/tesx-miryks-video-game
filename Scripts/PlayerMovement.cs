using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbodyPlayer;
    Animator animator;
    public Transform centerPoint;
    float horizontal, vertical;
    Vector3 playerMovement;
    bool isMoving;
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 1f;
    public AudioSource walkingAudio;
    public AudioSource runningAudio;

    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(isMoving) {
            if(!walkingAudio.isPlaying) {
                walkingAudio.Play();
            }
        } else {
            walkingAudio.Stop();
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isMoving = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);
        if(isMoving && !Player.isDead) {
            if(Input.GetKey(KeyCode.W)) {
                animator.SetBool("IsWalkingForward", true);
                transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;

                if(Input.GetKey(KeyCode.LeftShift) && !PlayerWeapon.isWeaponEquiped) {
                    if(walkingAudio.isPlaying) {
                        walkingAudio.Stop();
                    }
                    if(!runningAudio.isPlaying){
                        runningAudio.Play();
                    }
                    moveSpeed = 5.5f;
                    rotationSpeed = 3f;
                    animator.SetBool("IsRunning", true);
                    CheckRunningJump();
                } else if(Input.GetKeyUp(KeyCode.LeftShift) && !PlayerWeapon.isWeaponEquiped) {
                    moveSpeed = 1.5f;
                    rotationSpeed = 1f;
                    animator.SetBool("IsRunning", false);
                }
            } else if(Input.GetKey(KeyCode.S)) {
                animator.SetBool("IsWalkingBackward", true);
                transform.position += Camera.main.transform.forward * -1 * moveSpeed * Time.deltaTime;
            } else if(Input.GetKey(KeyCode.D)) {
                animator.SetBool("IsWalkingRight", true);
                transform.position += Camera.main.transform.right * moveSpeed * Time.deltaTime;
            } else if(Input.GetKey(KeyCode.A)) {
                animator.SetBool("IsWalkingLeft", true);
                transform.position += Camera.main.transform.right * -1 * moveSpeed * Time.deltaTime;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("CanTurn")) {
                transform.eulerAngles += new Vector3 (0, Mathf.DeltaAngle(transform.eulerAngles.y, centerPoint.eulerAngles.y) * Time.deltaTime * rotationSpeed, 0);
            }
	    } else {
            animator.SetBool("IsWalkingForward", false);
            animator.SetBool("IsWalkingBackward", false);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsRunning", false);
            moveSpeed = 1.5f;
            rotationSpeed = 1f;
        }

        if(moveSpeed == 1.5f) {
            CheckStandingJump();
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            runningAudio.Stop();
        }
    }

    void CheckStandingJump() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("StandingJump");
        }
    }

    void CheckRunningJump() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("RunningJump");
        }
    }
    
}
