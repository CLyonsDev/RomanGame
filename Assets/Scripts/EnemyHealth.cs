using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    int maxHealth = 100;
    int currentHealth = 0;

    int xpForKill = 250;

    public bool isDead = false;

    public Rigidbody[] rbs;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        rbs = GetComponentsInChildren<Rigidbody>();
        xpForKill = Random.Range(10, 250);
	}

    public void TakeDamage(int amt, GameObject attacker)
    {
        currentHealth -= amt;

        if(currentHealth <= 0)
            Die(attacker);
    }

    public void Heal(int amt)
    {
        currentHealth += amt;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void Die(GameObject killer)
    {
        currentHealth = 0;
        isDead = true;
        killer.GetComponent<XPSystem>().GainXp(xpForKill);

        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            //rb.AddForce(Vector3.up * Random.Range(-0.015f, 0.015f));
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        isDead = true;
    }
}
