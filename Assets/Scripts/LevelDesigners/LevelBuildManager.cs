using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuildManager : MonoBehaviour {

    [Header("Ground")]
    public GameObject groundBbject1;
    public GameObject groundObject2;
	public Vector3 groundStartPoint;

    public GameObject LevelParent;

    public int blocksInXAxis, blocksInYAxis;

    private List<GameObject> levelBlocks;

    [Header("-----------------------------------------------------------------------------------------------------------------------")]
    [Header("Walls")]

    public GameObject[] walls;
    public GameObject[] Trees;



    public void BuildGround()
    {
        for(int i=0; i<blocksInYAxis; i++)
		{
            for(int j=0; j<blocksInXAxis; j++)
			{
                Debug.Log("Bla bla 2");
                GameObject g = (Mathf.Abs(i-j) % 2 == 0 ) ? groundBbject1 : groundObject2;
                Vector3 pos = new Vector3(groundStartPoint.x + j, LevelParent.transform.position.y, groundStartPoint.z + i);

                GameObject block = Instantiate(g, pos, g.transform.rotation);
                block.transform.SetParent(LevelParent.transform);
                levelBlocks.Add(block);

			}
		}
    }

    public void ClearFloor()
    {
        for (int i = 0; i < levelBlocks.Count; i++)
        {
            GameObject b = levelBlocks[i];
            DestroyImmediate(b);


        }
        levelBlocks.Clear();
    }
}
