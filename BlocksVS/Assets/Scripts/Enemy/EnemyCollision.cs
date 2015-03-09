using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour 
{

    void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Ammo":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
