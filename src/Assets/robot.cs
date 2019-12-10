using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public Vector3[] directions;
    public int dir = 0;
    int startDir = 0;
    Vector3 rotation;
    public Vector3 position;
    public Vector3 start;
    // Start is called before the first frame update
    bool rotating = true;
    float startDeg;
    float targetDeg;
    float speed;
    float turned;
    public Light light;

    float litTime = 0;

    void Start()
    {
        directions = new Vector3[4];
        directions[0] = new Vector3(1, 0, 0);
        directions[1] = new Vector3(0, -1, 0);
        directions[2] = new Vector3(-1, 0, 0);
        directions[3] = new Vector3(0, 1, 0);

        rotation = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if(litTime>0)
        {
            litTime--;
            if(litTime==0)
            {
                light.GetComponent<Light>().enabled = false;
            }
        }
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

    public void right()
    {
        /*
        rotation.z -= 90;
        this.transform.Rotate(new Vector3(0,0,-90), Space.Self);*/
        rotating = true;
        startDeg = rotation.z;
        targetDeg = startDeg - 90;
        speed = -4.5f;
        turned = 0;
        dir++;
        if (dir > 3)
            dir = 0;

    }

    public void left()
    {
        /*rotation.z += 90;
        this.transform.Rotate(new Vector3(0, 0, 90), Space.Self);*/
        rotating = true;
        startDeg = rotation.z;
        targetDeg = startDeg + 90;
        speed = 4.5f;
        turned = 0;
        dir--;
        if (dir < 0)
            dir = 3;

    }

    public void lightRobot()
    {
        litTime = 30;
        light.GetComponent<Light>().enabled = true;

    }

    public void forward()
    {
        //this.transform.position = this.transform.position + directions[dir]*2;
        position += directions[dir];
    }

    public void back()
    {
        //this.transform.position = this.transform.position - directions[dir]*2;
        position -= directions[dir];
    }

    public void setPos(Vector3 pos)
    {
        this.transform.position = pos;

    }

    public void setStart(Vector3 pos)
    {
        this.transform.position = pos;
        start = pos;
    }

    public void setStartDir(int i)
    {
        startDir = i;
        reset();
    }
    public Vector3 getPos()
    {
        return this.transform.position;
    }

    public Vector3 getMapPosition()
    {
        Vector3 rval = new Vector3();
        rval.x = position.x;
        rval.y = position.y * -1;
        rval.z = 0;
        return rval;
    }

    public Vector3 getForward()
    {
        Vector3 rval = position + directions[dir];
        rval.y *= -1;
        return rval;
    }

    public Vector3 getBackward()
    {
        Vector3 rval = position - directions[dir];
        rval.y *= -1;
        return rval;
    }

    public Vector3 getRealForward()
    {
        return this.transform.position + directions[dir] * 2;
    }

    public Vector3 getRealBackward()
    {
        return this.transform.position - directions[dir] * 2;
    }

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
