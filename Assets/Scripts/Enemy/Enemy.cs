using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;
    
    [SerializeField] private float moveSpeed = 3f;

    public float MoveSpeed { get; set; }
    public WayPoint wayPoint { get; set; }

    public Vector3 CurrentPointPosition => wayPoint.GetWayPointPos(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _lastPointPos;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;

        _lastPointPos = transform.position;
    }

    private void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPosReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if(CurrentPointPosition.x > _lastPointPos.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPosReached()
    {
        float disToNextPointPos = (transform.position - CurrentPointPosition).magnitude;

        if(disToNextPointPos < 0.1f)
        {
            _lastPointPos = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWayPointIndex = wayPoint.Points.Length - 1; 
        if(_currentWaypointIndex < lastWayPointIndex)
        {
            _currentWaypointIndex ++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }
}
