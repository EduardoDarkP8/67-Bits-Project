using UnityEngine;
namespace BitsProject
{
    public class PlayerColor : MonoBehaviour
    {
        [SerializeField] Color[] colorStages;
        [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
        public void UpdateColor(int level) 
        {
            for (int i = 0; i < colorStages.Length; i++) 
            {
                 if(level > i)skinnedMeshRenderer.material.color = colorStages[i];
            }
        }
    }
}