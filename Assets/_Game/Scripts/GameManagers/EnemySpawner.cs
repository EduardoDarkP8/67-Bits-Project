using System.Collections.Generic;
using UnityEngine;
namespace BitsProject
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        [SerializeField] int maxEnemies;
        [SerializeField] float spawnTime;
        [SerializeField] EnemyManager enemyPrephab;
        float currentTime;
		List<EnemyManager> enemies = new List<EnemyManager>();
		
        private void Start()
		{
            currentTime = spawnTime;
		}
		private void Update()
		{
			SpawnerUpdate();
		}
		
        public void PopEnemie(EnemyManager enemy) 
        {
            if (enemies.Contains(enemy)) 
            {
                enemies.Remove(enemy);
            }
        }
        
        public void InstantiateEnemy() 
        {
            var randomPosition = NavMeshPaths.Instance.GetRandomPosition(out int index);
            var newEnemie = Instantiate(enemyPrephab, randomPosition.position, Quaternion.identity);
            newEnemie.transform.LookAt(NavMeshPaths.Instance.GetPosition(index));
            newEnemie.enemyMovement.SetIndex(index);
			enemies.Add(newEnemie);
        }

		public void SpawnerUpdate() 
        {
            if (currentTime >= spawnTime) 
            {
                currentTime = 0;
                InstantiateEnemy();
            }
            if (enemies.Count < maxEnemies) 
            {
                currentTime += Time.deltaTime;
            }
        }

    }
}
