using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    public enum SwimmingBehaviour
    {
        Generic,
        Jellyfish,
        Jumpy,
        Wavy
    }
    public float quality = 1, size = 1;
    public float maxSpeedX,maxSpeedY,minSpeedX,minSpeedY,patternSpeed;
    public SwimmingBehaviour swimmingBehaviour;
	private Vector3 position, goalVelocity,currVelocity;
	// Use this for initialization
	void Start () {
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

    void Swim()
    {
        //Determines the way the fish swims
        float theta = Time.timeSinceLevelLoad * patternSpeed;
        switch (swimmingBehaviour)
        {
            case SwimmingBehaviour.Jumpy:
                //TODO:
                //Add if time exists
            case SwimmingBehaviour.Generic:
                transform.localPosition += new Vector3(maxSpeedX, 0,0);
                break;

            case SwimmingBehaviour.Jellyfish:

                currVelocity.x = minSpeedX + maxSpeedX * (Mathf.Sin(theta)+1)/2;
                transform.position += currVelocity;

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
                
                currVelocity.y = maxSpeedY * Mathf.Sin(theta);
                transform.position += currVelocity;
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
    }
}