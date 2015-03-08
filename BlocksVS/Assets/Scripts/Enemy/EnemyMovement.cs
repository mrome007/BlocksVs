using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    public Transform EnemyTarget;
    public float EnemySpeed;
    private bool withinEnemyTarget;

    void Start()
    {
        withinEnemyTarget = false;
    }
	
	void Update () 
    {
        if (EnemyTarget == null)
            return;
        Vector3 newPos = Vector3.MoveTowards(transform.position, EnemyTarget.position, EnemySpeed * Time.deltaTime);
        newPos.z = transform.position.z;
        transform.position = newPos;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        if(xPos > PlayerGridDimensions.PlayerGridMinX && xPos < PlayerGridDimensions.PlayerGridMaxX
            && yPos > PlayerGridDimensions.PlayerGridMinY && yPos < PlayerGridDimensions.PlayerGridMaxY && !withinEnemyTarget)
        {
            //Debug.Log("hello");
            withinEnemyTarget = true;
            transform.parent = EnemyTarget.parent;
        }
	}
}
