using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    List<Node> path = new List<Node>(); 
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }
    
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }

        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.GoldPenalty();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++) // changed initial "i" from =0 to =1 so when enemy gets a new path they go straight to the next node in the path*** (plus enemies dont wait at spawn)
        {
            Vector3 startpos = transform.position; 
            Vector3 endpos = gridManager.GetPositionFromCoordinates(path[i].coordinates); 
            float travelPercent = 0f;

            transform.LookAt(endpos);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startpos, endpos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

}
