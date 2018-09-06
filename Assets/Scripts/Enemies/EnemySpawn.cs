using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {


	public float spawnWidth;
	public float spawnRate;
	public GameObject enemyPrefab;
	private float lastSpawnTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lastSpawnTime + 1 / spawnRate < Time.time) {
			lastSpawnTime = Time.time;
			Vector3 spawnPosition = transform.position;
			spawnPosition += new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
			Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
		}
	}
}
