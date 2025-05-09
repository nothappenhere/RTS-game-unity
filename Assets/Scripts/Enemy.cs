using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    internal void ReceiveDamage(int damageToInflict)
    {
        health -= damageToInflict;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
