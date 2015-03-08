using UnityEngine;
using System.Collections;

public class AmmoExample : MonoBehaviour 
{	
	void Update () 
    {
        transform.Translate(Vector3.right * Time.deltaTime * 1.0f);
	}
}
