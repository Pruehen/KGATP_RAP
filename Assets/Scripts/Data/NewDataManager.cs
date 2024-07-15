using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public static class NewDataManager
{

    public static int LoadedClearedStage { get; private set; }

    public static string _dataRootPath = Application.dataPath + "/Resources/Xml";


    public static void ReadClearedStage()
    {
        XDocument doc = XDocument.Load($"{_dataRootPath}/ClearedStage.xml");
        var dataElements = doc.Descendants("data");
        foreach (var data in dataElements)
        {
            LoadedClearedStage = ReadIntData(data, "StageNum");
        }
    }

    public static void ModifyClearedStage(int stageNum)
    {
        XDocument doc = XDocument.Load($"{_dataRootPath}/ClearedStage.xml");

        XElement dataElement = doc.Descendants("data")
            .FirstOrDefault(e => e.Attribute("StageNum") != null);

        if (dataElement != null)
        {
            dataElement.SetAttributeValue("StageNum", $"{stageNum}");
        }
        else
        {
            Debug.LogError("StageNum 속성을 가진 요소를 찾을 수 없음");
        }
        doc.Save($"{_dataRootPath}/ClearedStage.xml");

        LoadedClearedStage = stageNum;
    }

    public static int ReadIntData(XElement data, string columnName)
    {
        string readedData = data.Attribute(columnName).Value;
        if (readedData.Length > 0) { return int.Parse(readedData); }
        else { return 0; }
    }

}
