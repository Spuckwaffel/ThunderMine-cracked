// Decompiled with JetBrains decompiler
// Type: ThunderMine.OnProgramStart
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;

namespace ThunderMine
{
  internal class OnProgramStart
  {
    public static string AID;
    public static string Secret;
    public static string Version;
    public static string Name;
    public static string Salt;

    public static void Initialize(string name, string aid, string secret, string version)
    {
      if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version) || name.Contains("APPNAME"))
      {
        int num = (int) MessageBox.Show("Failed to initialize your application correctly in Program.cs!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
        Process.GetCurrentProcess().Kill();
      }
      OnProgramStart.AID = aid;
      OnProgramStart.Secret = secret;
      OnProgramStart.Version = version;
      OnProgramStart.Name = name;
      string[] strArray1 = new string[0];
      using (WebClient webClient = new WebClient())
      {
        try
        {
          webClient.Proxy = (IWebProxy) null;
          Security.Start();
          string[] strArray2 = Encryption.DecryptService(Encoding.Default.GetString(webClient.UploadValues(Constants.ApiUrl, new NameValueCollection()
          {
            ["token"] = Encryption.EncryptService(Constants.Token),
            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
            [nameof (aid)] = Encryption.APIService(OnProgramStart.AID),
            ["session_id"] = Constants.IV,
            ["api_id"] = Constants.APIENCRYPTSALT,
            ["api_key"] = Constants.APIENCRYPTKEY,
            ["session_key"] = Constants.Key,
            [nameof (secret)] = Encryption.APIService(OnProgramStart.Secret),
            ["type"] = Encryption.APIService("start")
          }))).Split("|".ToCharArray());
          string str = strArray2[2];
          if (!(str == "success"))
          {
            if (!(str == "binderror"))
            {
              if (str == "banned")
              {
                int num = (int) MessageBox.Show("This application has been banned for violating the TOS" + Environment.NewLine + "Contact us at support@auth.gg", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
                Process.GetCurrentProcess().Kill();
                return;
              }
            }
            else
            {
              int num = (int) MessageBox.Show(Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"), OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
              Process.GetCurrentProcess().Kill();
              return;
            }
          }
          else
          {
            Constants.Initialized = true;
            if (strArray2[3] == "Enabled")
              ApplicationSettings.Status = true;
            if (strArray2[4] == "Enabled")
              ApplicationSettings.DeveloperMode = false;
            ApplicationSettings.Hash = strArray2[5];
            ApplicationSettings.Version = strArray2[6];
            ApplicationSettings.Update_Link = strArray2[7];
            if (strArray2[8] == "Enabled")
              ApplicationSettings.Freemode = true;
            if (strArray2[9] == "Enabled")
              ApplicationSettings.Login = true;
            ApplicationSettings.Name = strArray2[10];
            if (strArray2[11] == "Enabled")
              ApplicationSettings.Register = true;
            ApplicationSettings.TotalUsers = strArray2[13];
            if (ApplicationSettings.DeveloperMode)
            {
              int num1 = (int) MessageBox.Show("Application is in Developer Mode, bypassing integrity and update check!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
              int num2 = strArray2[12] == "Enabled" ? 1 : 0;
              if (ApplicationSettings.Version != OnProgramStart.Version)
              {
                int num3 = (int) MessageBox.Show("Update " + ApplicationSettings.Version + " available, redirecting to update!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
                Process.Start(ApplicationSettings.Update_Link);
                Process.GetCurrentProcess().Kill();
              }
            }
            if (!ApplicationSettings.Status)
            {
              int num4 = (int) MessageBox.Show("Looks like this application is disabled, please try again later!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
              Process.GetCurrentProcess().Kill();
            }
          }
          Security.End();
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
          Process.GetCurrentProcess().Kill();
        }
      }
    }
  }
}
