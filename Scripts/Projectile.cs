using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PlayerManagement;


public class Projectile : MonoBehaviour
{
    public GameObject impactEffect;
    public int damageAmount = 3;
    public float radius = 3;
    private void onCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Player")
            {
               // PlayerManager.TakeDamage(damageAmount);
            }
        }
        Destroy(gameObject);
    }
}
