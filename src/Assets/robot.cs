using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public Vector3 [] directions;
    public int dir = 0;
    Vector3 rotation;
    public Vector3 position;
    public Vector3 start;
    // Start is called before the first frame update
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
        
    }

    public void right()
    {
        rotation.z -= 90;
        this.transform.Rotate(new Vector3(0,0,-90), Space.Self);
        dir++;
        if (dir > 3)
            dir = 0;
        
    }

    public void left()
    {
        rotation.z += 90;
        this.transform.Rotate(new Vector3(0,0,90), Space.Self);
        dir--;
        if (dir < 0)
            dir = 3;

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
        while (dir != 0)
            right();
    }
}
