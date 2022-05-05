// Decompiled with JetBrains decompiler
// Type: ThunderMine.App
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using System.Collections.Generic;

namespace ThunderMine
{
  internal class App
  {
    public static string Error = (string) null;
    public static Dictionary<string, string> Variables = new Dictionary<string, string>();

    public static string GrabVariable(string name)
    {
      try
      {
        if (User.ID != null || User.HWID != null || User.IP != null || !Constants.Breached)
          return App.Variables[name];
        Constants.Breached = true;
        return "User is not logged in, possible breach detected!";
      }
      catch
      {
        return "N/A";
      }
    }
  }
}
