using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level;

// Interaction between PlayerBody & LevelManager
public class PlayerLevel : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    // Stats for GUI
     private int lvl;

     private int XP;

     private int lvl_XP;

     void Awake() 
    {
        LoadStats();
    }

    private void LoadStats()
    {
        lvl = levelManager.lvl();
        XP = levelManager.XP();
        lvl_XP = levelManager.Lvl_XP();
    }

    // need to implement amount of XP earned
    public void KillEnemy()
    {
        int XP_earned = 30;

        levelManager.GainXP(XP_earned);

        XP += XP_earned;

        if (XP >= lvl_XP)
            LoadStats();

    }

    public void CollectItem()
    {
        int XP_earned = 2;

        levelManager.GainXP(XP_earned);

        XP += XP_earned;

        if (XP >= lvl_XP)
            LoadStats();

    }

}
