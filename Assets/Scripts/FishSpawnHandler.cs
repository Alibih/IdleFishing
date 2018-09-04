﻿using System.Collections;
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
    GameObject Pond;
    // Use this for initialization
    void Start()
    {
        leftSpawnArea = transform.Find("LeftPoint").GetComponent<Transform>();
        rightSpawnArea = transform.Find("RightPoint").GetComponent<Transform>();
        Pond = transform.Find("Pond").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedSpawnTime += Time.deltaTime;


        if(elapsedSpawnTime>= spawnRate)
        {
            Transform currTransform = Random.Range(0, 2) == 0 ? leftSpawnArea : rightSpawnArea;

            elapsedSpawnTime = 0;
            Fish newFish;

            //Initiates fish if there's too little in the Pool
            if (Fish.Pool.Count < 10)
            {
                newFish = (Fish)Instantiate(fishes[Random.Range(0, fishes.Count)]);
                print("instantiated " + newFish.name);
            }
            else
            {
                int rand = Random.Range(0, Fish.Pool.Count);
                newFish = Fish.Pool[rand];
                newFish.enabled = true;
                Fish.Pool.Remove(newFish);
                print("resumed " + newFish.name);
            }


            Vector3 pos = new Vector3(currTransform.position.x + spawnXOffset * Random.Range(-1f, 1f),
                currTransform.position.y + Random.Range(newFish.minDepth,newFish.maxDepth));
            newFish.transform.position = pos;


            newFish.FlipX(currTransform == rightSpawnArea);

            newFish.transform.parent = Pond.transform;
        }
    }
}
