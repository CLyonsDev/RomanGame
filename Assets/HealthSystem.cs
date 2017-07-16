using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {

    private PlayerStats stats;

    private Image hpBar;
	// Use this for initialization
	void Start () {
        stats = GetComponent<PlayerStats>();

        hpBar = stats.playerUI.transform.FindDeepChild("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, (float)stats.currentHealth / stats.maxHealth, 20.0f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(null, 25);
        }
	}

    public void TakeDamage(GameObject source, int amt)
    {
        stats.currentHealth -= amt;

        if(stats.currentHealth <= 0)
        {
            Die();
        }

        stats.SetCharacterSheetInfo();
    }

    private void Die()
    {
        Debug.Log("Ded");
        GetComponent<Movement>().enabled = false;
        GetComponentInChildren<MouseLook>().enabled = false;
        //Destroy(this.gameObject);
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
