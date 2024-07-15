using UnityEngine;

namespace kjh
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] GameObject Prefab_SkillProjectile;
        public void Command_Parrying()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject projectile1 = ObjectPoolManager.Instance.DequeueObject(Prefab_SkillProjectile);
                GameObject projectile2 = ObjectPoolManager.Instance.DequeueObject(Prefab_SkillProjectile);

                Quaternion offset1 = Quaternion.Euler(0, i * 5, 0);
                Quaternion offset2 = Quaternion.Euler(0, i * -5, 0);
                projectile1.GetComponent<PlayerSkillProjectile>().Init(this.transform.position, this.transform.rotation * offset1, 30, 15);
                projectile2.GetComponent<PlayerSkillProjectile>().Init(this.transform.position, this.transform.rotation * offset2, 30, 15);
            }
        }
    }
}