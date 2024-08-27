using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Stats_Scr : MonoBehaviour
{
    public static Player_Stats_Scr instance;


    //TODO: нужно определится с Base Damage и всяким таким или немного поменять систему

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static class Ship
    {
        public static int maxHp = 10;
        public static int curHp = 10;
        public static int armor = 0;
        public static float damageMultiplier
        {
            get { return damageMultiplier_; }
            set
            {
                damageMultiplier_ = value;

                Machinegun.bulletDamage = (int)(Machinegun.bulletDamage * damageMultiplier_);
                //Debug.Log(Machinegun.bulletDamage + " | " + damageMultiplier_);
            }

        }
        private static float damageMultiplier_ = 1f;
        public static float firerateMultiplier
        {
            get { return firerateMultiplier_; }
            set
            {
                firerateMultiplier_ = value;
                Machinegun.bulletSpawnDelay = Machinegun.bulletSpawnDelay * firerateMultiplier_;
                CircleBotsStats.bulletSpawnDelay = CircleBotsStats.bulletSpawnDelay * firerateMultiplier_;
            }
        }
        private static float firerateMultiplier_ = 1f;
    }

    public static class Machinegun
    {
        public static float bulletSpawnDelay = 0.5f;
        public static float bulletSpreadAngle = 5f;
        public static int bulletDamage = 5;
        public static float bulletScale = 1f;
    }
    public static class CircleBotsStats
    {
        public static float bulletSpawnDelay = 2f;
        public static int bulletDamage = 5;
        public static int botsCount = 3;
        public static float botsRotationSpeed = 1f;
        //public Vector3 circleCenter;
        //public bool allBotsIsFiring;
    }
}









