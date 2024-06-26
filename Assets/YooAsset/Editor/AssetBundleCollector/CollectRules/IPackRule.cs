﻿
namespace YooAsset.Editor
{
	public struct PackRuleData
	{
		public string AssetPath;
		public string CollectPath;
		public string GroupName;
		public string UserData;

		public PackRuleData(string assetPath, string collectPath, string groupName, string userData)
		{
			AssetPath = assetPath;
			CollectPath = collectPath;
			GroupName = groupName;
			UserData = userData;
		}
	}

	public struct PackRuleResult
	{
		private readonly string _bundleName;
		private readonly string _bundleExtension;

		public PackRuleResult(string bundleName, string bundleExtension)
		{
			_bundleName = bundleName;
			_bundleExtension = bundleExtension;
		}

		/// <summary>
		/// 获取主资源包全名称
		/// </summary>
		public string GetMainBundleName(string packageName, bool uniqueBundleName)
		{
			string fullName;
			string bundleName = EditorTools.GetRegularPath(_bundleName).Replace('/', '_').Replace('.', '_').Replace(" ", "_").ToLower();
			if (uniqueBundleName)
				fullName = $"{packageName}_{bundleName}.{_bundleExtension}";
			else
				fullName = $"{bundleName}.{_bundleExtension}";
			return fullName.ToLower();
		}

		/// <summary>
		/// 获取共享资源包全名称
		/// </summary>
		public string GetShareBundleName(string packageName, bool uniqueBundleName)
		{
			// 注意：冗余的共享资源包名返回空
			if (string.IsNullOrEmpty(_bundleName) && string.IsNullOrEmpty(_bundleExtension))
				return string.Empty;

			string fullName;
			string bundleName = EditorTools.GetRegularPath(_bundleName).Replace('/', '_').Replace('.', '_').Replace(" ", "_").ToLower();
			if (uniqueBundleName)
				fullName = $"{packageName}_share_{bundleName}.{_bundleExtension}";
			else
				fullName = $"share_{bundleName}.{_bundleExtension}";
			return fullName.ToLower();
		}
	}

	/// <summary>
	/// 资源打包规则接口
	/// </summary>
	public interface IPackRule
	{
		/// <summary>
		/// 获取打包规则结果
		/// </summary>
		PackRuleResult GetPackRuleResult(PackRuleData data);

		/// <summary>
		/// 是否为原生文件打包规则
		/// </summary>
		bool IsRawFilePackRule();
	}
}