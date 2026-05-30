using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static Codice.Client.BaseCommands.BranchExplorer.ExplorerData.BrExTreeBuilder.BrExFilter;

public class CardGenerator : Editor
{
    [MenuItem("Tools/Generate Card")]
    public static void GenerateAllCards()
    {
        string savePath = "Assets/CardData";
        if(!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

        string[] searchDirs = new string[] { "Assets/2D Cards Game Art Pack/Sprites/Standard 52 Cards" };

        foreach(CardShape shape in System.Enum.GetValues(typeof(CardShape)))
        {
            for(int i = 1; i<= 13; i++)
            {
                if (i == 1) CreateCardAsset(savePath, shape, "A", i, searchDirs);
                else if(i == 11) CreateCardAsset(savePath, shape, "J", 10, searchDirs);
                else if (i == 12) CreateCardAsset(savePath, shape, "Q", 10, searchDirs);
                else if (i == 13) CreateCardAsset(savePath, shape, "K", 10, searchDirs);
                else CreateCardAsset(savePath, shape, i.ToString(), i, searchDirs);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Jobs done perfectly");
    }

    private static void CreateCardAsset(string savePath, CardShape shape, string numberAsString, int number, string[] searchDirs)
    {
        string fileName = $"{shape}_{numberAsString:00}.asset";
        string fullPath = $"{savePath}/{fileName}";

        if (AssetDatabase.LoadAssetAtPath<CardSO>(fullPath) != null) return;

        string spriteName = $"card{shape.ToString()}s_{numberAsString}";

        CardSO newCard = ScriptableObject.CreateInstance<CardSO>();

        string[] guids = AssetDatabase.FindAssets($"{spriteName} t:Sprite", searchDirs);

        Sprite foundSprite = null;

        if (guids.Length > 0)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

            foundSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Something Wrong");
        }

        newCard.Initialize(foundSprite, shape, number, numberAsString);

        AssetDatabase.CreateAsset(newCard, fullPath);
    }

    /*private static void CreateCardAsset(string savePath, CardShape shape, string number, string[] searchDirs)
    {
        string fileName = $"{shape}_{number:00}.asset";
        string fullPath = $"{savePath}/{fileName}";

        if (AssetDatabase.LoadAssetAtPath<CardSO>(fullPath) != null) return;

        CardSO newCard = ScriptableObject.CreateInstance<CardSO>();
        newCard.cardShape = shape;
        newCard.cardNumberAsString = number;
        
        if(Int32.TryParse(number, out int cardNumber))
        {
            newCard.cardNumber = cardNumber;
        }
        else
        {
            switch(number)
            {
                case "A":
                    newCard.cardNumber = 1;
                    break;

                default:
                    newCard.cardNumber = 10;
                    break;
            }
        }

        string spriteName = $"card{shape.ToString()}s_{number}";

        string[] guids = AssetDatabase.FindAssets($"{spriteName} t:Sprite", searchDirs);

        if (guids.Length > 0)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);

            Sprite foundSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            newCard.cardImage = foundSprite;

            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Something Wrong");
        }

        AssetDatabase.CreateAsset(newCard, fullPath);
    }*/
}
