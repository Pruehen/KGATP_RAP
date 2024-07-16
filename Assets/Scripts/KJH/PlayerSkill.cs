using UnityEngine;

namespace kjh
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] GameObject Prefab_SkillProjectile;
        [Range(1, 20)][SerializeField] int Spread = 10;
        [Range(0f, 360f)][SerializeField] float Angle = 30;
        [Range(1f, 128f)][SerializeField] float Velocity = 30;        
        public void Command_Parrying()
        {
            for (int i = 0; i <= Angle * 0.5f; i += Spread)
            {                
                GameObject projectile1 = ObjectPoolManager.Instance.DequeueObject(Prefab_SkillProjectile);
                GameObject projectile2 = ObjectPoolManager.Instance.DequeueObject(Prefab_SkillProjectile);

                Quaternion offset1 = Quaternion.Euler(0, i, 0);
                Quaternion offset2 = Quaternion.Euler(0, -i, 0);
                projectile1.GetComponent<PlayerSkillProjectile>().Init(this.transform.position, this.transform.rotation * offset1, Velocity);
                projectile2.GetComponent<PlayerSkillProjectile>().Init(this.transform.position, this.transform.rotation * offset2, Velocity);
            }
        }
    }
}