using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class ChipGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Chips")]
    public static void GenerateAllChips()
    {
        string targetPath = "Assets/SO/ChipData";
        if(!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }

        var chipTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(ChipSO)) && !t.IsAbstract);

        int count = 0;

        foreach(var type in chipTypes)
        {
            string assetName = $"{type.Name}.asset";
            string fullPath = Path.Combine(targetPath, assetName);

            ChipSO chip = AssetDatabase.LoadAssetAtPath(fullPath, type) as ChipSO;

            if(chip == null)
            {
                chip = ScriptableObject.CreateInstance(type) as ChipSO;
                AssetDatabase.CreateAsset(chip, fullPath);
            }

            if(chip.chipImage == null)
            {
                string[] searchDirs = new string[] { "Assets/Sprites/Chip" };
                string[] guids = AssetDatabase.FindAssets($"{type.Name} t:Sprite", searchDirs);

                Sprite foundSprite = null;

                if (guids.Length > 0)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

                    foundSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

                    chip.Initialize(foundSprite, type.Name);

                    Debug.Log("Success");
                }
                else
                {
                    chip.Initialize(type.Name);

                    Debug.Log("Something Wrong");
                }

                EditorUtility.SetDirty(chip);

                Debug.Log($"[ChipCreator] Created: {assetName}");
                count++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", $"{count}개의 새로운 Chip 에셋이 생성되었습니다!", "확인");
    }
}
