using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
	public GameObject [] Weapons;
	public Transform PlayerGrid;
	public LayerMask PlayerGridLayer;
	private int whichWeapon;
	private Ray ray;
    private GameObject weap;
    private Vector3 clickDownPos;
    private Vector3 releasePos;
    private bool occupied;
    private bool hitPlayerGrid;


	void Start()
	{
        hitPlayerGrid = false;
		whichWeapon = 0;
        occupied = false;
        clickDownPos = new Vector3();
        releasePos = new Vector3();
	}

	void Update()
	{
		ClickPlayerGrid ();
	}

	private void ClickPlayerGrid()
	{
		if(Input.GetMouseButtonDown(0))
		{
            clickDownPos = Input.mousePosition;
            PlayerOnGridClick();
		}
		if(Input.GetMouseButtonUp(0) && !PlayerGridSpins.running && !occupied)
		{
            releasePos = Input.mousePosition;
            PlayerOnGridRelease(releasePos, clickDownPos, weap);
		}
	}

	public void PickWeapon(int x)
	{
		whichWeapon = x;
		whichWeapon = Mathf.Clamp (whichWeapon, 0, Weapons.Length);
		Debug.Log ("Weapon Number: " + x);
	}

    private void PlayerOnGridClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        hitPlayerGrid = false;
        if (Physics.Raycast(ray, out hit, 100.0f, PlayerGridLayer))
        {
            hitPlayerGrid = true;
            Vector3 weaponPos = hit.collider.gameObject.transform.position;
            PlayerGrid playerGrid = hit.collider.gameObject.GetComponent<PlayerGrid>();
            if (playerGrid.CurrentlyStationed != null)
            {
                occupied = true;
                return;
            }
            occupied = false;
            weap = (GameObject)Instantiate(Weapons[whichWeapon], weaponPos, Quaternion.identity); //rotate on the z-axis
            //vector3.right will be forward.
            playerGrid.CurrentlyStationed = weap;
            weap.SetActive(false);
            weap.transform.parent = PlayerGrid;
        }
    }

    private void PlayerOnGridRelease(Vector3 release, Vector3 click, GameObject spawnedWeapon)
    {
        if (!hitPlayerGrid)
            return;
        Quaternion weaponRot;
        float differenceX = release.x - click.x;
        float differenceY = release.y - click.y;
        if (Mathf.Abs(differenceX) < Mathf.Abs(differenceY)) //vertical point of view
        {
            if (differenceY < 0)
            {
                weaponRot = Quaternion.Euler(0.0f, 0.0f, 270.0f);
            }
            else
            {
                weaponRot = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            }
        }
        else //horizontal point of view
        {
            if (differenceX < 0)
            {
                weaponRot = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            }
            else
            {
                weaponRot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
        }
        spawnedWeapon.SetActive(true);
        spawnedWeapon.transform.rotation = weaponRot;
    }

}
