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
    public TMP_Text errorMessageText;

    void Start()
    {
        ListFiles();
    }

    void ListFiles()
    {
        string[] fbxFiles = Directory.GetFiles(targetDirectory, "*.glb");
        foreach (string file in fbxFiles)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
            buttonObj.GetComponentInChildren<TMP_Text>().text = Path.GetFileName(file);
            buttonObj.GetComponent<Button>().onClick.AddListener(() => LoadGLB(file));
        }
    }
    void LoadGLB(string filePath)
    {
        try
        {
            var gltf = gameObject.AddComponent<GLTFast.GltfAsset>();
            gltf.Url = filePath;
            errorMessageText.text = "File Loaded:" + filePath;         
        }
        catch (Exception e)
        {
            Debug.LogError("Error while loading GLB: " + e.Message);
            errorMessageText.text = "Error: " + e.Message;         
        }
    }
}
