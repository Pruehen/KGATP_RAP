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
        ��ź_����,
        ��ź_����,
        ��ź_����,
        ������_����,
        ������_����,
        ������_����,
        �ö��_����,
        �ö��_����,
        �ö��_����,
        ����_����,
        ����_����,
        ����_����
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
                    text = "����";
                    break;
                case EquipType.Armor:
                    text = "�尩";
                    break;
                case EquipType.Thruster:
                    text = "������";
                    break;
                case EquipType.Reactor:
                    text = "������";
                    break;
                case EquipType.Radiator:
                    text = "�濭��";
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
                    text = $"ü�� + {value:F0}";
                    break;
                case IncreaseableStateType.HpMultiple:
                    text = $"ü�� + {value:F1}%";
                    break;
                case IncreaseableStateType.Atk:
                    text = $"���ݷ� + {value:F0}";
                    break;
                case IncreaseableStateType.AtkMultiple:
                    text = $"���ݷ� + {value:F1}%";
                    break;
                case IncreaseableStateType.Def:
                    text = $"���� + {value:F0}";
                    break;
                case IncreaseableStateType.DefMultiple:
                    text = $"���� + {value:F1}%";
                    break;
                case IncreaseableStateType.CritRate:
                    text = $"ġ��Ÿ Ȯ�� + {value:F1}%";
                    break;
                case IncreaseableStateType.CritDmg:
                    text = $"ġ��Ÿ ���� + {value:F1}%";
                    break;
                case IncreaseableStateType.PhysicsDmg:
                    text = $"���� ���� ���� + {value:F1}%";
                    break;
                case IncreaseableStateType.OpticsDmg:
                    text = $"������ ���� ���� + {value:F1}%";
                    break;
                case IncreaseableStateType.ParticleDmg:
                    text = $"���� ���� ���� + {value:F1}%";
                    break;
                case IncreaseableStateType.PlasmaDmg:
                    text = $"�ö�� ���� ���� + {value:F1}%";
                    break;
            }
            return text;
        }
    }
}
