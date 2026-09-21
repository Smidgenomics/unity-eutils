// smidgens @ github

namespace Smidgenomics.Unity.EUtils.Editor
{
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// [MenuItem]
	/// </summary>
	internal static class Menus
	{
		// re-open project
		[MenuItem("Help/Restart Editor", false, -20)]
		private static void Restart() => UnityUtility.RestartEditor();
		
		// sometimes for whatever reason the scene camera starts slanting
		[MenuItem("Help/Fix Tilted Scene Camera", false, 100)]
		private static void FixTiltedCamera()
		{
			if (!SceneView.lastActiveSceneView)
			{
				return;
			}
			var sv = SceneView.lastActiveSceneView;
			var rot = sv.rotation.eulerAngles;
			rot.z = 0f; // removing tilt
			sv.rotation = Quaternion.Euler(rot);
		}

		// open folder in terminal
		[MenuItem("Assets/Git Terminal", false, -20)]
		private static void OpenGitTerminal()
		{
			var path = AssetDatabase.GetAssetPath(Selection.activeObject);
			var ppath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
			var fullpath = ppath + "/" + path;
			var startInfo = new System.Diagnostics.ProcessStartInfo
			{
				WorkingDirectory = fullpath,
				FileName = "cmd"
			};
			System.Diagnostics.Process.Start(startInfo);
		}

		// check if active folder is git repo
		[MenuItem("Assets/Git Terminal", true, -20)]
		private static bool OpenGitTerminal_Validate()
		{
			if (!Selection.activeObject)
			{
				return false;
			}
			var path = AssetDatabase.GetAssetPath(Selection.activeObject);
			var ppath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);

			var gitPath = ppath + "/" + path + "/.git";

			return System.IO.Directory.Exists(gitPath)
			|| System.IO.File.Exists(gitPath);
		}

		// copy guid of selected asset
		[MenuItem("Assets/Copy GUID", false)]
		private static void MI_CopyGUID() => GUIUtility.systemCopyBuffer =
			AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));

		// Only allow option if asset is selected
		[MenuItem("Assets/Copy GUID", true)]
		private static bool MI_CopyGUID_Validate() => IsSelectionAsset();

		private static bool IsSelectionAsset()
		{
			return Selection.activeObject && EditorUtility.IsPersistent(Selection.activeObject);
		}

		private static Object GetAssetFromGUID(string guid)
		{
			return AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid));
		}

	}

}