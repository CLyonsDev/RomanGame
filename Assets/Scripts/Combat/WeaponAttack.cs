using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WeaponAttack {

    public string attackName;
    public string triggerName;

    public int minDamage;
    public int maxDamage;
    public int critModifier;

    public float attackTimeout;
    public float damageDelay;

    public enum attackTypes
    {
        light,
        medium,
        heavy,
    }

    public enum damageTypes
    {
        bludgeoning,
        piercing,
        slashing
    };

    public attackTypes attackType;
    public damageTypes damageType;



	public WeaponAttack(int miDmg, int maDmg, int crMod, float atTime)
    {
        this.minDamage = miDmg;
        this.maxDamage = maDmg;
        this.critModifier = crMod;
        this.attackTimeout = atTime;
    }
}
