using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismSpawner : MonoBehaviour
{
    public GameObject[] organismPrefabs; // Array to hold different organism prefabs
    public float spawnInterval = 5.0f; // Time interval between each spawn
    public int maxOrganisms = 10; // Maximum number of organisms that can be spawned at a time
    public float spawnRadius = 5.0f; // Radius around the spawner where organisms will be spawned
    public AudioClip spawnLoopAudio; // The audio clip to play in the background
    private AudioSource audioSource; // Audio source to play the audio


    private int currentOrganismCount = 0;

    void Start()
    {
        // Set up the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = spawnLoopAudio;
        audioSource.loop = true; 
        audioSource.playOnAwake = false; 
        audioSource.volume = 0.1f;
        // Start the audio loop
        if (spawnLoopAudio != null)
        {
            audioSource.Play();
        }
        // Start the spawning routine
        StartCoroutine(SpawnOrganismsRoutine());
    }

    private IEnumerator SpawnOrganismsRoutine()
    {
        // Infinite loop to keep spawning organisms at intervals
        while (true)
        {
            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Check if we are under the maxOrganisms limit
            if (currentOrganismCount < maxOrganisms)
            {
                SpawnOrganism();
            }
        }
    }

    private void SpawnOrganism()
    {
        // Choose a random organism prefab from the array
        int randomIndex = Random.Range(0, organismPrefabs.Length);
        GameObject organismPrefab = organismPrefabs[randomIndex];

        // Calculate a random position within the spawn radius
        Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        // Instantiate the chosen organism prefab at the random position
        GameObject newOrganism = Instantiate(organismPrefab, randomPosition, Quaternion.identity);
        
        // Optionally, keep track of the number of spawned organisms
        currentOrganismCount++;
        newOrganism.GetComponent<Organism>().OnDeath += () => currentOrganismCount--; // Decrement count when organism dies

        Debug.Log("Spawned organism: " + newOrganism.name);
    }

    // Optional: Visualize the spawn radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
