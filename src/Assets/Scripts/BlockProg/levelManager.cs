using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class manages the level, loads the level by duplicating tiles and putting them where they should be on the map
 * and executes specific moves and instructions while the game executes as directed by GameManager
 */
public class levelManager : MonoBehaviour
{
    //Level Data
    public static int level;

    public float[,] level0;
    public float[,] level1;
    public float[,] level2;
    public float[,] level3;
    public float[,] level4;
    public float[,] level5;
    public float[,] level6;
    public float[,] level7;
    public float[,] level8;
    public float[,] level9;
    public float[,] level10;

    public float[,] currLevel;
    float[,] path;
    int[] startDir;

    public int[] levelMoves;
    int moves = 0;

    int objectives;
    int completedObjectives;

    Vector3 offset;
    Vector3 startLocation;
    Vector3 endLocation;

    public int stars = 1;

    float rows = 5;
    float cols = 6;

    float xoffset = -6;
    float yoffset = 4;

    //tiles
    public GameObject start;
    public GameObject finish;
    public GameObject grass;
    public GameObject wall;
    public GameObject water;
    public GameObject mud;
    public GameObject boulder;
    public GameObject light;
    public GameObject note;

    //the robot
    public GameObject robot;

    
    public AudioClip levelcomplete;
    public AudioClip driving;
    public AudioClip splash;
    public AudioClip beep;
    AudioSource drive;
    AudioSource audioSource;


    // Start is called before the first frame update
    public List<GameObject> tiles;
    public List<GameObject> boulders;

    //movement stuff
    public bool isMoving;
    Vector3 startPos;
    Vector3 endPos;
    float speed;
    float traveled;
    bool boulderMoving;
    Vector3 boulderEndPos;
    Vector3 boulderEndMapPos;
    GameObject bo;
    public bool complete;
    bool justBeeped = false;

    /**
     * returns the current level
     */
    public int getLevel()
    {
        return level;
    }

    public void setVol(float f)
    {
        drive.volume = f;
    }
    /**
     * Initializes variables including a set of arrays representing the level data
     */
    void Start()
    {
        drive = GetComponent<AudioSource>();

        drive.loop = true;
        drive.clip = driving;
        drive.volume = 0.4f;
        audioSource = GetComponent<AudioSource>();

        speed = .06f;
        traveled = 0;
        isMoving = false;
        tiles = new List<GameObject>();
        boulders = new List<GameObject>();
        offset = new Vector3(-2.888f, 3.974f);

        startDir = new int[] { 0, 0, 3, 1, 0, 0, 1, 1, 2, 0, 3 };
        levelMoves = new int[] { 30,4,11,23,14,16,11,24,69,20,20 };
        currLevel = new float[(int)rows, (int)cols];

        level0 = new float[,] { { 1,1,1,1,1,1},
                                { 1,1,1,1,1,1},
                                { 1,1,1,1,1,1},
                                { 1,1,1,1,1,1},
                                { 1,1,1,1,1,1}
        };

        level1 = new float[,] { { 2,2,2,2,2,2},
                                { 3,3,3,3,2,2},
                                { 0,0,0,3,3,3},
                                { 0,8,0,0,0,9},
                                { 1,1,1,1,1,1}
        };

        level2 = new float[,] { { 9,3,2,2,2,2},
                                { 0,3,2,1,1,1},
                                { 0,0,3,0,0,0},
                                { 0,3,2,3,1,0},
                                { 0,3,2,3,1,8}
        };

        level3 = new float[,] { { 0,0,0,9,1,8},
                                { 0,2,2,2,3,0},
                                { 0,0,0,1,0,1},
                                { 1,1,0,1,0,1},
                                { 1,1,0,0,0,1}
        };

        level4 = new float[,] { { 0,9,2,2,2,0},
                                { 0,0,0,6,0,0},
                                { 1,1,1,1,0,0},
                                { 0,8,0,5,0,0},
                                { 4,4,4,4,4,4}
        };

        level5 = new float[,] { { 0,1,0,2,0,9},
                                { 0,1,0,2,0,0},
                                { 8,4,0,0,1,0},
                                { 0,1,0,2,0,0},
                                { 0,1,0,4,0,0}
        };

        level6 = new float[,] { { 0,8,0,2,2,0},
                                { 0,0,0,2,2,0},
                                { 0,4,0,2,0,9},
                                { 0,0,0,2,2,0},
                                { 0,0,0,2,2,0}
        };

        level7 = new float[,] { { 0,1,1,0,5,0},
                                { 0,1,1,0,0,1},
                                { 8,0,2,0,4,9},
                                { 4,4,2,0,0,1},
                                { 0,0,2,0,6,0}
        };

        level8 = new float[,] { { 0,0,0,0,0,0},
                                { 0,4,4,4,0,8},
                                { 4,0,2,1,0,0},
                                { 0,4,2,1,1,1},
                                { 0,0,2,2,0,9}
        };

        level9 = new float[,] { { 5,0,8,0,0,2},
                                { 1,1,1,1,0,2},
                                { 0,0,2,4,0,2},
                                { 0,1,2,1,1,2},
                                { 6,4,2,0,9,2}
        };

        level10 = new float[,] {    { 1,9,0,4,0,0},
                                    { 2,0,1,0,4,0},
                                    { 4,0,1,4,1,2},
                                    { 0,4,1,2,1,4},
                                    { 0,0,4,2,1,8}
        };


        
        //setLevel(0);


    }
    /**
     * generates a random level, then loads the specified level
     */
    public void setLevel(int i)
    {
        generateRandomLevel();
        level = i;
        resetLevel();
        GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().activeLevel.GetComponent<activeLevel>().setLevel(i);


    }
    /**
     * this function copies the level into the currentLevel array and then generates the level based on that.
     */
    public void resetLevel()
    {
        if (level == 0)
        {
            
            copyLevel(level0);
        }
        if (level==1)
        {
            copyLevel(level1);
        }
        if (level == 2)
        {
            copyLevel(level2);
        }
        if (level == 3)
        {
            copyLevel(level3);
        }
        if (level == 4)
        {
            copyLevel(level4);
        }
        if (level == 5)
        {
            copyLevel(level5);
        }
        if (level == 6)
        {
            copyLevel(level6);
        }
        if (level == 7)
        {
            copyLevel(level7);
        }
        if (level == 8)
        {
            copyLevel(level8);
        }
        if (level == 9)
        {
            copyLevel(level9);
        }
        if (level == 10)
        {
            copyLevel(level10);
        }

        generateLevel();


    }
    /**
     * This function copies a specified level into the provided array
     */
    public void copyLevel(float[,] arr)
    {

        currLevel = new float[(int)rows, (int)cols];
        for (int i = 0; i < rows; i++)
        {

            for (int j = 0; j < cols; j++)
            {
                currLevel[i,j] = arr[i,j];
            }
        }
            
    }
    /**
     * This function calls clear to clear the old level then
     * iterates through the current level array and generates the tiles
     */
    public void generateLevel()
    {
        clear();
        for (int i = 0; i < rows; i++)
        {
            
            for (int j = 0; j < cols; j++)
            {
                if (currLevel[i, j] == 0)
                {
                    GameObject go = Instantiate(grass);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 1)
                {
                    GameObject go = Instantiate(wall);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 2)
                {
                    GameObject go = Instantiate(water);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 3)
                {
                    GameObject go = Instantiate(mud);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                }
                if(currLevel[i, j] == 4)
                {//grass with boulder
                    GameObject go = Instantiate(grass);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                    GameObject b = Instantiate(boulder);
                    b.transform.position = offset + new Vector3(j * 2, i * -2);
                    boulders.Add(b);


                }
                if(currLevel[i, j] == 5)
                {//note tile
                    GameObject go = Instantiate(note);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                    objectives++;
                }
                if(currLevel[i, j] == 6)
                {//bulb tiles
                    GameObject go = Instantiate(light);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                    objectives++;
                }
                if (currLevel[i, j] == 8)
                {
                    startLocation = new Vector3(i, j);
                    GameObject go = Instantiate(start);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                    robot.GetComponent<robot>().setStart(go.transform.position);
                    robot.GetComponent<robot>().setStartDir(startDir[level]);
                    robot.GetComponent<robot>().position = new Vector3(j, i*-1, 0);
                }
                if (currLevel[i, j] == 9)
                {
                    endLocation = new Vector3(i, j);
                    GameObject go = Instantiate(finish);
                    tiles.Add(go);
                    go.transform.position = offset + new Vector3(j * 2, i * -2);
                 
                }

            }
        }
    }


    /**
     * instruct the robot robot to turn right
     */
    public void right()
    {
        robot.GetComponent<robot>().right();
    }
    /**
     * Instructs the robot to turn left
     */
    public void left()
    {
        robot.GetComponent<robot>().left();
    }
    /**
     * Has the robot activate it's light
     * If the robot was on a light objective it increases the objective count 
     * and turns the light tile into a grass tile
     */
    public void lightActivate()
    {
        Vector3 temp = robot.GetComponent<robot>().getMapPosition();
        robot.GetComponent<robot>().lightRobot();
        if (currLevel[(int)temp.y, (int)temp.x]==6)
        {
            completedObjectives++;
            
            currLevel[(int)boulderEndMapPos.y, (int)boulderEndMapPos.x] = 0;
            GameObject go = Instantiate(grass);
            tiles.Add(go);
            go.transform.position = robot.GetComponent<robot>().getPos();

        }
    }
    /**
     * The robot plays musical beeps
     *  If the robot was on a music objective it increases the objective count 
     * and turns the music tile into a grass tile
     */
    public void musicActivate()
    {
        Vector3 temp = robot.GetComponent<robot>().getMapPosition();
        audioSource.PlayOneShot(beep);
        justBeeped = true;
        if (currLevel[(int)temp.y, (int)temp.x] == 5)
        {
            completedObjectives++;
            currLevel[(int)boulderEndMapPos.y, (int)boulderEndMapPos.x] = 0;
            GameObject go = Instantiate(grass);
            tiles.Add(go);
            go.transform.position = robot.GetComponent<robot>().getPos();

        }
    }

    /**
     * This function checks if moving forward is legal for the robot (and any boulder it might be pushing)
     * and then initiates moving the robot in that direction
     */
    public void forward()
    {
        if (legalMove(robot.GetComponent<robot>().getForward())&&boulderMove(robot.GetComponent<robot>().getForward()))
        {
            robot.GetComponent<robot>().forward();
            startPos = robot.GetComponent<robot>().getPos();
            endPos = robot.GetComponent<robot>().getRealForward();
            isMoving = true;
        }
    }
    /**
     * This function checks if moving bakcward is legal for the robot (and any boulder it might be pushing)
     * and then initiates moving the robot in that direction
     */
    public void back()
    {
        if (legalMove(robot.GetComponent<robot>().getBackward()))
        {
            robot.GetComponent<robot>().back();
            startPos = robot.GetComponent<robot>().getPos();
            endPos = robot.GetComponent<robot>().getRealBackward();
            isMoving = true;
        }
    }

    /**
     * returns true if the move is within bounds and the destination tile is not bush or water
     */
    public bool legalMove(Vector3 pos)
    {
        
        if (pos.x >= 0 && pos.x < cols && pos.y>=0 && pos.y<rows)
        {
            if (currLevel[(int)pos.y,(int)pos.x] != 1 && currLevel[(int)pos.y, (int)pos.x]!=2)
            {
                return true;
            }
        }
        return false;
    }
    /**
     * Calculates and returns the number of stars earned
     * 1 for level completion
     * 1 for completing all objectives
     * 1 for copmleting the level in a minimal number of moves
     */
    public int getStars()
    {
        stars = 0;

        if(complete)
        {
            stars++;

        }
        if(completedObjectives>=objectives)
        {
            stars++;
        }
        if(moves<=levelMoves[level])
        {
            stars++;
        }
        return stars;
    }

    /**
     * returns true if it would be legal for a boulder to move there
     * checks to see if the spot is in bouns
     * the spot is illegal if the destination tile is a bush or contains a boulder
     */
    public bool legalBoulderMove(Vector3 pos)
    {

        if (pos.x >= 0 && pos.x < cols && pos.y >= 0 && pos.y < rows)
        {
            if (currLevel[(int)pos.y, (int)pos.x] != 1 && getBoulder(pos)==null)
            {
                
                return true;
            }
        }
        return false;
    }

    /**
    *resets the robot and the level
    */
    public void reset()
    {

        drive.Stop();

        complete = false;
        robot.GetComponent<robot>().reset();
        resetLevel();

    }
    /**
     * resets variables and deletes all tiles and boulders
     */
    public void clear()
    {
        moves = 0;
        objectives = 0;
        completedObjectives = 0;
        foreach (GameObject go in tiles)
        {
            Destroy(go);
        }
        tiles.Clear();
        foreach (GameObject go in boulders)
        {
            Destroy(go);
        }
        boulders.Clear();
    }
    /**
     * Returns true if there is no boulder in this spot or if the boulder would be able to move to the next square
     * If the boulder can move it initiates the boulder movement
     */
    public bool boulderMove(Vector3 pos)
    {
        bool rval=true;

        GameObject b = getBoulder(pos);
        if(b!=null)
        {
            
            Vector3 oneFurther = pos + pos - robot.GetComponent<robot>().getMapPosition();
            
            if (legalBoulderMove(oneFurther))
            {
                boulderEndMapPos = oneFurther;
                boulderEndPos  = pos - robot.GetComponent<robot>().getMapPosition();
                boulderEndPos *=2;
                boulderEndPos.y *= -1;
                boulderEndPos += b.transform.position;

                boulderMoving = true;
                bo = b;
            }
            else
            {
                rval = false;
            }

        }

        return rval;
    }

    /**
     * returns the map position of a game object
     */
    public Vector3 getPos(GameObject go)
    {
        Vector3 rval = new Vector3();
        Vector3 temp = go.transform.position - offset;
        rval.x = temp.x / 2;
        rval.y = temp.y / -2;
        return rval;
    }

    /**
     * Increments the move counter, used for star calculation
     */
    public void addMove()
    {
        moves++;
    }

    /**
     * if there is a boulder at this position, return a refrence to it
     */
    public GameObject getBoulder(Vector3 pos)
    {

        GameObject rval = null;
        foreach(GameObject go in boulders)
        {
            if((getPos(go)-pos).magnitude<0.1)
            {
                rval = go;
            }
        }
        return rval;
    }

    /**
     * Begin playing the driving noise for Robot movement
     */
    public void startDriving()
    {
        if (!drive.isPlaying || justBeeped)
        {
            justBeeped = false;
            drive.Play();
        }
    }
    /**
     * Stop playing the drive noise
     */
    public void stopDriving()
    {
        drive.Stop();
    }

    /**
     * On update if the robot or boulder are moving move them toward their destination
     * If they reach they're destination then they stop moving
     */
    private void Update()
    {
        if(isMoving)
        {
            Vector3 temp = robot.GetComponent<robot>().getPos() + ((endPos - startPos) * speed);
            if (boulderMoving)
            { 
                bo.transform.position += ((endPos - startPos) * speed);
            }
            robot.GetComponent<robot>().setPos(temp);
            traveled += speed;
            if(traveled>=1)
            {
                traveled = 0;
                robot.GetComponent<robot>().setPos(endPos);
                if (boulderMoving)
                {
                    bo.transform.position = boulderEndPos;
                    if (currLevel[(int)boulderEndMapPos.y, (int)boulderEndMapPos.x] == 2)
                    {//spot is water

                        currLevel[(int)boulderEndMapPos.y, (int)boulderEndMapPos.x] = 3;
                        GameObject go = Instantiate(mud);
                        //go.layer = 8;
                        go.GetComponent<SpriteRenderer>().rendererPriority = 8;
                        tiles.Add(go);
                        go.transform.position = bo.transform.position;
                        boulders.Remove(bo);
                        Destroy(bo);
                        audioSource.volume = 1;
                        audioSource.PlayOneShot(splash);
                    }
                }
                isMoving = false;
                
                boulderMoving = false;
                temp = robot.GetComponent<robot>().getMapPosition();
                if(currLevel[(int)temp.y,(int)temp.x]==9)
                {
                    complete = true;
                    drive.Stop();
                    audioSource.PlayOneShot(levelcomplete);
                }
            }
        }
    }

    /**
     * Generates a random level and puts it into slot 0 on the level array
     */
    public void generateRandomLevel()
    {
        int startx=0, starty=0;
        int endx=0, endy=0;
        //The path array is used for calculating a path from the start to the finish
        path = new float[,]         {   { 0,0,0,0,0,0},
                                        { 0,0,0,0,0,0},
                                        { 0,0,0,0,0,0},
                                        { 0,0,0,0,0,0},
                                        { 0,0,0,0,0,0}
        };

        //generate impassable terrain
        int water = Random.Range(0, 6);
        if (water ==1 || water ==2)
            water=1;
        if (water == 3 || water == 4)
            water = 2;
        if (water == 5)
            water = 3;

         //generates 0 to 3 water features
        while(water>0)
        {
            water--;
            if(Random.Range(0,2)==0)
            {//genenrate river
                if (Random.Range(0, 2)==0)
                {
                    int i = (int)Random.Range(0, rows);
                    for(int j=0; j<cols;j++)
                    {
                        level0[i, j] = 2;
                    }
                }
                else
                {
                    int j = (int)Random.Range(0, cols);
                    for (int i = 0; i < rows; i++)
                    {
                        level0[i, j] = 2;
                    }

                }

            }
            else
            {//generate pond
                int size = Random.Range(1, 4);
                int px = (int)Random.Range(0, rows-size);
                int py = (int)Random.Range(0, cols-size);
                for(int i = px; i<px+size; i++)
                    for(int j = py; j<py+size; j++)
                    {
                        level0[i, j] = 2;
                    }
            }
        }

        if(Random.Range(0,2)==0)
        {//flip water and trees half of the time
            for(int i =0; i<rows;++i)
            {
                for(int j=0; j<cols; ++j)
                {
                    if (level0[i, j] == 1)
                    {
                        level0[i, j] = 2;
                    }
                    else
                    {
                        level0[i, j] = 1;
                    }
                }
            }
        }


        //pick a start and end
        //start and end are generated on opposite walls of the level
        int wall = Random.Range(0, 4);
        if(wall==0)
        {
            startDir[0] = 1;
            startx = 0;
            starty = (int)Random.Range(0, cols);
            endx = (int)rows - 1;
            endy = (int)Random.Range(0, cols);
        }
        if (wall == 1)
        {

            startDir[0] = 3;
            startx = (int)rows - 1;
            starty = (int)Random.Range(0, cols);
            endx = 0;
            endy = (int)Random.Range(0, cols);
        }
        if (wall == 2)
        {
            startDir[0] = 0;
            startx = (int)Random.Range(0, rows);
            starty = 0;
            endx = (int)Random.Range(0, rows);
            endy = (int)cols - 1;
        }
        if (wall == 3)
        {
            startDir[0] = 2;
            startx = (int)Random.Range(0, rows);
            starty = (int)cols - 1;
            endx = (int)Random.Range(0, rows);
            endy = 0;
        }
        level0[startx,starty] = 8;
        level0[endx, endy] = 9;

        path[endx, endy] = 9;

 

        //call recursive pathing function to generate a path from start to finish
        pathToFinish(startx, starty);

        
        //now that the path has been decided identify other areas that could
        //have paths in them without breaking up the main path
        //randomly starts at a random point on the map and then starts over from the beggining
        for (int i = Random.Range(0,(int)rows); i < rows; ++i)
        {
            for (int j = Random.Range(0, (int)cols); j < cols; ++j)
            {
                if(legalPathPlacement(i,j))
                {
                    if(path[i,j]!=2)
                    path[i, j] = 1;
                }
            }
        }
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                if (legalPathPlacement(i, j))
                {
                    if (path[i, j] != 2)
                        path[i, j] = 1;
                }
            }
        }

        //pick number of objectives 0-2
        path[endx, endy] = 9;
        int numObjectives = Random.Range(0, 4);

        //for each objective being generated pick a spot identified as a potential path in the previous loops
        // put an objective there and then generate a path to the closest point on the main path
        for(int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                if(path[i,j]==1)
                {
                    int adjCount = 0;
                    if(inBounds(i+1,j))
                    {
                        if (path[i+1,j] != 0)
                            adjCount++;
                        if (path[i + 1, j] == 9)
                            adjCount=2;

                    }
                    if (inBounds(i - 1, j))
                    {
                        if (path[i - 1, j] != 0)
                            adjCount++;
                        if (path[i - 1, j] == 9)
                            adjCount=2;

                    }
                    if (inBounds(i, j+1))
                    {
                        if (path[i, j+1] != 0)
                            adjCount++;
                        if (path[i, j + 1] == 9)
                            adjCount=2;

                    }
                    if (inBounds(i,j-1))
                    {
                        if (path[i, j-1] != 0)
                            adjCount++;
                        if (path[i, j - 1] == 9)
                            adjCount=2;

                    }
                    if(adjCount<2 && numObjectives>0)
                    {
                        if(Random.Range(0,2)==1)
                        {
                            addPath(i, j);
                            path[i, j] = 5;
                            level0[i, j] = 5;

                        }
                        else
                        {
                            addPath(i, j);
                            path[i, j] = 6;
                            level0[i, j] = 6;

                        }
                        numObjectives--;
                    }
                }
            }
        }


        /*
        string s = "";
        for (int i = 0; i<rows; i++)
        {
            for (int j = 0; j <cols; j++)
            {
                s += level0[i, j];
                s += ",";
            }

            s = "";
        }*/

        //copies the paths into the level
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                if (path[i, j] == 2)//  || path[i,j] == 1)
                {
                    level0[i, j] = 0;
                }
            }
        }
        level0[startx, starty] = 8;
        level0[endx, endy] = 9;

    }

    /**
     * For the given point on the map (x,y), find the closest point on the main path
     * and add a path from there to this point(x,y)
     */
    public void addPath(int x, int y)
    {
        int nearx = 100;
        int neary = 100;
        Vector2 c = new Vector2(x, y);
        Vector2 n = new Vector2(nearx, neary);
        float currDist = (c - n).magnitude;
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                if (path[i, j] == 2)
                {
                    n = new Vector2(i, j);
                    if ((c - n).magnitude < currDist)
                    {
                        currDist = (c - n).magnitude;
                        nearx = i;
                        neary = j;
                    }
                }
            }
        }
        //bool cont = true;
        int count = 0;
        while (x != nearx || y != neary)
        {
            
            if (x>nearx && path[x-1,y]!=9)// && path[x-1,y]==1)
            {
                x--;
                path[x, y] = 2;
            }
            else if (x < nearx && path[x + 1, y] != 9)// && path[x + 1, y] == 1)
            {
                x++;
                path[x, y] = 2;
            }
            else if (y > neary && path[x, y-1] != 9)// && path[x, y-1] == 1)
            {
                y--;
                path[x, y] = 2;
            }
            else if (y < neary && path[x, y +1] != 9)// && path[x, y+1] == 1)
            {
                y++;
                path[x, y] = 2;
            }
            count++;
            if (count > 10)
                break;
        }
    }

    /**
     * returns true if the position is in bounds
     */
    public bool inBounds(int x, int y)
    {
        if (x < 0 || y < 0 || x >= rows || y >= cols)
           return false;
        return true;
    }

    /**
     * Returns true if turning the spot into a part of the path
     * would not connect it to another part of the path.
     */
    public bool legalPathPlacement(int x, int y)
    {
        
        bool rval = true;

        //false if out of bounds
        if (!inBounds(x, y))
            return false;

        int openAdjacents = 0;
        
        if(inBounds(x+1,y))
        {
            if (path[x+1, y] ==2 || path[x + 1, y] ==1 )
                openAdjacents++;
        }
        if(inBounds(x -1, y))
        {
            if (path[x - 1, y] ==2 || path[x - 1, y]==1)
                openAdjacents++;
        }
        if(inBounds(x, y+1))
        {
            if (path[x, y+1] ==2|| path[x, y + 1] == 1)
                openAdjacents++;
        }
        if(inBounds(x, y-1))
        {
            if (path[x, y-1] ==2 || path[x, y - 1] == 1)
                openAdjacents++;
        }

        if (openAdjacents > 1)
            rval = false;

        return rval;
    }

    /**
     * Recursivley attempts to wander the map and create a path from start to finish
     * without looping back on itself
     */
    public bool pathToFinish(int x, int y)
    {

        if (!inBounds(x, y))
            return false;

        
        if (path[x, y] == 9)
            return true;

        if (path[x, y] != 0)
            return false;

        path[x, y] = 2;

        int trying = Random.Range(0, 4);
        int[,] dirs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        for (int i = 0; i < 4; i++)
        {
            if(legalPathPlacement(x+dirs[trying, 0],y+dirs[trying, 1]) && pathToFinish(x + dirs[trying, 0], y + dirs[trying, 1]))
            {
                return true;
            }
            trying++;
            if (trying > 3)
                trying = 0;
        }

        path[x, y] = 0;
        return false;


    }
}
