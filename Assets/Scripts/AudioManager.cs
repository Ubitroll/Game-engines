using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Variables
    public AudioClip dirtDestroyed;
    public AudioClip dirtPlaced;
    public AudioClip stoneDestroyed;
    public AudioClip stonePlaced;
    public AudioClip grassDestoryed;
    public AudioClip grassPlaced;
    public AudioClip sandDestroyed;
    public AudioClip sandPlaced;

    VoxelGenerator voxelGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play sound when block destroyed
    void PlayDestoryBlockSound()
    {
        // Should check which texture it is
        //if (voxelGenerator.texNames.ToString() == "Dirt")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(dirtDestroyed);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Grass")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(grassDestoryed);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Stone")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(stoneDestroyed);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Sand")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(sandDestroyed);
        //}
        //else
        //{
            GetComponent<AudioSource>().PlayOneShot(dirtDestroyed);
        //}
    }

    // Play the place block sound
    void PlayPlaceBlockSound()
    {
        //if (voxelGenerator.texNames.ToString() == "Dirt" )
        //{
        //    GetComponent<AudioSource>().PlayOneShot(dirtPlaced);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Grass")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(grassPlaced);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Stone")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(stonePlaced);
        //}
        //else if (voxelGenerator.texNames.ToString() == "Sand")
        //{
        //    GetComponent<AudioSource>().PlayOneShot(sandPlaced);
        //}
        //else
        //{
            GetComponent<AudioSource>().PlayOneShot(dirtPlaced);
        //}
    }

    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += PlayDestoryBlockSound;
        VoxelChunk.OnEventBlockPlaced += PlayPlaceBlockSound;
    }

    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= PlayDestoryBlockSound;
        VoxelChunk.OnEventBlockPlaced -= PlayPlaceBlockSound;
    }
}
