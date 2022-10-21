using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character character;
    public Collider collider;


    protected virtual void Awake()
    {
        character = transform.GetComponentInParent<Character>();
        collider = transform.GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
