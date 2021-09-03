using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float playerHP = 5f;

    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("ENEMY"))
        {
            Enemy enemy =  other.gameObject.GetComponent<Enemy>();
            playerHP -=  enemy.damage;
        }
    }
}
