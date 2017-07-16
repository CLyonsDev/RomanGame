using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour {

    public int level;
    public int currentXP;
    public int xpToNextLevel;

    private PlayerStats stats;

    public Image xpBar;
    public Text levelText;

	// Use this for initialization
	void Start () {
        stats = GetComponent<PlayerStats>();
        UpdateInfo();
    }

    // Update is called once per frame
    void Update () {
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, (float)stats.currentXP / stats.xpToNextLevel, 5.0f * Time.deltaTime);
        levelText.text = level.ToString();

        if(Input.GetKeyDown(KeyCode.I))
        {
            GainXp(25);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            stats.RandomizeStats();
        }
	}

    private void UpdateInfo()
    {
        level = stats.currentLevel;
        xpToNextLevel = stats.xpToNextLevel;
        currentXP = stats.currentXP;
        stats.ResetXP();
    }

    public void GainXp(int amt)
    {
        stats.currentXP += amt;

        if (stats.currentXP >= stats.xpToNextLevel)
            LevelUp();

        UpdateInfo();

        stats.SetCharacterSheetInfo();
    }

    public void LevelUp()
    {
        stats.currentLevel++;
        stats.currentXP -= stats.xpToNextLevel;
        if (stats.currentXP >= stats.xpToNextLevel)
            LevelUp();
        UpdateInfo();
    }
}
