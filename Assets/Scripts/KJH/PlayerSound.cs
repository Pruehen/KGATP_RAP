using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] GameObject Prefab_PC_sound_atk_base_1_2;
    [SerializeField] GameObject Prefab_PC_sound_atk_base_3;
    [SerializeField] GameObject Prefab_PC_sound_atk_parrying_1;
    [SerializeField] GameObject Prefab_PC_sound_atk_special_1;
    [SerializeField] GameObject Prefab_PC_sound_atk_strong_1;
    [SerializeField] GameObject Prefab_PC_sound_evasion_1;

    void PlaySound(GameObject prefab)
    {
        SoundPlayerManager.Instance.PlaySound(prefab, this.transform.position);
    }
    /// <summary>
    /// combo = 1, 2, 3
    /// </summary>
    /// <param name="combo"></param>
    public void Play_AttackSound(int combo)
    {
        if(combo == 1 || combo == 2)
        {
            PlaySound(Prefab_PC_sound_atk_base_1_2);
        }
        else
        {
            PlaySound(Prefab_PC_sound_atk_base_3);
        }
    }
    public void Play_StrongAttackSound()
    {
        PlaySound(Prefab_PC_sound_atk_strong_1);
    }
    public void Play_EvasionSound()
    {
        PlaySound(Prefab_PC_sound_evasion_1);
    }
    public void Play_ParryingSound()
    {
        PlaySound(Prefab_PC_sound_atk_parrying_1);
    }
    public void Play_SpecialAttackSound()
    {
        PlaySound(Prefab_PC_sound_atk_special_1);
    }
}
