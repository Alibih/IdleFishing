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
    public float quality = 100, size = 100;
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
        switch (swimmingBehaviour)
        {
            case SwimmingBehaviour.Generic:
                transform.localPosition += new Vector3(maxSpeedX, 0,0);
                break;
            case SwimmingBehaviour.Jellyfish:
                currVelocity = Vector3.Lerp(currVelocity, goalVelocity, patternSpeed);
                transform.localPosition += currVelocity;
                if (System.Math.Abs(currVelocity.x - goalVelocity.x) < 0.000001)
                {
                    goalVelocity = new Vector3((goalVelocity.x == maxSpeedX) ? minSpeedX : maxSpeedX, 0, 0);
                }
                break;
            case SwimmingBehaviour.Jumpy:
                //TODO:
                break;
            case SwimmingBehaviour.Wavy:

                currVelocity = Vector3.Lerp(currVelocity, goalVelocity, patternSpeed);
                transform.localPosition += currVelocity;
                if (System.Math.Abs(currVelocity.y - goalVelocity.y) < 0.000001)
                {
                    print("22");
                    goalVelocity = new Vector3(maxSpeedX, goalVelocity.y * -1, 0);
                }
                break;
        }
    }
}