using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public Dictionary<int, EnemySkill> LoadedEnemySkillList { get; private set; }
    public Dictionary<int, Projectile> LoadedProjectileList { get; private set; }
    public Dictionary<int, PlayerSkill> LoadedPlayerSkillList { get; private set; }
    public Dictionary<int, PlayerCharacter> LoadedPlayerCharacterList { get; private set; }

    //임시로 바탕화면에 DataParser를 복사해놔야 데이터 파싱됨.
    private string _dataRootPath;


    private void Awake()
    {
        _dataRootPath = Application.dataPath + "/Resources/Xml";

        Instance = this;
        ReadAllDataOnAwake();
    }

    private void Start()
    {
        tempParsingTest();
    }

    private void ReadAllDataOnAwake()
    {
        //table에서 table 읽을 수도 있어서 순서가 중요.
        ReadData("Projectile");
        ReadData("EnemySkill");
        ReadData("PlayerSkill");
        ReadData("PlayerCharacter");
    }

    private void ReadData(string tableName)
    {
        switch (tableName)
        {
            case "PlayerCharacter":
                ReadPlayerCharacterTable(tableName);
                break;
            case "PlayerSkill":
                ReadPlayerSkillTable(tableName);
                break;
            case "Projectile":
                ReadProjectileTable(tableName);
                break;
            case "EnemySkill":
                ReadEnemySkillTable(tableName);
                break;
        }
    }

    private void ReadPlayerCharacterTable(string tableName)
    {
        LoadedPlayerCharacterList = new Dictionary<int, PlayerCharacter>();

        XDocument doc = XDocument.Load($"{_dataRootPath}/{tableName}.xml");
        var dataElements = doc.Descendants("data");

        foreach (var data in dataElements)
        {
            var tempPlayerCharacter = new PlayerCharacter();
            tempPlayerCharacter.DataID = int.Parse(data.Attribute("DataID").Value);
            tempPlayerCharacter.Name = data.Attribute("Name").Value;
            tempPlayerCharacter.HP = int.Parse(data.Attribute("HP").Value);
            tempPlayerCharacter.Atk = int.Parse(data.Attribute("Atk").Value);
            tempPlayerCharacter.Atk_base = this.LoadedPlayerSkillList[int.Parse(data.Attribute("Atk_base").Value)];
            tempPlayerCharacter.Atk_strong = this.LoadedPlayerSkillList[int.Parse(data.Attribute("Atk_strong").Value)];
            tempPlayerCharacter.Evasion = this.LoadedPlayerSkillList[int.Parse(data.Attribute("Evasion").Value)];
            tempPlayerCharacter.Atk_special = this.LoadedPlayerSkillList[int.Parse(data.Attribute("Atk_special").Value)];
            tempPlayerCharacter.Cost_type = int.Parse(data.Attribute("Cost_type").Value);

            LoadedPlayerCharacterList.Add(tempPlayerCharacter.DataID, tempPlayerCharacter);
        }
    }

    private void ReadPlayerSkillTable(string tableName)
    {
        LoadedPlayerSkillList = new Dictionary<int, PlayerSkill>();

        XDocument doc = XDocument.Load($"{_dataRootPath}/{tableName}.xml");
        var dataElements = doc.Descendants("data");

        foreach ( var data in dataElements)
        {
            var tempPlayerSkill = new PlayerSkill();
            tempPlayerSkill.DataID = ReadIntData(data, "DataID");
            tempPlayerSkill.Name = data.Attribute("Name").Value;
            tempPlayerSkill.Cost_value = ReadIntData(data, "Cost_value");
            tempPlayerSkill.Cost_recovery = ReadIntData(data, "Cost_recovery");
            tempPlayerSkill.Atk_multiply = ReadfloatData(data, "Atk_multiply");
            tempPlayerSkill.Cooltime = ReadfloatData(data, "Cooltime");
            tempPlayerSkill.Buff = ReadIntData(data, "Buff");
            tempPlayerSkill.Debuff = ReadMultipleData(data, "Debuff");
            tempPlayerSkill.Melee_angle = ReadIntData(data, "Melee_angle");
            tempPlayerSkill.Combo_ID = ReadIntData(data, "Combo_ID");

            LoadedPlayerSkillList.Add(tempPlayerSkill.DataID, tempPlayerSkill);
        }
    }

    private void ReadProjectileTable(string tableName)
    {
        LoadedProjectileList = new Dictionary<int, Projectile>();

        XDocument doc = XDocument.Load($"{_dataRootPath}/{tableName}.xml");
        var dataElements = doc.Descendants("data");
        foreach ( var data in dataElements )
        {
            var tempProjectile = new Projectile();
            tempProjectile.DataID = ReadIntData(data, "DataID");
            tempProjectile.Name = data.Attribute("Name").Value;
            tempProjectile.Size_min = ReadfloatData(data, "Size_min");
            tempProjectile.Size_increase = ReadfloatData(data, "Size_increase");
            tempProjectile.Size_max = ReadfloatData(data, "Size_max");
            tempProjectile.Atk_multiply = ReadfloatData(data, "Atk_multiply");
            tempProjectile.Force = ReadfloatData(data, "Force");
            tempProjectile.Lifetime = ReadfloatData(data, "Lifetime");
            tempProjectile.Collision_able = ReadStringData(data, "Collision_able");
            tempProjectile.Disappear_condition = ReadStringData(data, "Disappear_condition");
            tempProjectile.Parry_able = ReadboolData(data, "Parry_able");
            tempProjectile.Bounce_num = ReadIntData(data, "Bounce_num");
            tempProjectile.Combo_ID = ReadStringData(data, "Combo_ID");

            LoadedProjectileList.Add(tempProjectile.DataID, tempProjectile);
        }
    }

    private void ReadEnemySkillTable(string tableName)
    {
        LoadedEnemySkillList = new Dictionary<int, EnemySkill>();
        XDocument doc = XDocument.Load($"{_dataRootPath}/{tableName}.xml");
        var dataElements = doc.Descendants("data");
        foreach (var data in dataElements)
        {
            var tempEnemySkill = new EnemySkill();
            tempEnemySkill.DataID = ReadIntData(data, "DataID");
            tempEnemySkill.Name = ReadStringData(data, "Name");
            tempEnemySkill.Cooltime = ReadfloatData(data, "Cooltime");
            tempEnemySkill.Buff = ReadIntData(data, "Buff");
            tempEnemySkill.Debuff = ReadIntData(data, "Debuff");
            tempEnemySkill.Missle_angle = ReadfloatData(data, "Missle_angle");
            tempEnemySkill.Missle_ea = ReadIntData(data, "Missle_ea");
            tempEnemySkill.Missle_ID = ReadIntData(data, "Missle_ID");
            tempEnemySkill.Combo_ID = ReadIntData(data, "Combo_ID");

            LoadedEnemySkillList.Add(tempEnemySkill.DataID, tempEnemySkill);
        }
    }

    private void ReadEnemyTable(string tableName) 
    {
        
    }

    private string ReadStringData(XElement data, string columnName)
    {
        return data.Attribute(columnName).Value;
    }
    private int ReadIntData(XElement data, string columnName)
    {
        string readedData = data.Attribute(columnName).Value;
        if (readedData.Length > 0) { return int.Parse(readedData); }
        else { return 0; }
    }

    private float ReadfloatData(XElement data, string columnName) 
    {
        string readedData = data.Attribute(columnName).Value;
        if (readedData.Length > 0) { return float.Parse(readedData); }
        else { return 0; }
    }

    private bool ReadboolData(XElement data, string columnName)
    {
        string readedData = data.Attribute(columnName).Value;
        readedData = readedData.ToLower();
        if (readedData == "true") { return true; }
        else if(readedData == "false") { return false; }
        else { Debug.LogError($"wrong table bool값 at {columnName}"); return false; }
    }

    private List<int> ReadMultipleData(XElement data, string columnName)
    {
        //여러개의 정보가 담긴 데이터는 , 로 구분 되어야함. (띄어쓰기는 해도 상관없음)
        string multipleData = data.Attribute(columnName).Value;
        multipleData = multipleData.Replace(" ", "");
        var dataList = multipleData.Split(',');
        var list = new List<int>();
        if(dataList.Length > 0)
        {
            foreach (string item in dataList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    list.Add(int.Parse(item));
                }
            }
        }
        return list;
    }

    private void tempParsingTest()
    {

        foreach (var skill in LoadedEnemySkillList)
        {
            Debug.Log($"{skill.Value.Name}\n" +
                $"{skill.Value.DataID}\n" +
                $"{skill.Value.Missle_ID}");
        }
    }
}
