using UnityEngine;
using UnityEditor;


[InitializeOnLoad]
public class Startup
{
    static Startup()
    {
        Debug.Log("Lisään projektiin tagit: OVastaus,VVastaus,Vastattu,Oikea,Kysymys");
        TagHelper.AddTag("OVastaus");
        TagHelper.AddTag("VVastaus");
        TagHelper.AddTag("Vastattu");
        TagHelper.AddTag("Oikea");
        TagHelper.AddTag("Kysymys");
    }
}
public static class TagHelper
{
    public static void AddTag(string tag)
    {
        UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                {

                    return;     // Tag already present, nothing to do.
                }
            }

            tags.InsertArrayElementAtIndex(0);
            tags.GetArrayElementAtIndex(0).stringValue = tag;
            so.ApplyModifiedProperties();
            so.Update();
        }
    }
}


