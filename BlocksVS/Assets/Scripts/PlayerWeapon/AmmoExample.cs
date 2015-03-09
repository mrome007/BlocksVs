using UnityEngine;
using System.Collections;

public class AmmoExample : MonoBehaviour 
{
    private float distanceTraveled = 0.0f;
	void Update () 
    {
        transform.Translate(Vector3.right * Time.deltaTime * 1.0f);
        distanceTraveled += Time.deltaTime;
        if(distanceTraveled > 5.0f)
        {
            Destroy(gameObject);
        }
	}
}
