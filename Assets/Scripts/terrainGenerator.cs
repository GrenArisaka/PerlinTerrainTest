using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainGenerator : MonoBehaviour
{


    public int seed;
    public int lerps = 4;
    public int width, length, depth;
    public float BaseHeight = 20f;
    public float scale = 19.3f;
    float nextActionTime = 0.0f;
    public float period = 5.0f;

    public float treePerlinThreshold = 0.7f;
    public float treePerlinScale = 10.0f;
    public int treeCount = 300;
    public int treeSpread = 10;
    // Start is called before the first frame update
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        // Terrain Heightmap Stuff
        TerrainData tData = GenerateTerrain(terrain.terrainData);
        // Terrain Painting Stuff
        tData.SetAlphamaps(0, 0, getSplatMapData(tData));
        tData.treeInstances = generateTreeInstances(1);
    }
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            // execute block of code here




        }

    }

    //          TERRAIN STUFF STARTS HERE
    TerrainData GenerateTerrain(TerrainData tDataInput)
    {
        TerrainData tData = tDataInput;
        // Some Terrain properties related manipulating down here.
        tData.heightmapResolution = width + 1;
        tData.size = new Vector3(width, depth, length);
        tData.SetHeights(0, 0, generateHeightMap());
        return tData;
    }
    float[,] generateHeightMap()
    {
        float[,] heightmap = new float[width, length];
        // Some heightmap- related editing here.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                heightmap[x, y] = generateHeightFloat(x, y, seed, lerps);

            }
        }
        return heightmap;
    }
    float generateHeightFloat(int x, int y, int seed, int lerps)
    {
        float height = 0;
        /*
        float xCoord = (float)x / width *scale + seed ;
        float yCoord = (float)y / length * scale + seed;
        height = Mathf.PerlinNoise(xCoord, yCoord);

        for (int i = 0;i<lerps;i++) {
            float div = Mathf.Pow(lerpRate, i);
            xCoord = (float)x / width * scale/div + seed*i;
            yCoord = (float)y / length * scale/div + seed*i;
            height = height * ((1-1/div)+ Mathf.PerlinNoise(xCoord, yCoord)/div);

        }
       */


        float xCoord = (float)x / width * scale + seed;
        float yCoord = (float)y / length * scale + seed;
        float rawHeight = Mathf.PerlinNoise(xCoord, yCoord) * depth;

        for (int i = 0; i < lerps; i++)
        {
            float div = Mathf.Pow(2, (i + 1));
            xCoord = (float)x / width * scale * div + seed;
            yCoord = (float)y / length * scale * div + seed;
            float h = (Mathf.PerlinNoise(xCoord, yCoord) / div * 2 + (1 - 1 / div));
            rawHeight = rawHeight * h;

        }


        height = (BaseHeight + rawHeight) / depth;
        return height;
    }
    //          TERRAIN STUFF ENDS HERE

    //          TERRAIN PAINTING STARTS HERE
    float[,,] getSplatMapData(TerrainData tData)
    {
        float[,,] SplatMapData = new float[tData.alphamapWidth, tData.alphamapHeight, tData.alphamapLayers];
        for (int y = 0; y < tData.alphamapHeight; y++)
        {
            for (int x = 0; x < tData.alphamapWidth; x++)
            {
                float x_01 = (float)x / tData.alphamapWidth;
                float y_01 = (float)y / tData.alphamapHeight;

                float height = tData.GetHeight(Mathf.RoundToInt(y_01 * tData.heightmapResolution), Mathf.RoundToInt(x_01 * tData.heightmapResolution));
                float steepness = tData.GetSteepness(y_01, x_01);
                float[] splatweights = new float[tData.alphamapLayers];
                splatweights[0] = Mathf.Pow(1 - height / depth, 2);
                splatweights[1] = Mathf.Pow(height / depth, 2);
                splatweights[2] = Mathf.Clamp01(steepness * steepness / (tData.heightmapResolution / 5.0f));
                float z = splatweights[0] + splatweights[1] + splatweights[2];
                for (int i = 0; i < tData.alphamapLayers; i++)
                {
                    splatweights[i] /= z;
                    SplatMapData[x, y, i] = splatweights[i];

                }



            }
        }



        return SplatMapData;
    }
    TreeInstance[] generateTreeInstances(int numberoftypes)
    {
        TreeInstance[] trees = new TreeInstance[treeCount];
        int tIndex = 0;
        int spread = treeSpread / width;
        TreeInstance tree = new TreeInstance
        {
            prototypeIndex = 0,
            position = new Vector3(0, 0, 0)
        };
        trees[0] = tree;
        Debug.Log(trees.Length);
        return trees;
    }
}
