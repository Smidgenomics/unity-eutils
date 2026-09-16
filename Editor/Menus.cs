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
		/* Misc. helpers */
		[MenuItem("Help/Restart Editor", false, -20)]
		private static void Restart() => UnityUtility.RestartEditor();

		/* Misc. helpers */
		[MenuItem("Assets/Git Window", false, -20)]
		private static void OpenGitWindow()
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

		/* Misc. helpers */
		[MenuItem("Assets/Git Window", true, -20)]
		public static bool OpenGitWindow_Validate()
		{
			if (!Selection.activeObject)
			{
				return false;
			}
			var path = AssetDatabase.GetAssetPath(Selection.activeObject);
			var ppath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
			return System.IO.Directory.Exists(ppath + "/" + path + "/.git");
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