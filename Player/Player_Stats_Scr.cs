using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats_Scr : MonoBehaviour
{
    public static Player_Stats_Scr instance;

    public static ShipStats ship = new ShipStats(10, 0, 1f, 1f);

    public static MachineGunStats machineGun = new MachineGunStats(0.5f, 5f, 5, 1f);


    //TODO: нужно определится с Base Damage и всяким таким или немного поменять систему

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public struct ShipStats
    {
        public ShipStats(int _hp, int _armor, float _damageMultiplier, float _firerateMultiplier)
        {
            hp = _hp; 
            armor = _armor;
            damageMultiplier_ = _damageMultiplier;
            firerateMultiplier_ = _firerateMultiplier;
        }

        public int hp;
        public int armor;
        public float damageMultiplier
        {
            get { return damageMultiplier_; }
            set { damageMultiplier_ = value;

                machineGun.bulletDamage = (int)(machineGun.bulletDamage * damageMultiplier_);
                Debug.Log(machineGun.bulletDamage + " | " + damageMultiplier_);
            }

        }
        private float damageMultiplier_;
        public float firerateMultiplier
        {
            get { return firerateMultiplier_; }
            set {  firerateMultiplier_ = value;
                machineGun.bulletSpawnDelay = machineGun.bulletSpawnDelay * firerateMultiplier_;
            }
        }
        private float firerateMultiplier_;
    }

    public struct MachineGunStats
    {
        public MachineGunStats(float _bulletSpawnDelay, float _bulletSpreadAngle, int _bulletDamage, float _bulletScale)
        {
            bulletSpawnDelay = _bulletSpawnDelay;
            bulletSpreadAngle = _bulletSpreadAngle;
            bulletDamage = _bulletDamage;
            bulletScale = _bulletScale;
        }

        public float bulletSpawnDelay;
        public float bulletSpreadAngle;
        public int bulletDamage;
        public float bulletScale;
    }


}
