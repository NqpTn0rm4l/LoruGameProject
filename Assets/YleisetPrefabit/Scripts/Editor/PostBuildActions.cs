
    using System;
     using System.IO;
     using System.Text.RegularExpressions;
     using UnityEditor;
     using UnityEditor.Callbacks;
     
     public class PostBuildActions
{
#if UNITY_WEBGL
    //[PostProcessBuildAttribute(1)]
    //public static void OnPostProcessBuild(BuildTarget target, string targetPath)
    //{
    //    var path = Path.Combine(targetPath, "Build/UnityLoader.js");
    //    var text = File.ReadAllText(path);
    //    text = text.Replace("UnityLoader.SystemInfo.mobile", "false");
    //    File.WriteAllText(path, text);
    //}
#endif
}



