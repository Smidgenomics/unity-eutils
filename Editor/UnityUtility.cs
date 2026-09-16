// smidgens @ github

namespace Smidgenomics.Unity.EUtils.Editor
{
	using System;
	using System.Diagnostics;
	using UnityEditor;
	using UnityEngine;
	using System.IO;

	// misc unity editor utils
	internal static class UnityUtility
	{
		public static void RestartEditor()
		{
			var projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
			EditorApplication.OpenProject(projectPath);
		}

		public static class AssetInfo
		{
			public static Type TypeAtPath(in string path)
			{
				return AssetDatabase.GetMainAssetTypeAtPath(path);
			}

			public static string GetPath(in string guid) => AssetDatabase.GUIDToAssetPath(guid);
		}


	}
}