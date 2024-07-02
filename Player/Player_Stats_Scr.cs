using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats_Scr : MonoBehaviour
{
    public static Player_Stats_Scr instance;

    public static ShipStats ship = new ShipStats(10, 0);

    public static MachineGunStats machineGun = new MachineGunStats(0.5f, 5f);


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public struct ShipStats
    {
        public ShipStats(int _hp, int _armor)
        {
            hp = _hp; 
            armor = _armor;
        }

        public int hp;
        public int armor;
    }

    public struct MachineGunStats
    {
        public MachineGunStats(float _bulletSpawnDelay, float _bulletSpreadAngle)
        {
            bulletSpawnDelay = _bulletSpawnDelay;
            bulletSpreadAngle = _bulletSpreadAngle;
        }

        public float bulletSpawnDelay;
        public float bulletSpreadAngle;
    }


}
