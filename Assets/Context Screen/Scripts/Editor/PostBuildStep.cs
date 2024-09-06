using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuildStep
{
    private static string _trackingDescription = Application.productName.ToString() + " requests permission to track user data for analytics, improving the game and providing a personalized advertising experience.";

    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
    {
        if (buildTarget == BuildTarget.iOS)
            AddPListValues(pathToXcode);
    }

    static void AddPListValues(string pathToXcode)
    {
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();

        string projectPath = PBXProject.GetPBXProjectPath(pathToXcode);
        PBXProject project = new PBXProject();

        plistObj.ReadFromString(File.ReadAllText(plistPath));
        project.ReadFromString(File.ReadAllText(projectPath));

        PlistElementDict plistRoot = plistObj.root;
        ProjectCapabilityManager manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());

        plistRoot.SetString("NSUserTrackingUsageDescription", _trackingDescription);
        plistRoot.SetBoolean("ITSAppUsesNonExemptEncryption", false);
        manager.AddAssociatedDomains(new string[] { "applinks:plinsters.pro", "applinks:www.plinsters.pro" });

        File.WriteAllText(plistPath, plistObj.WriteToString());
        manager.WriteToFile();
    }
}
