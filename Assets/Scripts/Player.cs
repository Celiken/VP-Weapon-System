using CelikenVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;

    private void Awake()
    {
        playerStats.ClearStatsUpgrades();
    }
}
