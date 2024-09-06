using UnityEngine;

namespace FluffyDisket
{
    public class IUnit : MonoBehaviour
    {
        [SerializeField] public Animator animator;

        public virtual void SetAnim(string anim)
        {
            animator?.Play(anim);
        }

        public void SetAnimSpeed(float sp)
        {
            if(animator)
               animator.speed = sp;
        }
    }
}