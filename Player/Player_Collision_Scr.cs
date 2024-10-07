using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Collision_Scr : MonoBehaviour
{
    [SerializeField] private UI_HP_Bar_Scr UI_HP_Bar; 

    private void OnTriggerEnter2D(Collider2D collision) //TODO: сделать интерфейсы для получения урона?
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(2);
        if (collision.gameObject.CompareTag("Enemy_Proj"))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy_BaseProj_Scr>().projDamage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        GetComponent<Enemy_Flash_Scr>().StartFlash();
        
        Player_Stats_Scr.Ship.curHp -= damage;
        if (Player_Stats_Scr.Ship.curHp <= 0)
            Die();

        UI_HP_Bar.UpdateHPBar();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
