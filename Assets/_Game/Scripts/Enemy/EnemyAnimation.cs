using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    const string MOVE = "move";
    [SerializeField] Animator animator;
    public void EnableAnimator(bool enable) 
    {
        animator.enabled = enable;
    }
    public void SetMove(int move) 
    {
        if (move >= 0) 
        {
            animator.SetFloat(MOVE,move);
        }
    }
}
