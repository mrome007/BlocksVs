using UnityEngine;
using System.Collections;

public class PlayerGridSpins : MonoBehaviour 
{
	public float SpinSpeed;
	public static bool running = false;
	public void SpinGridsRight()
	{
		if(running)
			return;
		StartCoroutine ("SpinRight");
	}

	private IEnumerator SpinRight()
	{
		running = true;
		Vector3 ObjectRotation = gameObject.transform.rotation.eulerAngles;
		float rotz = ObjectRotation.z;
		if(rotz <= 0)
			rotz += 360f;
		float newz = rotz - 90.0f;

		while(rotz > newz)
		{
			rotz = Mathf.MoveTowards(rotz,newz,SpinSpeed*Time.deltaTime);
			ObjectRotation.z = rotz;
			gameObject.transform.eulerAngles = ObjectRotation;
			yield return 0;
		}
		running = false;
	}

	public void SpinGridsLeft()
	{
		if(running)
			return;
		StartCoroutine ("SpinLeft");
	}
	
	private IEnumerator SpinLeft()
	{
		running = true;
		Vector3 ObjectRotation = gameObject.transform.rotation.eulerAngles;
		float rotz = ObjectRotation.z;
		if(rotz <= 0)
			rotz += 360f;
		float newz = rotz + 90.0f;
		
		while(rotz < newz)
		{
			rotz = Mathf.MoveTowards(rotz,newz,SpinSpeed*Time.deltaTime);
			ObjectRotation.z = rotz;
			gameObject.transform.eulerAngles = ObjectRotation;
			yield return 0;
		}
		running = false;
	}
}
