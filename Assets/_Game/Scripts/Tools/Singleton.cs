using UnityEngine;
namespace BitsProject
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] bool dontDestroyOnLoad;
        public static T Instance;
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }
    }
}