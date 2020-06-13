using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    // stores position of player
    Transform tr_Player; 
    // stores rotation speed and move speed
    // mark it as public if you want to assign manually
    float f_RotSpeed = 3.0f, f_MoveSpeed = 3.0f;

    
    void Start()
    {
        // Make sure your 'Player' Game object is tagged as "Player" 
        // or you can delete this method and assign your player gameobject manually
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    
    void Update()
    {
        // Look towards Player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_Player.position - transform.position), 
        f_RotSpeed * Time.deltaTime);

        // Move towards Player
        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
    }
}