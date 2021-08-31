using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BallRoadPainter : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private BallMovevent _ballMovevent;
    [SerializeField] private MeshRenderer _ballmeshRenderer;

    public int PaintedRoadTiles = 0;

    private void Start()
    {
        _ballmeshRenderer.material.color = _levelManager.paintColor;

        Paint(_levelManager.defaultBallRoadTile, 0.5f, 0f);

        _ballMovevent.onMoveStart += OnBallMoveStarHandler;
    }

    private void OnBallMoveStarHandler(List<RoadTile> RoadTiles, float totalDuration)
    {
        float stepDuration = totalDuration / RoadTiles.Count;

        for (int i = 0; i < RoadTiles.Count; i++)
        {
            RoadTile roadTile = RoadTiles[i];

            if (!roadTile.IsPainted)
            {
                float duration = totalDuration / 2f;
                float delay = i * (stepDuration / 2f);
                Paint(roadTile, duration, delay);

                if (PaintedRoadTiles == _levelManager.roadTilesList.Count)
                {
                    Debug.Log("Level Completed");
                }
            }
        }
    }

    private void Paint(RoadTile roadTile, float duration, float delay)
    {
        roadTile.MeshRenderer.material.DOColor(_levelManager.paintColor, duration).SetDelay(delay);

        roadTile.IsPainted = true;
        PaintedRoadTiles++;
    }
}
