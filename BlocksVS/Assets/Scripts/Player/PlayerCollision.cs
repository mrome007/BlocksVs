using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour 
{
    void Update()
    {
        transform.Translate(Vector3.zero);  //for the rigidbody to think the object is moving to detect collsion.
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
                //Debug.Log("Destroyed an enemy " + gameObject.name);
                Destroy(other.gameObject);
                EnemySpawner.NumEnemies--;
                break;
            default:
                break;
        }
    }
    
}
