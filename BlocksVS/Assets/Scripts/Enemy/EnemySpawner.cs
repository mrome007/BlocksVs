using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Text;
using System.IO;
public class EnemySpawner : MonoBehaviour 
{

    //eventually I will spawn enemies based off of patterns given in a .txt file.
    //bound 1-2; 2-3; 3-4; 1-4
    //just have to plan a smart way to keep adding to the event until the game ends.
    public Transform EnemyTarget;
    public GameObject[] Enemies;
    public Transform[] Bounds = new Transform[4];
    public static int NumEnemies = 0;
    private int CapWave = 10; //maximum number of waves for now is capped at 10.
    private int WaveNo;
    private const string LevelPath = "Levels/";
    //.txt format
    //first entry is always the number of waves
    //1. how many spawn functions to use. ex. 3
    //2. which enemies to spawn. ex. 0,1,3
    //3. num to spawn. ex. 10,5,3 delimiter is comma.


    public delegate IEnumerator SpawnEnemyEventHandler(int whichEnemy, int num, float timeBetweenSpawns, Transform bounds1, Transform bounds2);
    public static event SpawnEnemyEventHandler SpawnEnemy;
        
    void Start()
    {
        WaveNo = 0;
        NumEnemies = 0;
        ReadLevel(LevelPath + "TestLevel");
        /*
        SpawnEnemy += SpawnProtEnemy;
        SpawnEnemyEventHandler spawn = SpawnEnemy;
        if (SpawnEnemy != null)
            StartCoroutine(spawn(0,5,0.1f,Bounds[0],Bounds[1]));
         * */
    }

    IEnumerator SpawnProtEnemy(int whichEnemy,int num, float timeBetweenSpawns, Transform bounds1, Transform bounds2)
    {
        WaveNo++;
        int numEnemies = 0;
        Debug.Log(WaveNo);
        while (numEnemies < num)
        {
            SpawnAnEnemy(Enemies[whichEnemy], RandomPosFromRange(bounds1.position, bounds2.position));
            numEnemies++;
            NumEnemies++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        //wait for the wave to finish.
        while(NumEnemies > 0)
        {
            yield return 0;
        }
     
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnEnemy -= SpawnProtEnemy;

        yield return new WaitForSeconds(timeBetweenSpawns*2.0f);
        if(WaveNo <= CapWave)
        {
            WhichToSpawn(0);
            ContinueToSpawnEnemies(UnityEngine.Random.Range(5, 10), UnityEngine.Random.Range(0.1f, 1.0f), UnityEngine.Random.Range(0, 4));
        }
    }
	
    //will add more spawn functions to add.
    //I can use prot enemy to spawn a single type of enenmy.
    //for future use, I can create spawning functions that spawn
    //a combination of enemies.
    void WhichToSpawn(int enemyNum)
    {
        switch(enemyNum)
        {
            case 0:
                SpawnEnemy += SpawnProtEnemy;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    void ContinueToSpawnEnemies(int num, float timeBetween, int comingFrom)
    {
        SpawnEnemyEventHandler spawn = SpawnEnemy;
        if (SpawnEnemy == null)
            return;
        switch(comingFrom)
        {
            case 0: //left
                StartCoroutine(spawn(0, num, timeBetween, Bounds[0], Bounds[1]));
                break;

            case 1: //down
                StartCoroutine(spawn(0, num, timeBetween, Bounds[1], Bounds[2]));
                break;

            case 2: //right
                StartCoroutine(spawn(0, num, timeBetween, Bounds[2], Bounds[3]));
                break;

            case 3: //up
                StartCoroutine(spawn(0, num, timeBetween, Bounds[0], Bounds[3]));
                break;
            default:
                break;
        }
    }


    void SpawnAnEnemy(GameObject enemy, Vector3 pos)
    {
        GameObject em = (GameObject)Instantiate(Enemies[0], pos, Quaternion.identity);
        em.GetComponent<EnemyMovement>().EnemyTarget = EnemyTarget;
    }

    Vector3 RandomPosFromRange(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3(UnityEngine.Random.Range(pos1.x, pos2.x), UnityEngine.Random.Range(pos1.y,pos2.y), 0.0f);
    }

    bool ReadLevel(string filename)
    {
        try 
        {
            TextAsset readText = Resources.Load(filename) as TextAsset;
            string[] lines = readText.text.Split('\n');
            if(lines.Length <= 0)
            {
                return false;
            }
            CapWave = StringToInt(lines[0]);
            Debug.Log(CapWave);

            
            /*   char[] delimiter = {','};
                string[] lines1 = lines[2].Split(delimiter);
                for (int i = 0; i < lines1.Length; i++)
                {
                    Debug.Log(lines1[i].Length + " " + lines1[i]);
                }
             */
            StartCoroutine(SpawnFromText(lines));
            return true;
        }
        catch(Exception e)
        {
            print(e.Message);
            return false;
        }
    }


    IEnumerator SpawnFromText(string[] lines)
    {
        for (int index = 1; index < lines.Length; index += 3)
        {
            WaveNo++;
            yield return StartCoroutine(SpawnWave(lines[index],lines[index+1],lines[index+2]));
            yield return 0;
        }
        Debug.Log("END OF LEVEL");
    }

    IEnumerator SpawnWave(string numSpawns, string enemySpawn, string numPerSpawn)
    {
        int numberSpawn = StringToInt(numSpawns);
        int[] whichEnemies = new int[numberSpawn];
        int[] numberPerSpawn = new int[numberSpawn];
        string[] enemySpawnSplit = enemySpawn.Split(',');
        string[] numberSpawnSplit = numPerSpawn.Split(',');

        for (int index = 0; index < numberSpawn; index++)
        {
            whichEnemies[index] = StringToInt(enemySpawnSplit[index]);
            numberPerSpawn[index] = StringToInt(numberSpawnSplit[index]);
            Debug.Log(numberPerSpawn[index]);
        }

        for(int index = 0; index < numberSpawn; index++)
        {
            SpawnEnemy += SpawnTypeOfEnemy;
            ContinueSpawnEnemyWave(whichEnemies[index], numberPerSpawn[index],
                                   UnityEngine.Random.Range(0.1f, 2.0f), UnityEngine.Random.Range(0, 4));
            yield return 0;
        }

        while (NumEnemies > 0)
        {
            yield return 0;
        }
        yield return new WaitForSeconds(2.0f);
        Debug.Log("End of Wave " + WaveNo);
    }


    IEnumerator SpawnTypeOfEnemy(int whichEnemy, int num, float timeBetweenSpawns, Transform bounds1, Transform bounds2)
    {
        Debug.Log("Spawning " + WaveNo);
        int numEnemies = 0;
        while (numEnemies < num)
        {
            SpawnAnEnemy(Enemies[whichEnemy], RandomPosFromRange(bounds1.position, bounds2.position));
            numEnemies++;
            NumEnemies++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnEnemy -= SpawnTypeOfEnemy;
    }


    private int StringToInt(string num)
    {
        int y = 0;
        for(int index = 0; index < num.Length; index++)
        {
            if(num[index] == '\n' || num[index] == '\r' || num[index] == ' ')
                continue;
            y = y * 10 + (num[index] - '0');
        }
        return y;
    }

    void ContinueSpawnEnemyWave(int whichEnemy,int num, float timeBetween, int comingFrom)
    {
        SpawnEnemyEventHandler spawn = SpawnEnemy;
        if (SpawnEnemy == null)
            return;
        switch (comingFrom)
        {
            case 0: //left
                StartCoroutine(spawn(whichEnemy, num, timeBetween, Bounds[0], Bounds[1]));
                break;

            case 1: //down
                StartCoroutine(spawn(whichEnemy, num, timeBetween, Bounds[1], Bounds[2]));
                break;

            case 2: //right
                StartCoroutine(spawn(whichEnemy, num, timeBetween, Bounds[2], Bounds[3]));
                break;

            case 3: //up
                StartCoroutine(spawn(whichEnemy, num, timeBetween, Bounds[0], Bounds[3]));
                break;
            default:
                break;
        }
    }

}
