﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelChunk : MonoBehaviour {

    public VoxelGenerator voxelGenerator;
    int[,,] terrainArray;
    int chunksize = 16;

	// Use this for initialization
	void Start () {
        voxelGenerator = GetComponent<VoxelGenerator>();

        // Instantiate the array with size based on chunksize
        terrainArray = new int[chunksize, chunksize, chunksize];

        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();

        voxelGenerator.UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitialiseTerrain()
    {
        // Itterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // Itterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // Itterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    // If we are operating on 4th layer
                    if (y == 3)
                    {
                        terrainArray[0, 3, 1] = 4;
                        terrainArray[0, 3, 2] = 4;
                        terrainArray[0, 3, 3] = 4;
                        terrainArray[1, 3, 3] = 4;
                        terrainArray[1, 3, 4] = 4;
                        terrainArray[2, 3, 4] = 4;
                        terrainArray[3, 3, 4] = 4;
                        terrainArray[4, 3, 4] = 4;
                        terrainArray[5, 3, 4] = 4;
                        terrainArray[5, 3, 3] = 4;
                        terrainArray[5, 3, 2] = 4;
                        terrainArray[6, 3, 2] = 4;
                        terrainArray[7, 3, 2] = 4;
                        terrainArray[8, 3, 2] = 4;
                        terrainArray[9, 3, 2] = 4;
                        terrainArray[10, 3, 2] = 4;
                        terrainArray[11, 3, 2] = 4;
                        terrainArray[12, 3, 2] = 4;
                        terrainArray[13, 3, 2] = 4;
                        terrainArray[13, 3, 3] = 4;
                        terrainArray[14, 3, 3] = 4;
                        terrainArray[15, 3, 3] = 4;
                        terrainArray[x, y, z] = 1;
                    }
                    // Else if the layer is below the fourth
                    else if (y < 3)
                    {
                        terrainArray[x, y, z] = 2;
                    }
                }
            }
        }
    }

    void CreateTerrain()
    {
        // Itterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // Itterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // Itterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    // If this voxel is not empty
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;
                        // Set texture name by value
                        switch (terrainArray[x, y, z])
                        {
                            case 1:
                                tex = "Grass";
                                break;
                            case 2:
                                tex = "Dirt";
                                break;
                            case 3:
                                tex = "Sand";
                                break;
                            case 4:
                                tex = "Stone";
                                break;
                            default:
                                tex = "Grass";
                                break;
                        }
                        // Check if we need to draw the negative x face
                        if(x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }
                        //Check if we need to draw positive x face
                        if (x == terrainArray.GetLength(0) - 1 || terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }
                        // Check if we need to draw the negative y face
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }
                        //Check if we need to draw positive y face
                        if (y == terrainArray.GetLength(1) - 1 || terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);
                        }
                        // Check if we need to draw the negative x face
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        //Check if we need to draw positive x face
                        if (z == terrainArray.GetLength(2) - 1 || terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                        print("Create " + tex + " Block");
                    }
                }
            }
        }
    }

    void CreateWaypoints()
    {

    }
}
