using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class LoadAsset : MonoBehaviour
{
    private string targetDirectory = Path.Combine(Application.dataPath, "_output");
    public GameObject buttonPrefab;
    public Transform contentPanel;
    public TMP_Text logText;
    public TMP_InputField directoryInputField;

    void Start()
    {
        ListFiles();
    }
    public void ApplyDirectorySetting()
    {
        targetDirectory = directoryInputField.text;
        ListFiles();
    }

    public void ListFiles()
    {
        try
        {
            string[] fbxFiles = Directory.GetFiles(targetDirectory, "*.glb");
            foreach (string file in fbxFiles)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
                buttonObj.GetComponentInChildren<TMP_Text>().text = Path.GetFileName(file);
                buttonObj.GetComponent<Button>().onClick.AddListener(() => LoadGLB(file));
            }
        }
        catch (DirectoryNotFoundException)
        {
            Debug.LogError($"Directory not found: {targetDirectory}");
            logText.text = $"Error: Directory not found ({targetDirectory})";
        }
        catch (ArgumentException)
        {
            Debug.LogError($"Invalid path: {targetDirectory}");
            logText.text = $"Error: Invalid path ({targetDirectory})";
        }
        catch (Exception e)
        {
            Debug.LogError($"Error while listing files: {e.Message}");
            logText.text = $"Error: {e.Message}";
        }
    }


    void LoadGLB(string filePath)
    {
        try
        {
            var gltf = gameObject.AddComponent<GLTFast.GltfAsset>();
            gltf.Url = filePath;
            logText.text = "File Loaded:" + filePath;
        }
        catch (Exception e)
        {
            Debug.LogError("Error while loading GLB: " + e.Message);
            logText.text = "Error: " + e.Message;
        }
    }
}
