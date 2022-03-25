using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
     private int curHealth;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(curHealth);
        if(curHealth <= 0)
        {
            if(gameObject.tag == "Ennemy")
            {
                GameController.EnnemyCount--;
                curHealth = maxHealth;
            }
        }
    }

    public void UpdateHealth(int amt)
    {
        curHealth += amt;
    }

    public void ResetHealth()
    {
        curHealth = maxHealth;
    }
}
