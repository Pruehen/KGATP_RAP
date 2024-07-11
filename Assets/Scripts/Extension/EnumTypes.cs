namespace EnumTypes
{
    public enum CombatStateType
    {
        Hp, 
        Atk, 
        Def, 
        CritRate, 
        CritDmg, 
        PhysicsDmg, 
        OpticsDmg, 
        ParticleDmg, 
        PlasmaDmg
    }
    public enum IncreaseableStateType
    {
        Hp,
        HpMultiple,
        Atk,
        AtkMultiple,
        Def,
        DefMultiple,
        CritRate,
        CritDmg,
        PhysicsDmg,
        OpticsDmg,
        ParticleDmg,
        PlasmaDmg
    }
    public enum EquipType
    {
        Weapon,
        Armor,
        Thruster,
        Reactor,
        Radiator
    }
    public enum WeaponSkillKeyCode
    {
        실탄_소형,
        실탄_중형,
        실탄_대형,
        레이저_소형,
        레이저_중형,
        레이저_대형,
        플라즈마_소형,
        플라즈마_중형,
        플라즈마_대형,
        입자_소형,
        입자_중형,
        입자_대형
    }

    public enum WeaponProjectileType
    {
        Physics,
        Optics,
        Particle,
        Plasma
    }
    public enum SetType
    {
        Alpha,
        Beta,
        Gamma,
        Delta,
    }
    public enum ItemType
    {
        Credit,
        SuperCredit,
        Fuel
    }
    public enum GachaItemType
    {
        Item,
        Star4Ship,
        Star5Ship
    }

    public static class EnumTextData
    {
        public static string EquipTypeText(EquipType enumData)
        {
            string text = string.Empty;
            switch (enumData)
            {
                case EquipType.Weapon:
                    text = "무기";
                    break;
                case EquipType.Armor:
                    text = "장갑";
                    break;
                case EquipType.Thruster:
                    text = "추진기";
                    break;
                case EquipType.Reactor:
                    text = "반응로";
                    break;
                case EquipType.Radiator:
                    text = "방열기";
                    break;
            }

            return text;
        }
        public static string EquipTypeText(IncreaseableStateType enumData, float value)
        {
            string text = string.Empty;
            switch (enumData)
            {
                case IncreaseableStateType.Hp:
                    text = $"체력 + {value:F0}";
                    break;
                case IncreaseableStateType.HpMultiple:
                    text = $"체력 + {value:F1}%";
                    break;
                case IncreaseableStateType.Atk:
                    text = $"공격력 + {value:F0}";
                    break;
                case IncreaseableStateType.AtkMultiple:
                    text = $"공격력 + {value:F1}%";
                    break;
                case IncreaseableStateType.Def:
                    text = $"방어력 + {value:F0}";
                    break;
                case IncreaseableStateType.DefMultiple:
                    text = $"방어력 + {value:F1}%";
                    break;
                case IncreaseableStateType.CritRate:
                    text = $"치명타 확률 + {value:F1}%";
                    break;
                case IncreaseableStateType.CritDmg:
                    text = $"치명타 피해 + {value:F1}%";
                    break;
                case IncreaseableStateType.PhysicsDmg:
                    text = $"물리 피해 증가 + {value:F1}%";
                    break;
                case IncreaseableStateType.OpticsDmg:
                    text = $"레이저 피해 증가 + {value:F1}%";
                    break;
                case IncreaseableStateType.ParticleDmg:
                    text = $"입자 피해 증가 + {value:F1}%";
                    break;
                case IncreaseableStateType.PlasmaDmg:
                    text = $"플라즈마 피해 증가 + {value:F1}%";
                    break;
            }
            return text;
        }
    }
}
