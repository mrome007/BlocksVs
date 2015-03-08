using UnityEngine;
using System.Collections;

public class PlayerGridDimensions : MonoBehaviour 
{
    public Transform BottomCorner;
    public Transform TopCorner;
    public static float PlayerGridMinX;
    public static float PlayerGridMaxX;
    public static float PlayerGridMinY;
    public static float PlayerGridMaxY;
	// Use this for initialization
	void Start () 
    {
        PlayerGridMinX = BottomCorner.position.x;
        PlayerGridMaxX = TopCorner.position.x;
        PlayerGridMinY = BottomCorner.position.y;
        PlayerGridMaxY = TopCorner.position.y;
	}
	
	
}
