using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public float TerrainWidth = 47.0f;
    public GameObject TerrainPrefab;
    private BirdController birdController;
    private List<GameObject> TerrainList;

    public float SpaceBetweenObstacles = 4.0f;
    public GameObject ObstaclePrefab;
    private List<GameObject> ObstacleList;

    public GameObject oldTVEffect;

    private int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    private void Start()
    {
        TerrainList = new List<GameObject>();
        ObstacleList = new List<GameObject>();
        birdController = FindObjectOfType<BirdController>();
        score = 0;
        if(TerrainList.Count == 0)
        {
            TerrainList.Add(GameObject.Instantiate(TerrainPrefab, new Vector3(0, 0, 0), Quaternion.identity));
        }

        if (ObstacleList.Count == 0)
        {
            for (int i = 1; i < 5; i++)
            {
                ObstacleList.Add(GameObject.Instantiate(ObstaclePrefab, new Vector3(3 + SpaceBetweenObstacles * i, 0, 0), Quaternion.identity));
            }
        }
    }

    private void Update()
    {
        if (birdController.transform.position.x - TerrainList[TerrainList.Count -1].transform.position.x > 0)
        {
            TerrainList.Add(GameObject.Instantiate(TerrainPrefab, new Vector3(birdController.transform.position.x + TerrainWidth * 0.5f, 0, 0), Quaternion.identity));
        }

        if (birdController.transform.position.x - ObstacleList[ObstacleList.Count - 4].transform.position.x > 0)
        {
            ObstacleList.Add(GameObject.Instantiate(ObstaclePrefab, new Vector3(birdController.transform.position.x + SpaceBetweenObstacles * 4.0f, 0, 0), Quaternion.identity));
        }

        if (TerrainList.Count > 0 && birdController.transform.position.x - TerrainList[0].transform.position.x > TerrainWidth)
        {
            GameObject.Destroy(TerrainList[0]);
            TerrainList.RemoveAt(0);
        }

        if (ObstacleList.Count > 0 && birdController.transform.position.x - ObstacleList[0].transform.position.x > SpaceBetweenObstacles + 7.0f)
        {
            GameObject.Destroy(ObstacleList[0]);
            ObstacleList.RemoveAt(0);
        }

        SwitchOldTVEffect();
    }

    private void SwitchOldTVEffect()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            oldTVEffect.SetActive(!oldTVEffect.activeInHierarchy);
        }
    }
}
