// Decompiled with JetBrains decompiler
// Type: ThunderMine.InfoManager
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ThunderMine
{
  internal class InfoManager
  {
    private System.Threading.Timer timer;
    private string lastGateway;

    public InfoManager() => this.lastGateway = this.GetGatewayMAC();

    public static IPAddress GetDefaultGateway() => ((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces()).Where<NetworkInterface>((Func<NetworkInterface, bool>) (n => n.OperationalStatus == OperationalStatus.Up)).Where<NetworkInterface>((Func<NetworkInterface, bool>) (n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)).SelectMany<NetworkInterface, GatewayIPAddressInformation>((Func<NetworkInterface, IEnumerable<GatewayIPAddressInformation>>) (n =>
    {
      IPInterfaceProperties ipProperties = n.GetIPProperties();
      return ipProperties == null ? (IEnumerable<GatewayIPAddressInformation>) null : (IEnumerable<GatewayIPAddressInformation>) ipProperties.GatewayAddresses;
    })).Select<GatewayIPAddressInformation, IPAddress>((Func<GatewayIPAddressInformation, IPAddress>) (g => g?.Address)).Where<IPAddress>((Func<IPAddress, bool>) (a => a != null)).FirstOrDefault<IPAddress>();

    private string GetArpTable()
    {
      string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
      using (Process process = Process.Start(new ProcessStartInfo()
      {
        FileName = pathRoot + "Windows\\System32\\arp.exe",
        Arguments = "-a",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        CreateNoWindow = true
      }))
      {
        using (StreamReader standardOutput = process.StandardOutput)
          return standardOutput.ReadToEnd();
      }
    }

    private string GetGatewayMAC() => new Regex(string.Format("({0} [\\W]*) ([a-z0-9-]*)", (object) InfoManager.GetDefaultGateway().ToString())).Match(this.GetArpTable()).Groups[2].ToString();
  }
}
