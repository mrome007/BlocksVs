using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    public Transform AmmoSpawnPos;
    public Transform AmmoCastLeft;
    public Transform AmmoCastRight;
    public GameObject Ammo;
    public LayerMask Enemy;
    private float TimeToFire = 0.0f;

   void Start()
    {
        TimeToFire = 0.0f;
    }
    //I should probably refactor this in a class by itself.
    void Update()
    {
        Ray ray = new Ray(AmmoSpawnPos.position, transform.right);
        Ray rayLeft = new Ray(AmmoCastLeft.position, transform.right);
        Ray rayRight = new Ray(AmmoCastRight.position, transform.right);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 5.0f);
        if ((Physics.Raycast(ray, out hit, 5.0f, Enemy) || Physics.Raycast(rayLeft, out hit, 5.0f, Enemy) || 
            Physics.Raycast(rayRight, out hit, 5.0f, Enemy)) && TimeToFire <= 0.0f)
        {
            TimeToFire = 2.0f;
            Instantiate(Ammo, AmmoSpawnPos.position, AmmoSpawnPos.rotation);
            Debug.Log("Hit An Enemy");
        }
        TimeToFire -= Time.deltaTime;
    }
}
