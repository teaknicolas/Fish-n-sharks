using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //<




    [System.Serializable]
    public struct Spawnable
    {
        public string type;

        public float weight;
    }

    [System.Serializable]
    public struct SpawnSettings
    {
        public string type;
        public float minWait;
        public float maxWait;

        public int maxObjects;


    }

    private float totalWeight;

    public GameObject Shark;

    public float cycleTimer = 3.0f;
    float currentTime;


    private bool spawningObject = false;

    

    public List<Spawnable> ennemySpawnables = new List<Spawnable>();

    public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();

    public static SpawnerScript instance;


    [SerializeField]

    private Camera mainCamera;

    [SerializeField]

    private GameObject spawnerPos;

    private void Awake()
    {
        instance = this;
        totalWeight = 0;
        foreach (Spawnable spawnable in ennemySpawnables)
            totalWeight += spawnable.weight;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = cycleTimer;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{

    //    if(currentTime > 0)
    //    {
    //        currentTime = currentTime -  Time.deltaTime;

    //    }
    //    else
    //    {
    //        Instantiate(Shark, this.transform);
    //        currentTime = cycleTimer;
    //    }
    //}

    private IEnumerator SpawnObject(string type, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.instance.SpawnFromPool(type, spawnerPos.transform.position, Quaternion.identity );  // selon la taille des bordures de notre jeu
        spawningObject = false;
        GameController.EnnemyCount++;
    }

    void Update()
    {
        if (!spawningObject && GameController.EnnemyCount < spawnSettings[0].maxObjects)
        {
            spawningObject = true;
            float pick = Random.value * totalWeight;
            int chosenIndex = 0;
            float cumulativeWeight = ennemySpawnables[0].weight; 

            while(pick > cumulativeWeight && chosenIndex < ennemySpawnables.Count - 1)
            {
                chosenIndex++;
                cumulativeWeight += ennemySpawnables[chosenIndex].weight;
            }

            StartCoroutine(SpawnObject(ennemySpawnables[chosenIndex].type, Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultiplier, spawnSettings[0].maxWait / GameController.DifficultyMultiplier)));
        }
    }
}
