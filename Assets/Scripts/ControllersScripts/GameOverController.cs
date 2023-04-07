using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{

    public static GameOverController instance;

    // 0.3.1 Spawning des ennemis, on va annuler le script dans l'Objet ObjectPooler a chaque fois qu'il ya game Over et le réactiver quand on rejoue
    //public SpawnerScript spawnerScript;
   
    
    void Awake()
    {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        //spawnerScript.enabled = false;
    }

    public void Restart() // Game is restarting
    {
        //spawnerScript.enabled = true;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Shark")) //  0.3.1 Spawning des ennemis Juste besoin de désactiver les ennemis quand on restart
        {
            enemy.GetComponent<Shark>().Reset();
            enemy.SetActive(false);
        }
    }
    

}
