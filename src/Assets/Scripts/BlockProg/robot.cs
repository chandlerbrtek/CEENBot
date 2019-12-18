using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class represents the robot on the field and tracks related positioning data
 */
public class robot : MonoBehaviour
{
    public Vector3[] directions;
    public int dir = 0;
    int startDir = 0;
    Vector3 rotation;
    public Vector3 position;
    public Vector3 start;


    bool rotating = true;
    float startDeg;
    float targetDeg;
    float speed;
    float turned;

    public Light spotlight;

    float litTime = 0;

    /**
     * Intializes some variables
     */
    void Start()
    {
        directions = new Vector3[4];
        directions[0] = new Vector3(1, 0, 0);
        directions[1] = new Vector3(0, -1, 0);
        directions[2] = new Vector3(-1, 0, 0);
        directions[3] = new Vector3(0, 1, 0);

        rotation = new Vector3(0, 0, 0);
        spotlight = GetComponentInChildren<Light>();

    }

    /**
     * Updates time sensitive robot funcitons related to rotation and the spotlight
     */
    void Update()
    {
        //if the light is on check to see if it should be turned off yet
        if(litTime>0)
        {
            litTime--;
            if(litTime==0)
            {
                spotlight.GetComponent<Light>().enabled = false;
            }
        }

        //if the robot is currently rotating, rotate and then check to see if we should stop
        if (rotating)
        {
            rotation.z += speed;
            this.transform.Rotate(new Vector3(0, 0, speed), Space.Self);
            turned += speed;
            if (Mathf.Abs(turned) == 90)
            {
                rotating = false;
            }
        }
    }

    /**
     * When called tracks the robots direciton and starts the clockwise rotation process 
     */
    public void right()
    {
        rotating = true;
        startDeg = rotation.z;
        targetDeg = startDeg - 90;
        speed = -4.5f;
        turned = 0;
        dir++;
        if (dir > 3)
            dir = 0;

    }

    /**
     * When called tracks the robots direciton and starts the counter clockwise rotation process 
     */
    public void left()
    {
        rotating = true;
        startDeg = rotation.z;
        targetDeg = startDeg + 90;
        speed = 4.5f;
        turned = 0;
        dir--;
        if (dir < 0)
            dir = 3;

    }

    /**
     * activates the light and sets it's timer
     */
    public void lightRobot()
    {
        litTime = 30;
        spotlight.GetComponent<Light>().enabled = true;

    }

    /**
     * Moves the robot forwardn it's map position
     * actual position movement logic is in Level Manager
     */
    public void forward()
    {
        position += directions[dir];
    }

    /**
     * Moves the robot backwards in it's map position
     * actual position movement logic is in Level Manager
     */
    public void back()
    {
        position -= directions[dir];
    }

    /**
     * Sets the robots actual position
     */
    public void setPos(Vector3 pos)
    {
        this.transform.position = pos;

    }

    /**
     * Sets the robots start position
     */
    public void setStart(Vector3 pos)
    {
        this.transform.position = pos;
        start = pos;
    }
    /**
     * Sets the direciton the robot will be facing on level start
     */
    public void setStartDir(int i)
    {
        startDir = i;
        reset();
    }

    /**
     * gets the robots actual position
     */
    public Vector3 getPos()
    {
        return this.transform.position;
    }

    /**
     * gets the robots coordinates on the map
     */
    public Vector3 getMapPosition()
    {
        Vector3 rval = new Vector3();
        rval.x = position.x;
        rval.y = position.y * -1;
        rval.z = 0;
        return rval;
    }

    /**
     * return the map position the robot would move to if it moved forwards
     */
    public Vector3 getForward()
    {
        Vector3 rval = position + directions[dir];
        rval.y *= -1;
        return rval;
    }

    /**
     * returns the map position the robot would move to if it moved backwards
     */
    public Vector3 getBackward()
    {
        Vector3 rval = position - directions[dir];
        rval.y *= -1;
        return rval;
    }

    /**
     * gets the actual positon the robot would be in if it moved foward
     */
    public Vector3 getRealForward()
    {
        return this.transform.position + directions[dir] * 2;
    }
    /**
     * gets the actual positon the robot would be in if it moved backwards
     */
    public Vector3 getRealBackward()
    {
        return this.transform.position - directions[dir] * 2;
    }

    /**
     * resets the robots map position, actual position, direction and rotation.
     */
    public void reset()
    {
        position = new Vector3(0, 0, 0);
        this.transform.position = start;
        while (dir != startDir)
        {
            rotation.z -= 90;
            this.transform.Rotate(new Vector3(0, 0, -90), Space.Self);
            dir++;
            if (dir > 3)
                dir = 0;

        }
    }
}
