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

    

    private bool spawningObject = false;

    public List<Spawnable> bonusSpawnables = new List<Spawnable>();

    public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();

    public static BonusSpawnerScript instance;

    [SerializeField] private Collider2D spawningZoneCollider;
    [SerializeField] private Transform player;

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
        ObjectPooler.instance.SpawnBonusFromPool(type, (Vector2)player.position + new Vector2(
            Random.Range( - spawningZoneCollider.bounds.size.x/2, 
            spawningZoneCollider.bounds.size.x / 2),
            Random.Range(-spawningZoneCollider.bounds.size.y / 2,
            spawningZoneCollider.bounds.size.y / 2)), 
            Quaternion.identity);  // selon la taille des bordures de notre jeu
        spawningObject = false;
        GameController.BonusCount++;
        
        //if(GameController.BonusCount >= spawnSettings[0].maxObjects &&  )
        //{
        //    StartCoroutine(ReloadObject(5f));
        //}
    }

    private IEnumerator ReloadObject( float time)
    {
        yield return new WaitForSeconds(time);
        GameController.BonusCount = 0;
    }
    void Update()
    {
        if (!spawningObject && GameController.BonusCount < spawnSettings[0].maxObjects)
        {
            spawningObject = true;
            
            int chosenIndex = 0;
            

            while ( chosenIndex < bonusSpawnables.Count - 1)
            {
                chosenIndex++;
                
            }
            
            StartCoroutine(SpawnObject(bonusSpawnables[chosenIndex].type, Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultiplier, spawnSettings[0].maxWait / GameController.DifficultyMultiplier)));
        }
        Debug.Log("Bonus COunt + Max objects :" + GameController.BonusCount + " /  " + spawnSettings[0].maxObjects);


    }
}
