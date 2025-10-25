using System.Collections.Generic;
using UnityEngine;

public class Create_Map : MonoBehaviour
{
    [SerializeField] GameObject player;
    List<GameObject> prefab_tiles = new List<GameObject>();

    [SerializeField] GameObject Map;
    [SerializeField] GameObject Enviroment_Objects;
    [SerializeField] List<GameObject> floor_prefabs = null;
    [SerializeField] List<GameObject> gravestones_prefabs = null;
    [SerializeField] List<GameObject> trees_prefabs = null;
    [SerializeField] List<GameObject> statues_prefabs = null;
    [SerializeField] int width = 0;
    [SerializeField] int height = 0;


    void Start()
    {
        Generate_Noise_Map();
        PlaceEnviromentalObjectsOnMap();
    }

    void Generate_Noise_Map()
    {
        for (int x = -width; x < width; x++)
        {
            for (int y = -height; y < height; y++)
            {
                Vector2Int tilePosition = new Vector2Int(x, y);
                float noise = Generate_Noise_2(x, y);

                if (noise >= 0.0 && noise < 0.4f)
                {
                    GameObject new_tile = Instantiate(floor_prefabs[0], new Vector3(x, y, 0), Quaternion.identity);
                    new_tile.transform.parent = Map.transform;
                    prefab_tiles.Add(new_tile);
                }
                else if(noise >= 0.4 && noise < 0.6f)
                {
                    GameObject new_tile = Instantiate(floor_prefabs[1], new Vector3(x, y, 0), Quaternion.identity);
                    new_tile.transform.parent = Map.transform;
                    prefab_tiles.Add(new_tile);
                }
                else if (noise >= 0.6 && noise < 0.7f)
                {
                    GameObject new_tile = Instantiate(floor_prefabs[2], new Vector3(x, y, 0), Quaternion.identity);
                    new_tile.transform.parent = Map.transform;
                    prefab_tiles.Add(new_tile);
                }
                else if (noise >= 0.7 && noise < 0.8f)
                {
                    GameObject new_tile = Instantiate(floor_prefabs[3], new Vector3(x, y, 0), Quaternion.identity);
                    new_tile.transform.parent = Map.transform;
                    prefab_tiles.Add(new_tile);
                }
                else if (noise >= 0.8 && noise <= 1.0f)
                {
                    GameObject new_tile = Instantiate(floor_prefabs[4], new Vector3(x, y, 0), Quaternion.identity);
                    new_tile.transform.parent = Map.transform;
                    prefab_tiles.Add(new_tile);
                }
            }
        }
    }

    void PlaceEnviromentalObjectsOnMap()
    {
        for(int i =0; i < 150; ++i)
        {
            int x = Random.Range(-width, width);
            int y = Random.Range(-height, height);
            
            float noise = Generate_Noise_2(x, y);
            if (noise >= 0.0 && noise < 0.6f)
            {
                GameObject new_tile = Instantiate(trees_prefabs[Random.Range(0, trees_prefabs.Count)], new Vector3(x, y, 0), Quaternion.identity);
                new_tile.transform.parent = Enviroment_Objects.transform;
                prefab_tiles.Add(new_tile);
            }
        }

        for (int i = 0; i < 200; ++i)
        {
            int x = Random.Range(-width, width);
            int y = Random.Range(-height, height);

            float noise = Generate_Noise_2(x, y);
            if (noise >= 0.0 && noise < 0.6f)
            {
                GameObject new_tile = Instantiate(statues_prefabs[Random.Range(0, statues_prefabs.Count)], new Vector3(x, y, 0), Quaternion.identity);
                new_tile.transform.parent = Enviroment_Objects.transform;
                prefab_tiles.Add(new_tile);
            }
        }

        for (int i = 0; i < 200; ++i)
        {
            int x = Random.Range(-width, width);
            int y = Random.Range(-height, height);

            float noise = Generate_Noise_2(x, y);
            if (noise >= 0.6 && noise < 0.8f)
            {
                GameObject new_tile = Instantiate(gravestones_prefabs[Random.Range(0, gravestones_prefabs.Count)], new Vector3(x, y, 0), Quaternion.identity);
                new_tile.transform.parent = Enviroment_Objects.transform;
                prefab_tiles.Add(new_tile);
            }
        }
    }

    // Source: youtu.be/qNZ-0-7WuS8&lc=UgyoLWkYZxyp1nNc4f94AaABAg
    float Generate_Noise_2(int x, int y)
    {
        float x_n = (x + 1000) * 0.5f;
        float y_n = (y + 1000) * 0.5f;

        float magnification = 7.0f;
        float raw_perlin = Mathf.PerlinNoise(
            (x_n) / magnification,
            (y_n) / magnification
        );

        float clamp_perlin = Mathf.Clamp01(raw_perlin); 
        float scaled_perlin = clamp_perlin * 5;

        return clamp_perlin;
    }
}
