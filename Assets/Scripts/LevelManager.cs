using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Texture")]
    [SerializeField] private Texture2D _levelTexture;

    [Header("Tils Prefab")]
    [SerializeField] private GameObject _prefabWallTile;
    [SerializeField] private GameObject _prefabRoadTile;

    [HideInInspector] public List<RoadTile> roadTilesList = new List<RoadTile>();
    [HideInInspector] public RoadTile defaultBallRoadTile;

    private Color _colorWall = Color.white;
    private Color _colorRoad = Color.black;

    private float _unitPerPixel;

    private void Awake()
    {
        Generate();
        defaultBallRoadTile = roadTilesList[0];
    }

    private void Generate()
    {
        _unitPerPixel = _prefabWallTile.transform.lossyScale.x;
        float halfUnitPerPixel = _unitPerPixel / 2f;

        float width = _levelTexture.width;
        float height = _levelTexture.height;

        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * _unitPerPixel) - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = _levelTexture.GetPixel(x, y);

                Vector3 spawnPosition = ((new Vector3(x, 0, y) * _unitPerPixel) - offset);

                if(pixelColor == _colorWall)
                {
                    Spawn(_prefabWallTile, spawnPosition);
                }
                else
                {
                    Spawn(_prefabRoadTile, spawnPosition);
                }
            }
        }
    }

    private void Spawn(GameObject prefabTile, Vector3 position)
    {
        position.y = prefabTile.transform.position.y;

        GameObject obj = Instantiate(prefabTile, position, Quaternion.identity, transform);

        if (prefabTile == _prefabRoadTile)
        {
            roadTilesList.Add(obj.GetComponent<RoadTile>());
        }
    }
}
