using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    [Tooltip("The amount of damage dealing to the other object.")]
    public int damage = 1;

    [Tooltip("The amount of damage to deal to self on touching the other object.")]
    public int selfDamage = 5;

    public void HitObject(GameObject toHit)
    {
        var theirDamage = toHit.GetComponentInParent<DamageTaking>();

        if (theirDamage)
        {
            theirDamage.TakeDamage(damage);
        }

        var ourDamage = GetComponentInParent<DamageTaking>();

        if (ourDamage)
        {
            ourDamage.TakeDamage(selfDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HitObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        HitObject(other.gameObject);
    }
}
