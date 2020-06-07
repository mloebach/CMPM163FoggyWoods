using System.Collections;
using UnityEngine;


public class treeSpawner : MonoBehaviour
{

	public GameObject[] trees;
	public GameObject xstreets;
	public GameObject zstreets;
	public GameObject crossroad;
	public int mapWidth = 100;
	public int mapHeight = 100;
	int [,] mapgrid;
	int buildingFootprint = 5;

    // Start is called before the first frame update
    void Start()
    {

	// mapgrid = new int[mapWidth, mapHeight];

	// //generate map data
	// for(int h = 0; h < mapWidth; h++) 
	// {
	// 	for(int w = 0; w < mapHeight; w++)
	// 	{
	// 		mapgrid[w,h] = (int) (Mathf.PerlinNoise(w/10.0f,h/10.0f)*10);
	// 		Debug.Log(w + " " + h);
	// 	}
	// 	//Debug.Log('\n');
	// }
	// //Debug.Log("Width = " + mapgrid[0,].length);

	// //build streets
	// int x =0;
	// for(int n = 0; n < 50; n++) 
	// {
	// 	for(int h = 0; h < mapHeight; h++)
	// 	{
	// 		mapgrid[x,h] = -1;
	// 	}
	// 	x += Random.Range(5,5);
	// 	if(x >= mapWidth) break;
	// }

	// int z = 0;
	// for(int n = 0; n < 10; n++)
	// {
	// 	for(int w = 0; w < mapWidth; w++) 
	// 	{
	// 		if(mapgrid[w,z] == -1) //put in a cross road
	// 		{
	// 			mapgrid[w,z] = -3;
	// 			//remove street up,down,left,right
	// 			if(w != (mapHeight -1))
	// 				mapgrid[w+1,z] = 11;
	// 			if(w != 0)
	// 				mapgrid[w-1,z] = 11;
	// 			if(z != 0)
	// 				mapgrid[w,z-1] = 11;
	// 			if(z != (mapHeight -1))
	// 				mapgrid[w,z+1] = 11;
	// 		}
	// 		else
	// 		{
	// 			if(w != 0 && mapgrid[w-1,z] != 11)
	// 				mapgrid[w,z] = -2;
	// 		}
	// 	}
	// 	z += Random.Range(5,5);
	// 	if(z >= mapHeight) break;
	// }
	

	//Generate a random number between two numbers  
	int RandomNumber()  {  
    	//Random random = new Random();  
    	//return random.Next(min, max);  
		return (int) UnityEngine.Random.Range(0f, 4.0f);
	}  

	//float seed = 67;
	//generate city
        for (int h = 0; h < mapHeight; h++) {
		for (int w = 0; w < mapWidth; w++) {
			
			//int result = (int) (Mathf.PerlinNoise(w/10.0f+seed,h/10.0f+seed) * 10);
			int result = (int) (Mathf.PerlinNoise(w/10.0f,h/10.0f) *10);
			//int result = mapgrid[w,h];
			Vector3 pos = new Vector3(w*buildingFootprint + RandomNumber(), 0, h*buildingFootprint + RandomNumber() );
			//Vector3 pos = new Vector3(w*buildingFootprint, 0, h*buildingFootprint);

			if(result < -2)
				Instantiate(crossroad,pos,crossroad.transform.rotation);
			else if(result < -1)
				Instantiate(xstreets,pos,xstreets.transform.rotation);
			else if(result < 0)
				Instantiate(zstreets,pos,zstreets.transform.rotation);
			else if(result < 2)
				Instantiate(trees[0],pos,Quaternion.identity);
			else if(result < 4)
				Instantiate(trees[1],pos,Quaternion.identity);
			else if(result < 6)
				Instantiate(trees[2],pos,Quaternion.identity);
			else if(result < 7)
				Instantiate(trees[3],pos,Quaternion.identity);
			else if(result < 8)
				Instantiate(trees[4],pos,Quaternion.identity);
			else if(result < 10)
                Instantiate(trees[5],pos,Quaternion.identity);
		}
	}
}

// public class RandomGenerator    
// {    
//     // Generate a random number between two numbers    
//     public int RandomNumber(int min, int max)    
//     {    
//         Random random = new Random();    
//         return random.Next(min, max);    
//     }    
// }
  

}
