using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnerScript : MonoBehaviour
{

    [System.Serializable]
    public struct Spawnable
    {
        public string type;

        
    }

    [System.Serializable]
    public struct SpawnSettings
    {
        public string type;
        public float minWait;
        public float maxWait;

        public int maxObjects;


    }

    public GameObject BonusGameobject;

    private bool spawningObject = false;

    public List<Spawnable> ennemySpawnables = new List<Spawnable>();

    public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();

    public static BonusSpawnerScript instance;

    [SerializeField]

    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private IEnumerator SpawnObject(string type, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.instance.SpawnBonusFromPool(type,new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y), Quaternion.identity);  // selon la taille des bordures de notre jeu
        spawningObject = false;
        GameController.EnnemyCount++;
    }
    void Update()
    {
        if (!spawningObject && GameController.BonusCount < spawnSettings[0].maxObjects)
        {
            spawningObject = true;
            
            int chosenIndex = 0;
            

            while ( chosenIndex < ennemySpawnables.Count - 1)
            {
                chosenIndex++;
                
            }
            
            StartCoroutine(SpawnObject(ennemySpawnables[chosenIndex].type, Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultiplier, spawnSettings[0].maxWait / GameController.DifficultyMultiplier)));
        }



    }
}
