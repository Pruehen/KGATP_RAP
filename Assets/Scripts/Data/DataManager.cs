using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public Dictionary<int, Projectile> LoadedProjectileList { get; private set; }
    public Dictionary<int, EnemySkill> LoadedEnemySkillList { get; private set; }
    public Dictionary<int, Enemy> LoadedEnemyList { get; private set; }
    public Dictionary<int, PlayerSkill> LoadedPlayerSkillList { get; private set; }
    public Dictionary<int, PlayerCharacter> LoadedPlayerCharacterList { get; private set; }

    public int LoadedClearedStage { get; private set; }

    //�ӽ÷� ����ȭ�鿡 DataParser�� �����س��� ������ �Ľ̵�.
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
        //table���� table ���� ���� �־ ������ �߿�.
        ReadData("Projectile");
        ReadData("EnemySkill");
        ReadData("Enemy");
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
            case "Enemy":
                ReadEnemyTable(tableName);
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
        LoadedEnemyList = new Dictionary<int, Enemy>();
        XDocument doc = XDocument.Load($"{_dataRootPath}/{tableName}.xml");
        var dataElements = doc.Descendants("data");
        foreach (var data in dataElements)
        {
            var tempEnemy = new Enemy();
            tempEnemy.DataID = ReadIntData(data, "DataID");
            tempEnemy.Name = ReadStringData(data, "Name");
            tempEnemy.Type = ReadStringData(data, "Type");
            tempEnemy.HP = ReadIntData(data, "HP");
            tempEnemy.Atk = ReadIntData(data, "Atk");
            tempEnemy.Rotation_sec = ReadfloatData(data, "Rotation_sec");
            tempEnemy.Rotation_angle = ReadfloatData(data, "Rotation_angle");
            tempEnemy.Skill = LoadedEnemySkillList[ReadIntData(data, "Skill")];

            LoadedEnemyList.Add(tempEnemy.DataID, tempEnemy);
        }
    }

    public  void ReadClearedStage()
    {
        XDocument doc = XDocument.Load($"{_dataRootPath}/ClearedStage.xml");
        var dataElements = doc.Descendants("data");
        foreach(var data in dataElements)
        {
            LoadedClearedStage = ReadIntData(data, "StageNum"); 
        }
    }

    public void ModifyClearedStage(int stageNum)
    {
        XDocument doc = XDocument.Load($"{_dataRootPath}/ClearedStage.xml");

        XElement dataElement = doc.Descendants("data")
            .FirstOrDefault(e => e.Attribute("StageNum") != null);

        if(dataElement != null)
        {
            dataElement.SetAttributeValue("StageNum", $"{stageNum}");
        }
        else
        {
            Debug.LogError("StageNum �Ӽ��� ���� ��Ҹ� ã�� �� ����");
        }
        doc.Save($"{_dataRootPath}/ClearedStage.xml");

        LoadedClearedStage = stageNum;
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
        else { Debug.LogError($"wrong table bool�� at {columnName}"); return false; }
    }

    private List<int> ReadMultipleData(XElement data, string columnName)
    {
        //�������� ������ ��� �����ʹ� , �� ���� �Ǿ����. (����� �ص� �������)
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
        ReadClearedStage();
        //Debug.Log($"{LoadedClearedStage}");

        ModifyClearedStage(2);
        //Debug.Log($"{LoadedClearedStage}");

    }
}
