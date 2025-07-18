using UnityEngine;
namespace BitsProject
{
    public class NavMeshPaths : Singleton<NavMeshPaths>
    {
        [SerializeField] Transform[] positions;
        public Transform GetPosition(int index) 
        {
            if (index < positions.Length)
                return positions[index];
            return positions[0];
        }
        public int GetRandomIndex() 
        {
            return Random.Range(0, positions.Length);
        }
        public Transform GetRandomPosition(out int index)
		{
            index = GetRandomIndex();

			return GetPosition(index);

		} 
    }
}