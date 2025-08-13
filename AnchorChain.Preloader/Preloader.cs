using BepInEx;
using Steamworks;
using System.Reflection;

namespace AnchorChain.Preloader;


[BepInPlugin("io.github.seapower_modders.anchorchain_preloader", "AnchorChain Preloader", "1.0.1")]
public class AnchorChainPreloader: BaseUnityPlugin
{
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
			else { ((IPluginLoader) Activator.CreateInstance(chainLoader)).LoadPlugins(); }

			Logger.LogInfo("AnchorChain preloader finished");
		}
		catch (Exception e) {
			Logger.LogError($"Failed to initialize AnchorChain with error: {e}");
		}
	}
}

public interface IPluginLoader
{
	public void LoadPlugins();
}