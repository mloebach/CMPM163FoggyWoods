using System.Collections;
using UnityEngine;


public class treeSpawner : MonoBehaviour
{

	public GameObject[] trees;
	public int mapWidth = 100;
	public int mapHeight = 100;
	int [,] mapgrid;
	public int buildingFootprint = 10;

    // Start is called before the first frame update
    void Start()
    {

	

	//Generate a random number between two numbers  
	int RandomNumber()  {  
    	//Random random = new Random();  
    	//return random.Next(min, max);  
		return (int) UnityEngine.Random.Range(0f, 4.0f);
	}  

	//generate city
        for (int h = 0; h < mapHeight; h++) {
		for (int w = 0; w < mapWidth; w++) {
			
			
			Vector3 pos = new Vector3(w*buildingFootprint + RandomNumber(), 0, h*buildingFootprint + RandomNumber() );

			Instantiate(trees[0], pos, Quaternion.identity);
		}
	}
}

  

}
