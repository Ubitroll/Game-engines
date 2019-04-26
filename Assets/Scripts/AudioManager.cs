using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Variables
    public AudioClip dirtDestroyed;
    public AudioClip dirtPlaced;
    
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
        GetComponent<AudioSource>().PlayOneShot(dirtDestroyed);
    }

    // Play the place block sound
    void PlayPlaceBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(dirtPlaced);
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
