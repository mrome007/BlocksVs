using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	public GameObject [] Weapons;
	public Transform PlayerGrid;
	public LayerMask PlayerGridLayer;
	private int WhichWeapon;
	private Ray ray;
	void Start()
	{
		WhichWeapon = 0;
	}

	void Update()
	{
		ClickPlayerGrid ();
	}

	private void ClickPlayerGrid()
	{
		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		if(Input.GetMouseButtonUp(0))
		{
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100.0f, PlayerGridLayer))
			{
				Debug.Log ("hit a grid at " + hit.collider.gameObject.transform.position);
				Vector3 weaponPos = hit.collider.gameObject.transform.position;
				GameObject weap = (GameObject)Instantiate(Weapons[WhichWeapon],weaponPos,Quaternion.identity); //rotate on the z-axis
																				//vector3.right will be forware.
				weap.transform.parent = PlayerGrid;
			}
		}
	}

	public void PickWeapon(int x)
	{
		WhichWeapon = x;
		WhichWeapon = Mathf.Clamp (WhichWeapon, 0, Weapons.Length);
		Debug.Log ("Weapon Number: " + x);
	}

}
