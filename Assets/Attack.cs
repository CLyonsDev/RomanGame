using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour {

    public GameObject dmgNum;
    public LayerMask lm;

    public bool debug = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && GetComponent<Movement>().canRun)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            GetComponent<Movement>().stamina -= 7f;

            if(Physics.Raycast(ray, out hit, 6f, lm))
            {
                if (hit.transform.root.tag == "Enemy")
                {
                    if (hit.transform.root.GetComponent<EnemyHealth>().isDead)
                        return;

                    //We have hit an enemy. Do fake damage for now.
                    int damage = Random.Range(10, 31);

                    if(debug)
                    {
                        GameObject num = (GameObject)Instantiate(dmgNum, hit.transform.root.GetChild(0).position, Quaternion.identity);
                        num.GetComponentInChildren<Text>().text = damage.ToString();
                    }


                    hit.transform.root.GetComponent<EnemyHealth>().TakeDamage(damage, this.transform.root.gameObject);
                }
            }
        }
	}
}
