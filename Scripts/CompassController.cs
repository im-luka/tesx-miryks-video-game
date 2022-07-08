using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoBehaviour
{
    public GameObject compassArrow;
    public static GameObject target;
    public GameObject player;
    public RectTransform compassNav;
    RectTransform rect;


    void Start()
    {
        rect = compassArrow.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(Player.pickedChests < GoalOfTheGame.allChestsCopy.Length) {
            target = GoalOfTheGame.allChestsCopy[Player.pickedChests];
        }

        Vector3[] compassEdges = new Vector3[4];
        compassNav.GetLocalCorners(compassEdges);
        float arrowScale = Vector3.Distance(compassEdges[1], compassEdges[2]);

        Vector3 direction = target.transform.position - player.transform.position;
        float angleToTarget = Vector3.SignedAngle(player.transform.forward, direction, player.transform.up);
        angleToTarget = Mathf.Clamp(angleToTarget, -90, 90) / 180f * arrowScale;
        rect.localPosition = new Vector3(angleToTarget, rect.localPosition.y, rect.localPosition.z);
    }
}
