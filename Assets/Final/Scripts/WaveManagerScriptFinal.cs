using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random; //Tells Random to use the Unity Engine random number generator.

public class WaveManagerScriptFinal : MonoBehaviour {

    [System.Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public Vector2 gridStartPos = new Vector2( 0f, 0f );
    public int columns = 8;                                         //Number of columns in our game board.
    public int rows = 8;                                            //Number of rows in our game board.
    public Count pickupCount = new Count(5, 9);
    public Count spawnerCount = new Count(1, 5);
    //public Count enemyCount = new Count(1, 5);
    //public GameObject exit;
    public GameObject[] pickupTiles;
    public GameObject[] spawnerTiles;
    public GameObject[] enemyTiles;
    public GameObject[] bossTiles;

    public string parentName = "Level";

    private Transform parent;

    private Transform transformHolder;                          //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();  //A list of possible locations to place tiles.
    
    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = Mathf.CeilToInt(gridStartPos.x - 0.1f); x <= columns - 1; x++)
        {
            for (int y = Mathf.CeilToInt(gridStartPos.y - 0.1f); y <= rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    
    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        
        Vector3 randomPosition = gridPositions[randomIndex];
        
        gridPositions.RemoveAt(randomIndex);
        
        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        if (tileArray.Length == 0)
        {
            return;
        }

        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            
            GameObject obj = (GameObject)Instantiate(tileChoice, randomPosition, Quaternion.identity);

            if (parent)
            {
                obj.transform.SetParent(parent);
            }
        }
    }


    //SetupScene initializes our level and calls the previous functions to lay out the game wave
    public void SetupScene(int wave)
    {
        parent = GameObject.Find(parentName).transform;

        //Reset our list of gridpositions.
        InitialiseList();
        
        LayoutObjectAtRandom(pickupTiles, pickupCount.minimum, pickupCount.maximum);

        int spawnerCount = (int)Mathf.Log(wave, 2f);

        LayoutObjectAtRandom(spawnerTiles, spawnerCount, spawnerCount);

        //Determine number of enemies based on current level number, based on a logarithmic progression
        int enemyCount = (int)Mathf.Log(wave, 2f);
        
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        if (wave % 10 == 0)
        {
            LayoutObjectAtRandom(bossTiles, (wave / 20) + 1, (wave / 20) + 1);
        }

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}

