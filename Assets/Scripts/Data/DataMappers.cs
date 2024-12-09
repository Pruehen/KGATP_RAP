using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
    public int Atk { get; set; }
    public PlayerSkill Atk_base { get; set; }
    public PlayerSkill Atk_strong { get; set; }
    public PlayerSkill Evasion { get; set; }
    public PlayerSkill Atk_special { get; set; }
    public int Cost_type { get; set; }
}

//�÷��̾� ��ų�� �� ��ų�� ��ġ�� �����Ͱ� ��� �и��ϰ� �ʹ�. �ϴ� ����� ��.
public class Skill
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int Cost_value { get; set; }
    public int Cost_recovery { get; set; }
    public float Atk_multiply { get; set; }
    public float Cooltime {  get; set; }
    public int Buff { get; set; }
    public int Debuff { get; set; }
    public int Melee_angle { get; set; }
    public float Missle_angle { get; set; }
    public int Missle_ea { get; set; }
    public int Missle_ID { get; set; }
    public int Combo_ID { get; set; }
}

public class PlayerSkill
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public int Cost_value { get; set; }
    public int Cost_recovery { get; set; }
    public float Atk_multiply { get; set; }
    public float Cooltime { get; set; }
    public int Buff { get; set; }
    public List<int> Debuff { get; set; }
    public int Melee_angle { get; set; }
    public int Combo_ID { get; set; }
}

public class EnemySkill
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public float Cooltime { get; set; }
    public int Buff { get; set; }
    public int Debuff { get; set; }
    public float Missle_angle { get; set; }
    public int Missle_ea { get; set; }
    public int Missle_ID { get; set; }
    public int Combo_ID { get; set; }
}

public class Projectile
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public float Size_min { get; set; }
    public float Size_increase { get; set; }
    public float Size_max { get; set; }
    public float Atk_multiply { get; set; }
    public float Force { get; set; }
    public float Lifetime { get; set; }
    public string Collision_able { get; set; }  //�ϴ� �ޱ⸸
    public string Disappear_condition { get; set; } //�ϴ� �ޱ⸸
    public bool Parry_able { get; set; }
    public int Bounce_num { get; set; }
    public string Combo_ID { get; set; }   //�ϴ� �ޱ⸸
}

public class Enemy
{
    public int DataID { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int HP { get; set; }
    public int Atk { get; set; }
    public float Rotation_sec { get; set; }
    public float Rotation_angle { get; set; }
    public EnemySkill Skill { get; set; }
}

