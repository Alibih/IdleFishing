using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnHandler : MonoBehaviour
{
    public int MaxFishes = 5;
    public float spawnRate = 1;
    public float elapsedSpawnTime = 0;
    public List<Fish> fishes = new List<Fish>();



    public float spawnYOffset = 5, spawnXOffset = 5;
    Transform leftSpawnArea, rightSpawnArea;
    GameObject Pool;
    // Use this for initialization
    void Start()
    {
        leftSpawnArea = transform.Find("LeftPoint").GetComponent<Transform>();
        rightSpawnArea = transform.Find("RightPoint").GetComponent<Transform>();
        Pool = transform.Find("Pool").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedSpawnTime += Time.deltaTime;


        if(elapsedSpawnTime>= spawnRate)
        {
            Transform currTransform = Random.Range(0, 2) == 0 ? leftSpawnArea : rightSpawnArea;
            Vector3 pos  = new Vector3(currTransform.position.x + spawnXOffset * Random.Range(-1f, 1f),
                currTransform.position.y + spawnYOffset * Random.Range(-1f, 1f));
            elapsedSpawnTime = 0;
            
            Fish newFish = (Fish)Instantiate(fishes[Random.Range(0, fishes.Count)]);
            newFish.transform.position = pos;

            if (currTransform == rightSpawnArea)
            {
                newFish.maxSpeedX *= -1;
                newFish.minSpeedX *= -1;
                newFish.FlipX(true);
            }

            newFish.transform.parent = Pool.transform;
            print("instantiated " + newFish.name);
        }
    }
}
