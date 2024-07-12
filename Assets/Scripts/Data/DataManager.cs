using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public Dictionary<int, PlayerCharacter> LoadedPlayerCharacterList { get; private set; }
    public Dictionary<int, PlayerSkill> LoadedPlayerSkillList { get; private set; }
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
        ReadData("PlayerCharacter");
        ReadData("PlayerSkill");
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
            tempPlayerCharacter.Atk_base = int.Parse(data.Attribute("Atk_base").Value);
            tempPlayerCharacter.Evasion = int.Parse(data.Attribute("Evasion").Value);
            tempPlayerCharacter.Atk_special = int.Parse(data.Attribute("Atk_special").Value);
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
        Debug.Log($"{this.LoadedPlayerCharacterList[101].DataID}\n" +
            $"{this.LoadedPlayerCharacterList[101].Name}\n" +
            $"{this.LoadedPlayerCharacterList[101].HP}\n" +
            $"{this.LoadedPlayerCharacterList[101].Atk}\n" +
            $"{this.LoadedPlayerCharacterList[101].Atk_base}\n" +
            $"{this.LoadedPlayerCharacterList[101].Evasion}\n" +
            $"{this.LoadedPlayerCharacterList[101].Atk_special}\n" +
            $"{this.LoadedPlayerCharacterList[101].Cost_type}");

        foreach(var skill in LoadedPlayerSkillList)
        {
            Debug.Log($"{skill.Value.DataID}\n" +
                $"{skill.Value.Name}\n" +
                $"{skill.Value.Atk_multiply}\n");
            foreach(var id in skill.Value.Debuff)
            {
                Debug.Log($"{id}");
            }
        }
        Debug.Log("?");
    }
}
