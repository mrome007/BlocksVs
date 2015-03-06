using UnityEngine;
using System.Collections;


//will work on this more later to reduce draw calls.
public class ActivityColor : MonoBehaviour 
{
    private MeshRenderer[] ObjectsMeshRenderers;
    private Material[] OriginalMaterial;
    void Start()
    {
        ObjectsMeshRenderers = new MeshRenderer[gameObject.transform.childCount];
        OriginalMaterial = new Material[gameObject.transform.childCount];
        int childCount = 0;
        foreach(Transform child in transform)
        {
            ObjectsMeshRenderers[childCount] = child.gameObject.GetComponent<MeshRenderer>();
            OriginalMaterial[childCount] = ObjectsMeshRenderers[childCount].material;
            childCount++;
        }
        InActiveColor();
    }

    public void InActiveColor()
    {
        for (int index = 0; index < gameObject.transform.childCount; index++)
        {
            ObjectsMeshRenderers[index].material.color = Color.black;
        }
    }

    public void ActiveColor()
    {
        for(int index = 0; index < gameObject.transform.childCount; index++)
        {
            ObjectsMeshRenderers[index].material.color = Color.white;
        }
    }


}
