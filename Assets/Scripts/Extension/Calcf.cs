using EnumTypes;
using System.Collections.Generic;
using UnityEngine;

public struct Calcf
{
    //public static float ShipUpgradePrice(ShipTable table, int level)
    //{
    //    return level * (table._maxCombatSlot * table._maxCombatSlot * table._star * table._star + 37) * 0.1f;
    //}

    //public static float EquipUPgradePrice(int equipLevel)
    //{
    //    return (equipLevel * 1200 * (((equipLevel / 4) * (equipLevel / 4)) + 1));
    //}

    //public static float DmgMultiple_Def(float baseDmg, float def)
    //{
    //    return baseDmg / (baseDmg + def);
    //}
    //public static long GetFleetUpgradeNeedCredit(int fleetCost)
    //{
    //    return (long)(Mathf.Pow(2, fleetCost)) * 50000;
    //}
    //public static long DropCredit(long _dropCredit)
    //{
    //    if (_dropCredit == 0)
    //    {
    //        return 0;
    //    }
    //    else
    //    {
    //        return (long)(NavMissionLogicManager.Instance.GetValue_StageState() * _dropCredit) + 15;
    //    }
    //}

    //static float GetWeaponPower(WeaponSkillTable table)
    //{
    //    return (table._dmg * 0.01f / table._collTime) * table._maxRange * 0.001f;
    //}


    //public static float CombatPower(ShipMaster shipMaster)
    //{
    //    ShipData shipData = shipMaster.CombatData.ShipData;
    //    List<Weapon> weaponList = shipMaster.FCS.UsingWeaponList();

    //    float dp = Mathf.Sqrt(shipData.GetFinalState(CombatStateType.Hp) * shipData.GetFinalState(CombatStateType.Def));
    //    float ap = shipData.GetFinalState(CombatStateType.Atk);
    //    float critMultiple = 1 + (shipData.GetFinalState(CombatStateType.CritDmg) * 0.01f) * (shipData.GetFinalState(CombatStateType.CritRate) * 0.01f);
    //    ap *= critMultiple;

    //    float physicsDps = 0;
    //    float opticsDps = 0;
    //    float particleDps = 0;
    //    float plasmaDps = 0;

    //    foreach (Weapon weapon in weaponList)
    //    {
    //        WeaponSkillTable weaponSkillTable = weapon.Table;

    //        switch (weaponSkillTable._weaponProjectileType)
    //        {
    //            case WeaponProjectileType.Physics:
    //                physicsDps += GetWeaponPower(weaponSkillTable);
    //                break;
    //            case WeaponProjectileType.Optics:
    //                opticsDps += GetWeaponPower(weaponSkillTable);
    //                break;
    //            case WeaponProjectileType.Particle:
    //                particleDps += GetWeaponPower(weaponSkillTable);
    //                break;
    //            case WeaponProjectileType.Plasma:
    //                plasmaDps += GetWeaponPower(weaponSkillTable);
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    physicsDps *= (100 + shipData.GetFinalState(CombatStateType.PhysicsDmg)) * 0.01f;
    //    opticsDps *= (100 + shipData.GetFinalState(CombatStateType.OpticsDmg)) * 0.01f;
    //    particleDps *= (100 + shipData.GetFinalState(CombatStateType.ParticleDmg)) * 0.01f;
    //    plasmaDps *= (100 + shipData.GetFinalState(CombatStateType.PlasmaDmg)) * 0.01f;

    //    ap *= (physicsDps + opticsDps + particleDps + plasmaDps);

    //    return dp + ap;
    //}
}
