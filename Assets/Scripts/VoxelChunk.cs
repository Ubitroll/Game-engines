using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoxelChunk : MonoBehaviour {

    public VoxelGenerator voxelGenerator;
    public int[,,] terrainArray;
    public int chunksize = 16;

    // Delegate Signature
    public delegate void EventBlockChanged();

    // Event Instances for EventBlockChanged
    public static event EventBlockChanged OnEventBlockDestroyed;
    public static event EventBlockChanged OnEventBlockPlaced;

    // Use this for initialization
    void Start () {
        InitialiseMesh();
        // Get terrainarray from XML file
        terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, "AssessmentChunk1");
        // Draw the correct faces
        CreateTerrain();
        // Update mesh info
        voxelGenerator.UpdateMesh();
    }
	
    // Initialises the mesh
    void InitialiseMesh()
    {
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

        if (Input.GetKeyDown(KeyCode.F1))
        {
            // Get terrainarray from XML file
            terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, "AssessmentChunk1");
            // Draw the correct faces
            CreateTerrain();
            // Update mesh info
            voxelGenerator.UpdateMesh();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Get terrainarray from XML file
            terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, "AssessmentChunk2");
            // Draw the correct faces
            CreateTerrain();
            // Update mesh info
            voxelGenerator.UpdateMesh();
        }
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

    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x > 0 && index.x < terrainArray.GetLength(0)) && (index.y > 0 && index.y < terrainArray.GetLength(1)) && (index.z > 0 && index.z < terrainArray.GetLength(2)))
        {
            // Change the block to the required type
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;
            // Create the new mesh
            CreateTerrain();
            // Update the mesh data
            GetComponent<VoxelGenerator>().UpdateMesh();

            if (blockType == 0)
            {
                OnEventBlockDestroyed();
            }
            else
            {
                OnEventBlockPlaced();
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
