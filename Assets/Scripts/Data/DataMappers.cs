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
    public int Atk_base { get; set; }
    public int Atk_strong { get; set; }
    public int Evasion { get; set; }
    public int Atk_special { get; set; }
    public int Cost_type { get; set; }
}

//플레이어 스킬과 적 스킬이 겹치는 데이터가 적어서 분리하고 싶다. 일단 만들긴 함.
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

public class SkillEnemy
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
    public int Lifetime { get; set; }
    public string Collision_able { get; set; }
    public bool Parry_able { get; set; }
    public int Bounce_num { get; set; }
    public int Combo_ID { get; set; }
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
    public int Skill { get; set; }
}

