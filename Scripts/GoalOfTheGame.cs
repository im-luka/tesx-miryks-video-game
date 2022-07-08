using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalOfTheGame : MonoBehaviour
{
    public GameObject[] allChests;
    public static GameObject[] allChestsCopy;
    public static int numberOfChestsInTheGame;

    private void Awake() {
        numberOfChestsInTheGame = allChests.Length;
        allChestsCopy = allChests;
    }
}
