using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPos => _currentPos;

    private Vector3 _currentPos;
    private bool _gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        _gameStarted = true;
        _currentPos = transform.position;
    }

    public Vector3 GetWayPointPos(int index)
    {
        return CurrentPos + Points[index];
    }


    private void OnDrawGizmos()
    {
        if(!_gameStarted && transform.hasChanged)
        {
            _currentPos = transform.position;
        }

        for(int i=0; i<points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPos, 0.5f);

            if(i<points.Length -1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPos, points[i + 1] + _currentPos);
            }

        }
    }
}
