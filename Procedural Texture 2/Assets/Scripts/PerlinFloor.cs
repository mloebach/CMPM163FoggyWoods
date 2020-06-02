
using UnityEngine;

public class PerlinFloor : MonoBehaviour
{
    // Start is called before the first frame update
   public int width = 256;
   public int height = 256;

   public float scale = 20f;

   void Start(){
       Renderer renderer = GetComponent<Renderer>();
       renderer.material.mainTexture = GenerateTexture();
   }

    Texture2D GenerateTexture (){
 
        Texture2D texture = new Texture2D(width, height);
        
        for(int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                Color color = CalculateColor(x,y);
                texture.SetPixel(x,y,color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y){
        float xCoord =(float) x/width * scale;
        float yCoord =(float) y/height * scale;
        int seedA = Random.Range(0,200);
        float startColor = 90f/255f;
        float sampleA = Mathf.PerlinNoise(xCoord+seedA,yCoord+seedA);
        float colorRangeA = (28f/255f)*sampleA;
        return new Color(startColor+colorRangeA+(20f/255f), 
        startColor+colorRangeA+(60f/255f), startColor+colorRangeA);

    }


}