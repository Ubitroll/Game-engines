using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelChunk : MonoBehaviour
{
    // delegate signatures
    public delegate void EventBlockChanged();
    public delegate void EventBlockChangedWithType(int blockType);
    public delegate void EventBlockChangedWithTypeAndPosition(int blockType, Vector3 blockPosition);

    // event instances for EventBlockChangedWithType
    public static event EventBlockChangedWithType OnEventBlockChanged;
    // event instances for EventBlockChangedWithPosition
    public static event EventBlockChangedWithTypeAndPosition OnEventBlockDestroyed;

    VoxelGenerator voxelGenerator;
    public int[,,] terrainArray;
    public int chunkSize = 16;

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
        terrainArray = new int[chunkSize, chunkSize, chunkSize];

        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();

        voxelGenerator.UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {

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

    // When game object is enabled
    void OnEnable()
    {
        PlayerScript.OnEventBlockSet += SetBlock;
    }
    // When game object is disabled
    void OnDisable()
    {
        PlayerScript.OnEventBlockSet -= SetBlock;
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

    void CreateTerrain()
    {
        // iterate horizontally on width
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            // iterate vertically
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                // iterate per voxel horizontally on depth
                for (int z = 0; z < terrainArray.GetLength(2);
                z++)
                {
                    // if this voxel is not empty
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;
                        // set texture name by value
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

                        // check if we need to draw the positive x face
                        if (x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive x face
                        if (x == terrainArray.GetLength(0) - 1 || terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }

                        // check if we need to draw the negative y face
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive y face
                        if (y == terrainArray.GetLength(1) - 1 || terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);
                        }

                        // check if we need to draw the negative z face
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        // check if we need to draw the positive z face
                        if (z == terrainArray.GetLength(2) - 1 || terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                    }
                }
            }
        }
    }

    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x >= 0 && index.x < terrainArray.GetLength(0)) && (index.y >= 0 && index.y < terrainArray.GetLength(1)) && (index.z >= 0 && index.z < terrainArray.GetLength(2)))
        {
            //Getting the data of what was already there
            int previousBlockType = terrainArray[(int)index.x, (int)index.y, (int)index.z];
            Debug.Log("Previous Block: " + previousBlockType + "\nNew Block: " + blockType);
            
            if (blockType == 0)
            {
                OnEventBlockDestroyed(previousBlockType, index);
            }
            else
            {
                //OnEventBlockPlaced(blockType);
            }

            // Changing the block to the required type
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;

            // Create the new mesh & Updating The Mesh Data
            CreateTerrain();
            voxelGenerator.UpdateMesh();

            OnEventBlockChanged(blockType);
        }
    }

    /*
    ====================================================================================================
    Handling File Saving, Loading & Clearing
    ====================================================================================================
    */
    public void LoadFile(string fileName)
    {
        if (XMLVoxelFileWriter.CheckIfFileExists(fileName))
        {
            Debug.Log("Loading Voxel Chunk from file");

            //Getting the terrainArray from the XML file
            terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(16, fileName);

            //Drawing the correct faces
            CreateTerrain();
            voxelGenerator.UpdateMesh();
        }
        else
        {
            Debug.Log("Error: There is no previously existing save file");
        }
    }

    public void SaveFile(string fileName)
    {
        Debug.Log("Saving Voxel Chunk to file");
        XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, fileName);
    }

    public void ClearFile()
    {
        Debug.Log("Clearing Voxel Chunk");

        terrainArray = new int[chunkSize, chunkSize, chunkSize];
        InitialiseTerrain();

        //Drawing the correct faces
        CreateTerrain();
        voxelGenerator.UpdateMesh();
    }
}
