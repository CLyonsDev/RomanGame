using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    //[SerializeField]
    //private int strength, vitality, dexterity, intelligence;
    [SerializeField]
    private int[] stats = new int[4]; //Str, Vit, Dex, Int
    private int[] modifiers; //Str, Vit, Dex, Int

    public int currentHealth;
    public int maxHealth;

    private int currentStamina;
    private int maxStamina;

    private int maxWeight;

    private int armorValue;

    public int minDamage;
    public int maxDamage;

    private int reputation;

    public int currentXP;
    public int currentLevel = 5;
    public int xpToNextLevel;

    private GameObject statScreen;
    public GameObject playerUI;

    private Text strText, vitText, agiText, intText;
    private Text strModText, vitModText, agiModText, intModText;
    private Text hpText, staminaText, xpText, dmgText, armorText, weightText;

    private Image hpBar;


	// Use this for initialization
	void Start () {
        statScreen = GameObject.FindGameObjectWithTag("Character Sheet");
        playerUI = GameObject.FindGameObjectWithTag("Player UI");

        currentXP = 0;

        GrabUIReferences();
        CalculateStats();
        SetCharacterSheetInfo();
    }

    public int ConvertStatToModifier(int value)
    {
        return Mathf.FloorToInt((value - 8) / 2.0f);
    }

    public void RandomizeStats()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = Random.Range(1, 21);
        }

        CalculateStats();
    }

    private void CalculateStats()
    {
        modifiers = new int[stats.Length];

        for (int i = 0; i < stats.Length; i++)
        {
            modifiers[i] = ConvertStatToModifier(stats[i]);
        }

        maxHealth = 100 + ((modifiers[0] * 10) + ((modifiers[1] / 2) * 10));
        maxStamina = 50 + ((modifiers[1] * 15) + (modifiers[2]) * 2);

        maxWeight = 100 + (modifiers[0] * 20) + (modifiers[1] * 10);

        armorValue = /*Armor Value (using 10 as a placeholder) ---> */ 10 + (modifiers[2] * 2) + modifiers[3];

        minDamage = 10 + (modifiers[0] * 2);
        maxDamage = 25 + (modifiers[0] * 2);

        maxStamina = Mathf.Clamp(maxStamina, 20, 10000);
        minDamage = Mathf.Clamp(minDamage, 1, int.MaxValue);
        maxDamage = Mathf.Clamp(maxDamage, 1, int.MaxValue);
        maxWeight = Mathf.Clamp(maxWeight, 10, int.MaxValue);

        ResetXP();
        RefillStats();
        SetCharacterSheetInfo();
    }

    private void RefillStats()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void ResetXP()
    {
        xpToNextLevel = (currentLevel + 1) * currentLevel * 15;
    }

    private void GrabUIReferences()
    {
        strModText = statScreen.transform.FindDeepChild("Strength").transform.Find("ModifierBG").GetComponentInChildren<Text>();
        vitModText = statScreen.transform.FindDeepChild("Vitality").transform.Find("ModifierBG").GetComponentInChildren<Text>();
        agiModText = statScreen.transform.FindDeepChild("Agility").transform.Find("ModifierBG").GetComponentInChildren<Text>();
        intModText = statScreen.transform.FindDeepChild("Intelligence").transform.Find("ModifierBG").GetComponentInChildren<Text>();

        strText = statScreen.transform.FindDeepChild("Strength").transform.Find("Attribute Value").GetComponentInChildren<Text>();
        vitText = statScreen.transform.FindDeepChild("Vitality").transform.Find("Attribute Value").GetComponentInChildren<Text>();
        agiText = statScreen.transform.FindDeepChild("Agility").transform.Find("Attribute Value").GetComponentInChildren<Text>();
        intText = statScreen.transform.FindDeepChild("Intelligence").transform.Find("Attribute Value").GetComponentInChildren<Text>();

        dmgText = statScreen.transform.FindDeepChild("Damage Text").GetComponent<Text>();
        armorText = statScreen.transform.FindDeepChild("Armor Text").GetComponent<Text>();
        hpText = statScreen.transform.FindDeepChild("Health Text").GetComponent<Text>();
        staminaText = statScreen.transform.FindDeepChild("Stamina Text").GetComponent<Text>();
        xpText = statScreen.transform.FindDeepChild("XP Text").GetComponent<Text>();
        weightText = statScreen.transform.FindDeepChild("Burden Text").GetComponent<Text>();
    }

    public void SetCharacterSheetInfo()
    {
        strText.text = stats[0].ToString();
        vitText.text = stats[1].ToString();
        agiText.text = stats[2].ToString();
        intText.text = stats[3].ToString();

        strModText.text = modifiers[0] > 0 ? "+" + modifiers[0].ToString() : modifiers[0].ToString();
        vitModText.text = modifiers[1] > 0 ? "+" + modifiers[1].ToString() : modifiers[1].ToString();
        agiModText.text = modifiers[2] > 0 ? "+" + modifiers[2].ToString() : modifiers[2].ToString();
        intModText.text = modifiers[3] > 0 ? "+" + modifiers[3].ToString() : modifiers[3].ToString();

        dmgText.text = minDamage.ToString() + "-" + maxDamage.ToString();
        hpText.text = currentHealth + "/" + maxHealth;
        staminaText.text = currentStamina + "/" + maxStamina;
        xpText.text = currentXP + "/" + xpToNextLevel;
        weightText.text = "60/" + maxWeight.ToString(); 

        armorText.text = armorValue.ToString();
    }
}
