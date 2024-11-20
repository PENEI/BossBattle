using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalWeapon : Weapon
{
    private Character player;
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (string.Equals(other.gameObject.tag, "Player"))
        {
            player = other.gameObject.GetComponent<Character>();
            player.hp -= character.power;
            Debug.Log("Hit");
        }
    }
}
