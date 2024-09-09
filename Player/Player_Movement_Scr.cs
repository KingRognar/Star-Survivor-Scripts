using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Scr : MonoBehaviour
{


    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
        transform.position = Vector3.Lerp(transform.position, new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0), 10 * Time.deltaTime);
    }
}
