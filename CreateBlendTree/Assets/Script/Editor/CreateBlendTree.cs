using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// BlendTreeファイルを作成する
/// </summary>
public class CreateBlendTree : MonoBehaviour
{
    static string _defaultFileName = "BlendTree";
    static string _extension = ".asset";

    [MenuItem("Assets/Create/BlendTree", false)]
    static void Execute()
    {
        //選択フォルダに同名ファイルがあるかチェックして、ある場合はナンバリングする
        var selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        var fileName = _defaultFileName + _extension;
        var path = selectedPath + "/" + fileName;
        int cnt = 0;
        while (File.Exists(path))
        {
            if (path.Contains(fileName))
            {
                cnt++;
                var newFileName = _defaultFileName + cnt + _extension;
                path = path.Replace(fileName, newFileName);
                fileName = newFileName;
            }
            else
            {
                break;
            }
        }

        //空のBlendTreeを保存する
        UnityEditor.Animations.BlendTree blendTree = new UnityEditor.Animations.BlendTree();
        AssetDatabase.CreateAsset(blendTree, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

