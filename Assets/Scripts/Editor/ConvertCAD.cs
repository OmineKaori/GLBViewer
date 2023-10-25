using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Scripting.Python;
using Unity.Plastic.Newtonsoft.Json;
using System.IO;

public class ConvertCAD : EditorWindow
{
    [MenuItem("Pixyz/Import CAD")]
    static void RunPython()
    {
        string path = EditorUtility.OpenFilePanel("Select your CAD fiel", Application.dataPath, "CATPart");
        if (path.Length != 0)
        {
            var data = new { input_file = path};
            string jsonData = JsonConvert.SerializeObject(data);
            string jsonFilePath = Path.Combine(Application.dataPath, "Pixyz/input_file.json");
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("Read input file: " + path);

            PythonRunner.RunFile($"{Application.dataPath}/Pixyz/scripts/watcher.py");
        }
    }
}
