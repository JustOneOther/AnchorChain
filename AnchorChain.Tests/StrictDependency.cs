﻿// Comment or uncomment following line to activate SD tests
#define SD
#if SD

using UnityEngine;


namespace AnchorChain.Tests;


/// <summary>
/// Info plugin for test group
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSDInfo", "SD Info", "1.0")]
public class StrictDependencyInfo : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogWarning("SD 4, SD 5, SD 6, SD 7, SD 10, and SD 11 should load");
	}
}

/// <summary>
/// Non-existent dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD1", "SD 1", "1.0")]
[ACDependency("io.github.seapower-modders.does-not-exist", "1.0", "1.0")]
public class Missing : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogError("SD 1 Loaded");
	}
}

/// <summary>
/// Under-versioned dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD2", "MD 2", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", "1.1", "1.2")]
public class StrictDependency2 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogError("SD 2 Loaded");
	}
}

/// <summary>
/// Over-versioned dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD3", "SD 3", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", "0.9.0", "0.9.9")]
public class StrictDependency3 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogError("SD 3 Loaded");
	}
}

/// <summary>
/// THE dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD4", "SD 4", "1.0")]
public class StrictDependency4 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 4 Loaded");
	}
}

/// <summary>
/// Open max dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD5", "SD 5", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", "1.0.0", null)]
public class StrictDependency5 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 5 Loaded");
	}
}

/// <summary>
/// Open min dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD6", "SD 6", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", null, "1.0.0")]
public class StrictDependency6 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 6 Loaded");
	}
}

/// <summary>
/// Open version dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD7", "SD 7", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", null, null)]
public class StrictDependency7 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 7 Loaded");
	}
}

/// <summary>
/// Under-versioned open max dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD8", "SD 8", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", "1.1.0", null)]
public class StrictDependency8 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogError("SD 8 Loaded");
	}
}

/// <summary>
/// Over-versioned open min dependency
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD9", "SD 9", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD4", null, "0.9.9")]
public class StrictDependency9 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.LogError("SD 9 Loaded");
	}
}

/// <summary>
/// Circular dependency 1
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD10", "SD 10", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD11", null, null)]
public class StrictDependency10 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 10 Loaded");
	}
}

/// <summary>
/// Circular dependency 2
/// </summary>
[ACPlugin("io.github.seapower-modders.AnchorChainSD11", "SD 11", "1.0")]
[ACDependency("io.github.seapower-modders.AnchorChainSD10", null, null)]
public class StrictDependency11 : IModInterface
{
	public void TriggerEntryPoint()
	{
		Debug.Log("SD 11 Loaded");
	}
}

#endif