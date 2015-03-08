using UnityEngine;
using System.Collections;

public class PlayerGridSpins : MonoBehaviour 
{
	public float SpinSpeed;
    public float UpAndDownSpeed;
	public static bool running = false;

	public void SpinGridsRight()
	{
		if(running)
			return;
		StartCoroutine ("SpinRight");
	}

    private IEnumerator GoUp()
    {
        float playerGridsZ = gameObject.transform.position.z;
        Vector3 targetPos = gameObject.transform.position;
        targetPos.z = playerGridsZ - 2.0f;
        float offset = 0.025f;
        while (playerGridsZ > targetPos.z + offset)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, UpAndDownSpeed * Time.deltaTime);
            playerGridsZ = gameObject.transform.position.z;
            yield return 0;
        }
        gameObject.transform.position = targetPos;
    }

    private IEnumerator GoDown()
    {
        float playerGridsZ = gameObject.transform.position.z;
        Vector3 targetPos = gameObject.transform.position;
        targetPos.z = playerGridsZ + 2.0f;
        float offset = 0.025f;
        while (playerGridsZ < targetPos.z - offset)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, UpAndDownSpeed * Time.deltaTime);
            playerGridsZ = gameObject.transform.position.z;
            yield return 0;
        }
        gameObject.transform.position = targetPos;
    }

	private IEnumerator SpinRight()
	{
        running = true;
        yield return StartCoroutine("GoUp");
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
        yield return StartCoroutine("GoDown");
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
        yield return StartCoroutine("GoUp");
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
        yield return StartCoroutine("GoDown");
		running = false;
	}
}
