using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalWeapon : Weapon
{
    private Character enemy;
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (string.Equals(other.gameObject.tag, "Enemy")) 
        {
            enemy = other.gameObject.GetComponent<Character>();
            enemy.hp -= character.power;
            Debug.Log("Hit");
        }
    }
}
