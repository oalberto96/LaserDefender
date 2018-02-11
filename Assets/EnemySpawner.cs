using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public float speed = 5f;
	public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;

    private float xmax;
    private float xmin;
    private bool movingRight = true;

	// Use this for initialization
	void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightBoundary.x;
        xmin = leftBoundary.x;
        foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }

	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightEdgeOfFormation = transform.position.x + 0.5f * width;
        float leftEdgeOfFormation = transform.position.x - 0.5f * width;
        if (leftEdgeOfFormation < xmin || rightEdgeOfFormation > xmax)
        {
            movingRight = !movingRight;
        }
	}
}
