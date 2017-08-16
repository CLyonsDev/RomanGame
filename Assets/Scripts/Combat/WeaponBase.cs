using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour {

    public string weaponName;
    public Image weaponSprite;

    public WeaponAttack[] attacks;

    private List<WeaponAttack> lightAttacks;
    private List<WeaponAttack> mediumAttacks;
    private List<WeaponAttack> heavyAttacks;

    private bool canAttack = true;
    public LayerMask attackLayermask;

    private Animator anim;


	// Use this for initialization
	void Start () {

        lightAttacks = new List<WeaponAttack>();
        mediumAttacks = new List<WeaponAttack>();
        heavyAttacks = new List<WeaponAttack>();

        anim = GetComponentInChildren<Animator>();

        foreach (WeaponAttack attack in attacks)
        {
            if (attack.attackType == WeaponAttack.attackTypes.heavy)
                heavyAttacks.Add(attack);
            else if (attack.attackType == WeaponAttack.attackTypes.light)
                lightAttacks.Add(attack);
            else
                mediumAttacks.Add(attack);
        }

        Debug.LogWarning(string.Format("<color=orange>Attacks serialized</color>: {0} light, {1} medium, {2} heavy.", lightAttacks.Count, mediumAttacks.Count, heavyAttacks.Count));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            doAttack(0);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            doAttack(1);
        }
	}

    private void doAttack(int attkIndex)
    {
        if (!canAttack)
            return;

        WeaponAttack attk = null;
        switch (attkIndex)
        {
            default:
                Debug.LogError("Invalid attack index!");
                break;
            case 0:
                if (lightAttacks.Count > 0)
                {
                    Debug.Log("Light Attack");
                    attk = lightAttacks[Random.Range(0, lightAttacks.Count)];
                }
                else
                {
                    Debug.LogError("No light attacks assigned! Check the weapon.");
                }
                break;

            case 1:
                if (heavyAttacks.Count > 0)
                {
                    Debug.Log("Heavy Attack");
                    attk = heavyAttacks[Random.Range(0, heavyAttacks.Count)];
                }
                else
                {
                    Debug.LogError("No heavy attacks assigned! Check the weapon.");
                }
                break;
        }

        StartCoroutine(AttackCooldown(attk));

        if (attk == null)
        {
            Debug.LogError("Attack not specified. Routine will now exit.");
            return;
        }

        Debug.Log("Attacking with " + attk.attackName + ".");

        anim.SetTrigger(attk.triggerName);

        StartCoroutine(DelayedAttack(attk));
    }

    private IEnumerator DelayedAttack(WeaponAttack attk)
    {
        if (attk == null)
        {
            Debug.LogError("No attack specified. Cooldown will not be applied.");
            yield return null;
        }

        yield return new WaitForSeconds(attk.damageDelay);

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 7f, attackLayermask))
        {
            if (hit.transform.root.tag == "Enemy")
            {
                if (hit.transform.root.GetComponent<EnemyHealth>().isDead)
                    yield return null;

                //We have hit an enemy. Do damage.
                int damage = Random.Range(attk.minDamage, attk.maxDamage + 1);

                hit.transform.root.GetComponent<EnemyHealth>().TakeDamage(damage, this.transform.root.gameObject);

                Debug.Log("Attack hit. Target: " + hit.transform.root.name + " and dealt " + damage.ToString() + " damage.");
            }
        }
    }

    private IEnumerator AttackCooldown(WeaponAttack attack)
    {
        if(attack == null)
        {
            Debug.LogError("No attack specified. Cooldown will not be applied.");
            yield return null;
        }

        canAttack = false;
        yield return new WaitForSeconds(attack.attackTimeout);
        canAttack = true;
    }
}
