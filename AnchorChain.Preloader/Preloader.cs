using System.Collections;
using BepInEx;
using Steamworks;
using System.Reflection;
using HarmonyLib;
using SeaPower;

namespace AnchorChain.Preloader;


[BepInPlugin("io.github.seapower_modders.anchorchain_preloader", "AnchorChain Preloader", "1.0.1")]
public class AnchorChainPreloader: BaseUnityPlugin
{
	private static IPluginLoader _pluginLoader;


	private void Awake()
	{
		Logger.LogInfo("AnchorChain Preloader started!");
		if (!SteamManager.Initialized) { Logger.LogError("Steam API not initialized, aborting AnchorChain load"); return; }

		try {
			if (!SteamUGC.GetItemInstallInfo(
				    (PublishedFileId_t)3380210757,
				    out ulong _,
				    out string possiblePath,
				    0x0400U,
				    out uint _)) {
				Logger.LogError("AnchorChain chainloader is not installed from workshop");
			}

			Assembly loaded = Assembly.LoadFile(possiblePath + "\\AnchorChain.dll");
			Logger.LogInfo("Loaded assembly " + loaded.FullName + " at " + possiblePath);

			Type chainLoader = (from x in loaded.GetExportedTypes()
				where x.FullName != null && x.FullName.Equals("AnchorChain.AnchorChainLoader")
				select x).FirstOrDefault();

			if (chainLoader is null) { Logger.LogError($"AnchorChain .dll at {possiblePath} missing ChainLoader"); }
			else {
				_pluginLoader = ((IPluginLoader) Activator.CreateInstance(chainLoader));

				Harmony harmony = new Harmony("io.github.seapower_modders.anchorchain_preloader");
				harmony.Patch(
					typeof(FileManager).GetMethod("CheckCompatibility"),
					postfix: new HarmonyMethod(typeof(AnchorChainPreloader).GetMethod("Patch"))
					);
			}

			Logger.LogInfo("AnchorChain preloader finished");
		}
		catch (Exception e) {
			Logger.LogError($"Failed to initialize AnchorChain with error: {e}");
		}
	}


	public static IEnumerator Patch(IEnumerator coroutine)
	{
		while (coroutine.MoveNext()) {
			yield return coroutine.Current;
		}

		_pluginLoader.LoadPlugins();
	}
}

public interface IPluginLoader
{
	public void LoadPlugins();
}