using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using System;

public class BallMovevent : MonoBehaviour
{
    [SerializeField] private SwipeListener _swipeListener;
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private float stepDuration = 0.1f;
    [SerializeField] private LayerMask WallsAndRoadsLayer;

    private const float MAX_RAY_DISTANCE = 10f;

    private Vector3 _moveDirection;
    private bool canMove = true;

    private void Start()
    {
        transform.position = _levelManager.defaultBallRoadTile.Position;

        _swipeListener.OnSwipe.AddListener(swipe => {
            switch (swipe) 
            {
                case "Right":
                    _moveDirection = Vector3.right;
                    break;
                case "left":
                    _moveDirection = Vector3.left;
                    break;
                case "Up":
                    _moveDirection = Vector3.forward;
                    break;
                case "Down":
                    _moveDirection = Vector3.back;
                    break;
            }
            MoveBall();
        });
    }

    private void MoveBall()
    {
        if (canMove)
        {
            canMove = false;

            RaycastHit[] hits = Physics.RaycastAll(transform.position, _moveDirection, MAX_RAY_DISTANCE, WallsAndRoadsLayer.value);

            Vector3 targetPosition = transform.position;

            int steps = 0;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.isTrigger)
                {

                }
                else
                {
                    if(i == 0)
                    {
                        canMove = true;
                        return;
                    }

                    steps = i;
                    targetPosition = hits[i - 1].transform.position;
                    break;
                }
            }

            float moveDuration = stepDuration * steps;
            transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutExpo).OnComplete(() => canMove = true);
        }
    }
}
