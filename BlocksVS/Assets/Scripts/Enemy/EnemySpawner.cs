using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{

    //eventually I will spawn enemies based off of patterns given in a .txt file.
    //bound 1-2; 2-3; 3-4; 1-4
    //just have to plan a smart way to keep adding to the event until the game ends.
    public Transform EnemyTarget;
    public GameObject[] Enemies;
    public Transform[] Bounds = new Transform[4];

    public delegate IEnumerator SpawnEnemyEventHandler(int num, float timeBetweenSpawns, Transform bounds1, Transform bounds2);
    public static event SpawnEnemyEventHandler SpawnEnemy;
        
    void Start()
    {
        SpawnEnemy += SpawnProtEnemy;
        SpawnEnemyEventHandler spawn = SpawnEnemy;
        if (SpawnEnemy != null)
            StartCoroutine(spawn(5,0.1f,Bounds[0],Bounds[1]));
        SpawnEnemy += SpawnProtEnemy;
        spawn = SpawnEnemy;
        if (SpawnEnemy != null)
            StartCoroutine(spawn(7, 0.5f, Bounds[1], Bounds[2]));
    }

    void Update()
    {
        
    }




    IEnumerator SpawnProtEnemy(int num, float timeBetweenSpawns, Transform bounds1, Transform bounds2)
    {
        int numEnemies = 0;
        while (numEnemies < num)
        {
            SpawnAnEnemy(Enemies[0], RandomPosFromRange(bounds1.position, bounds2.position));
            numEnemies++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnEnemy -= SpawnProtEnemy;
    }
	
    void SpawnAnEnemy(GameObject enemy, Vector3 pos)
    {
        GameObject em = (GameObject)Instantiate(Enemies[0], pos, Quaternion.identity);
        em.GetComponent<EnemyMovement>().EnemyTarget = EnemyTarget;
    }

    Vector3 RandomPosFromRange(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3(Random.Range(pos1.x, pos2.x), Random.Range(pos1.y,pos2.y), 0.0f);
    }

}
