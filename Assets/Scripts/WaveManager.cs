using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random; //Tells Random to use the Unity Engine random number generator.

public class WaveManager : MonoBehaviour {

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
    
    public int columns = 8;                                         //Number of columns in our game board.
    public int rows = 8;                                            //Number of rows in our game board.
    public Count pickupCount = new Count(5, 9);
    public Count spawnerCount = new Count(1, 5);
    //public Count enemyCount = new Count(1, 5);
    //public GameObject exit;
    public GameObject[] pickupTiles;
    public GameObject[] spawnerTiles;
    public GameObject[] enemyTiles;

    public string parentName = "Level";

    private Transform parent;

    private Transform transformHolder;                          //A variable to store a reference to the transform of our Board object.
    private List<Vector3> gridPositions = new List<Vector3>();  //A list of possible locations to place tiles.
    
    void InitialiseList()
    {
        gridPositions.Clear();
        
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    
    ////Sets up the outer walls and floor (background) of the game board.
    //void BoardSetup()
    //{
    //    transformHolder = new GameObject("Board").transform;

    //    // starting from -1 to fill corners.
    //    for (int x = -1; x < columns + 1; x++)
    //    {
    //        for (int y = -1; y < rows + 1; y++)
    //        {
    //            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

    //            //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
    //            if (x == -1 || x == columns || y == -1 || y == rows)
    //            {
    //                toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
    //            }

    //            GameObject instance =
    //                Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                
    //            instance.transform.SetParent(transformHolder);
    //        }
    //    }
    //}
    
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

        //Creates the outer walls and floor.
        //BoardSetup();

        //Reset our list of gridpositions.
        InitialiseList();
        
        LayoutObjectAtRandom(pickupTiles, pickupCount.minimum, pickupCount.maximum);
        
        LayoutObjectAtRandom(spawnerTiles, spawnerCount.minimum, spawnerCount.maximum);

        //Determine number of enemies based on current level number, based on a logarithmic progression
        int enemyCount = (int)Mathf.Log(wave, 2f);
        
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}

