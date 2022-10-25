using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmassQueue : MonoBehaviour
{
    [SerializeField] LandmassElement[] prefabs;
    Queue<LandmassElement> conductingQueue = new Queue<LandmassElement>();

    int seed = 0;
    LandmassElement current;

    System.Random generator = new System.Random();

    private void Start()
    {
        SpawnFirst();
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while(true)
        {
            SpawnNext();
            yield return new WaitForSeconds(3);
        }
    }

    void SpawnFirst()
    {
        int index = generator.Next(seed);
        index %= prefabs.Length;
        LandmassElement newOne = prefabs[index];
        seed++;

        current = Instantiate(newOne, Vector3.zero, Quaternion.identity);
    }

    void SpawnNext()
    {
        Vector3 position = current.right.position;

        int index = generator.Next(seed);
        index %= prefabs.Length;
        LandmassElement newOne = prefabs[index];
        seed++;

        current = Instantiate(newOne, position, Quaternion.identity);
    }
}
