// Decompiled with JetBrains decompiler
// Type: ThunderMine.Security
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace ThunderMine
{
  internal class Security
  {
    private const string _key = "3082010A0282010100D0A2FCAC2861DF72F05EE166613656F27D3C037B985FECFCB5D943BC28B40DD9C035FFE44E16C57772312A9457E54973E15D40DF91660E2914ACE0AC3705562F32F023EBF32BC218423AE9DA1C752FD843EC0176307E1EE97EFCA50510DBBC88C4A253A9A06C7646BFB30CE86B773708D4240AB72919898387C60FB2F0B1B4E579BB5BC9DA286C348DD81A1205C1C43BF522032C0CA4226E08C2108E847670363B292E8E90D8B541C03CB11B03A13A88BCCC209D899994F8EADDF43AE8BBE63214EC4817922EC9496855D47E00CA21B533950C5401C6E31A727BC1A14F025D7F94B3DB2D4EE749B05C83A68A3EB17A4E375CD5CE16904F0CB1F8B7B8E75A86D30203010001";

    public static string Signature(string value)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "");
      }
    }

    private static string Session(int length)
    {
      Random random = new Random();
      return new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length).Select<string, char>((Func<string, char>) (s => s[random.Next(s.Length)])).ToArray<char>());
    }

    public static string Obfuscate(int length)
    {
      Random random = new Random();
      return new string(Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length).Select<string, char>((Func<string, char>) (s => s[random.Next(s.Length)])).ToArray<char>());
    }

    public static void Start()
    {
      string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
      if (Constants.Started)
      {
        int num = (int) MessageBox.Show("A session has already been started, please end the previous one!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Process.GetCurrentProcess().Kill();
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(pathRoot + "Windows\\System32\\drivers\\etc\\hosts"))
        {
          if (streamReader.ReadToEnd().Contains("api.auth.gg"))
          {
            Constants.Breached = true;
            int num = (int) MessageBox.Show("DNS redirecting has been detected!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
            Process.GetCurrentProcess().Kill();
          }
        }
        InfoManager infoManager = new InfoManager();
        Constants.Token = Guid.NewGuid().ToString();
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ThunderMine.Security.PinPublicKey);
        Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(ThunderMine.Security.Session(32)));
        Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(ThunderMine.Security.Session(16)));
        Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
        Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
        Constants.Started = true;
      }
    }

    public static void End()
    {
      if (!Constants.Started)
      {
        int num = (int) MessageBox.Show("No session has been started, closing for security reasons!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Process.GetCurrentProcess().Kill();
      }
      else
      {
        Constants.Token = (string) null;
        ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((_param1, _param2, _param3, _param4) => true);
        Constants.APIENCRYPTKEY = (string) null;
        Constants.APIENCRYPTSALT = (string) null;
        Constants.IV = (string) null;
        Constants.Key = (string) null;
        Constants.Started = false;
      }
    }

    private static bool PinPublicKey(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      return certificate != null && certificate.GetPublicKeyString() == "3082010A0282010100D0A2FCAC2861DF72F05EE166613656F27D3C037B985FECFCB5D943BC28B40DD9C035FFE44E16C57772312A9457E54973E15D40DF91660E2914ACE0AC3705562F32F023EBF32BC218423AE9DA1C752FD843EC0176307E1EE97EFCA50510DBBC88C4A253A9A06C7646BFB30CE86B773708D4240AB72919898387C60FB2F0B1B4E579BB5BC9DA286C348DD81A1205C1C43BF522032C0CA4226E08C2108E847670363B292E8E90D8B541C03CB11B03A13A88BCCC209D899994F8EADDF43AE8BBE63214EC4817922EC9496855D47E00CA21B533950C5401C6E31A727BC1A14F025D7F94B3DB2D4EE749B05C83A68A3EB17A4E375CD5CE16904F0CB1F8B7B8E75A86D30203010001";
    }

    public static string Integrity(string filename)
    {
      using (MD5 md5 = MD5.Create())
      {
        using (FileStream inputStream = System.IO.File.OpenRead(filename))
          return BitConverter.ToString(md5.ComputeHash((Stream) inputStream)).Replace("-", "").ToLowerInvariant();
      }
    }

    public static bool MaliciousCheck(string date)
    {
      TimeSpan timeSpan = DateTime.Parse(date) - DateTime.Now;
      if (Convert.ToInt32(timeSpan.Seconds.ToString().Replace("-", "")) < 5 && Convert.ToInt32(timeSpan.Minutes.ToString().Replace("-", "")) < 1)
        return false;
      Constants.Breached = true;
      return true;
    }
  }
}
