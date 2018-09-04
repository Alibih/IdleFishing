using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    private static List<Fish> Pool = new List<Fish>();
    public enum SwimmingBehaviour
    {
        Generic,
        Jellyfish,
        Jumpy,
        Wavy
    }

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private float quality = 1, size = 1;

    [SerializeField]
    private float maxDepth, minDepth;

    [SerializeField]
    private float maxSpeedX,maxSpeedY,minSpeedX,minSpeedY,patternSpeed;

    public SwimmingBehaviour swimmingBehaviour;
	private Vector3 position, goalVelocity,currVelocity;
	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
        position = transform.localPosition;
        transform.localScale *= size;

        switch (swimmingBehaviour)
        {
            case SwimmingBehaviour.Generic:
            case SwimmingBehaviour.Jellyfish:
                goalVelocity = new Vector3(maxSpeedX, 0, 0);
                currVelocity = goalVelocity;
                break;
            case SwimmingBehaviour.Jumpy:
                break;
            case SwimmingBehaviour.Wavy:
                goalVelocity = new Vector3(maxSpeedX, maxSpeedY, 0);
                currVelocity = goalVelocity;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        Swim();
	}

    //TODO: Make it a pool instead, reuse objects instead of destroy/initiate
    void OnBecameInvisible()
    {
        this.enabled = false;
        Pool.Add(this);
    }

    public bool FlipX(bool flippedToRight)
    {
        bool currFlip = GetComponent<SpriteRenderer>().flipX;
        bool flipped = currFlip != flippedToRight;
        GetComponent<SpriteRenderer>().flipX = flippedToRight;
        if(flippedToRight && maxSpeedX > 0)
        {
            maxSpeedX *= -1;
            minSpeedX *= -1;
        }
        else if (!flippedToRight && maxSpeedX < 0)
        {
            maxSpeedX *= -1;
            minSpeedX *= -1;
        }
        return flipped;
    }
    public static Fish TakeFishFromPool(int minAllowedFishes)
    {
        if (Pool.Count <= minAllowedFishes)
            return null;
        int rand = Random.Range(0, Fish.Pool.Count);
        Fish newFish = Fish.Pool[rand];
        newFish.enabled = true;
        Fish.Pool.RemoveAt(rand);
        return newFish;
    }
    public float GetAppropriateDepth()
    {
        return Random.Range(minDepth, maxDepth);
    }
    void Swim()
    {
        //Determines the way the fish swims
        float theta = (Time.timeSinceLevelLoad-spawnTime) * patternSpeed;
        switch (swimmingBehaviour)
        {
            case SwimmingBehaviour.Jumpy:
                //TODO:
                //Add if time exists
            case SwimmingBehaviour.Generic:
                currVelocity = new Vector3(maxSpeedX, 0,0);
                break;

            case SwimmingBehaviour.Jellyfish:

                currVelocity.x = minSpeedX + maxSpeedX * (Mathf.Sin(theta)+1)/2;

                /*
                currVelocity = Vector3.Lerp(currVelocity, goalVelocity, patternSpeed);
                transform.localPosition += currVelocity;
                if (System.Math.Abs(currVelocity.x - goalVelocity.x) < 0.000001)
                {
                    goalVelocity = new Vector3((goalVelocity.x == maxSpeedX) ? minSpeedX : maxSpeedX, 0, 0);
                }
                */
                break;



            case SwimmingBehaviour.Wavy:
                currVelocity.x = maxSpeedX;
                currVelocity.y = maxSpeedY * Mathf.Sin(theta);
                /*
                currVelocity = Vector3.Lerp(currVelocity, goalVelocity, patternSpeed);
                transform.localPosition += currVelocity;
                if (System.Math.Abs(currVelocity.y - goalVelocity.y) < 0.000001)
                {
                    goalVelocity = new Vector3(maxSpeedX, goalVelocity.y * -1, 0);
                }

                */
                break;
        }


        transform.position += currVelocity * Time.deltaTime;
    }
}