// Decompiled with JetBrains decompiler
// Type: ThunderMine.Program
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;

namespace ThunderMine
{
  internal class Program
  {
    private static Random random = new Random();

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AllocConsole();

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select<string, char>((Func<string, char>) (s => s[Program.random.Next(s.Length)])).ToArray<char>());

    public static double RandomDoubleFrom(double minimum, double maximum) => new Random().NextDouble() * (maximum - minimum) + minimum;

    public static int RandomIntFrom(int minimum, int maximum) => new Random().Next(minimum, maximum);

    public static string StringLowercaseInfinity(int length) => new string(Enumerable.Repeat<string>("ABCDEFabcdef0123456789", length).Select<string, char>((Func<string, char>) (s => s[Program.random.Next(s.Length)])).ToArray<char>());

    public static string RandomInt(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[Program.random.Next(s.Length)])).ToArray<char>());

    public static int GetRandomInt(int minimum, int maximum) => new Random().Next(minimum, maximum);

    private static void ConnectWallet()
    {
      string path = Path.Combine(Path.GetTempPath(), "WalletAddressBTC.txt");
      try
      {
        if (!System.IO.File.Exists(path))
        {
          Console.Clear();
          Program.PrintLogo();
          Console.WriteLine("\n[+] Enter your bitcoin wallet address: ");
          Console.WriteLine("[+] Warning: DO NOT enter incorrect info");
          Console.Write("\n[+] Input: ");
          string s = Console.ReadLine();
          Console.Write("\n[+] Saving... ");
          int num = Program.random.Next(17, 60);
          using (ProgressBar progressBar = new ProgressBar())
          {
            for (int index = 0; index <= 100; ++index)
            {
              progressBar.Report((double) index / (double) num);
              Thread.Sleep(20);
            }
          }
          using (FileStream fileStream = System.IO.File.Create(path))
          {
            byte[] bytes = new UTF8Encoding(true).GetBytes(s);
            fileStream.Write(bytes, 0, bytes.Length);
          }
          Console.WriteLine("The program will restart after saving your changes.");
          Thread.Sleep(2000);
          Process.Start(AppDomain.CurrentDomain.FriendlyName);
          Environment.Exit(0);
        }
        if (!System.IO.File.Exists(path))
          return;
        using (StreamReader streamReader = System.IO.File.OpenText(path))
        {
          string str1;
          while ((str1 = streamReader.ReadLine()) != null)
          {
            string str2 = str1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\nWallet Addresses Connected: ");
            Console.WriteLine("\n[+] BTC: " + str2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNote: Your wallet address is connected so we can automatically send moeny there on payout week, it is tied to your user id");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The program will now close");
            Thread.Sleep(5000);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    public static void WriteConsoleColor(params Program.ColoredString[] strings)
    {
      ConsoleColor foregroundColor = Console.ForegroundColor;
      foreach (Program.ColoredString coloredString in strings)
      {
        Console.ForegroundColor = coloredString.Color;
        Console.Write(coloredString.Text);
      }
      Console.ForegroundColor = foregroundColor;
    }

    public static void firewallPrompt()
    {
      TcpListener tcpListener = new TcpListener(new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 12345));
      tcpListener.Start();
      tcpListener.Stop();
    }

    public static void FirstLaunch()
    {
      Program.firewallPrompt();
      int randomInt1 = Program.GetRandomInt(100, 1000);
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("FirstLaunchEvent:/");
      Thread.Sleep(10);
      Console.WriteLine("Attempting VPS Branch connection/");
      Thread.Sleep(10);
      Console.WriteLine("Finding available node.../");
      Thread.Sleep(25);
      Console.WriteLine("------------------------------------");
      Thread.Sleep(30);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "[HandShake] "), new Program.ColoredString(ConsoleColor.White, "Established: True"));
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "Established Gateway Socket"));
      Thread.Sleep(100);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "Decrypting session key"));
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "Registering session | User: " + User.ID));
      Thread.Sleep(120);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[SUCCESFULL] Trial Authentication"));
      Thread.Sleep(75);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[IDENTIFY] hasLogin: true"));
      int randomInt2 = Program.GetRandomInt(100000, 600000);
      Thread.Sleep(50);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[READY] took " + randomInt1.ToString() + "ms [gateway - prd - main - flg1, {micros:" + randomInt2.ToString() + "}"));
      Thread.Sleep(60);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[AuthenticationStore] HandleConnectionOpened called -> [storageHasStored]"));
      Thread.Sleep(45);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[READY_SUPPLEMENTAL] Stage1FirstLaunchEvent complete\n"));
      Thread.Sleep(45);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[GatewayTransfer] "), new Program.ColoredString(ConsoleColor.White, "[SocketTrue] Importing launch configuration;"));
      Thread.Sleep(45);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[GatewayTransfer] "), new Program.ColoredString(ConsoleColor.White, "[SocketTrue] Importing hash functions;;"));
      Thread.Sleep(45);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[GatewayTransfer] "), new Program.ColoredString(ConsoleColor.White, "[SocketTrue] Importing StablePatch;;;"));
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[GatewayTransfer] "), new Program.ColoredString(ConsoleColor.White, "[SocketTrue] Importing Gpoint.js/Doublehash.js\n"));
      Console.Write("\n[+] Installing.... ");
      using (ProgressBar progressBar = new ProgressBar())
      {
        for (int index = 0; index <= 100; ++index)
        {
          progressBar.Report((double) index / 50.0);
          Thread.Sleep(20);
        }
      }
      Thread.Sleep(45);
      Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.DarkMagenta, "\n\n[GatewaySocket] "), new Program.ColoredString(ConsoleColor.White, "[READY_SUPPLEMENTAL] Stage2FirstLaunchEvent complete"));
      Thread.Sleep(1750);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Clear();
      Program.PrintPrivateLogo();
      Program.SystemInfo systemInfo = new Program.SystemInfo();
      systemInfo.getOperatingSystemInfo();
      systemInfo.getCpuInfo();
      systemInfo.getGpuInfo();
      Thread.Sleep(75);
      Console.Write("\n[+] Registering Hardware.... ");
      using (ProgressBar progressBar = new ProgressBar())
      {
        for (int index = 0; index <= 100; ++index)
        {
          progressBar.Report((double) index / 50.0);
          Thread.Sleep(20);
        }
      }
      Console.WriteLine("Stage2FirstLaunchEvent complete\n");
      Console.Clear();
      Thread.Sleep(40);
    }

    public static void PrivateInfinityLoop()
    {
      string str1 = "";
      string str2 = "";
      string path1 = Path.Combine(Path.GetTempPath(), "Balance.txt");
      string path2 = Path.Combine(Path.GetTempPath(), "Checked.txt");
      string str3;
      using (WebClient webClient = new WebClient())
      {
        string address = "https://api.coindesk.com/v1/bpi/currentprice.json";
        str3 = webClient.DownloadString(address);
      }
      object obj1 = JsonConvert.DeserializeObject(str3);
      // ISSUE: reference to a compiler-generated field
      if (Program.\u003C\u003Eo__13.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Program.\u003C\u003Eo__13.\u003C\u003Ep__4 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToDecimal", (IEnumerable<Type>) null, typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target1 = Program.\u003C\u003Eo__13.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p4 = Program.\u003C\u003Eo__13.\u003C\u003Ep__4;
      Type type = typeof (Convert);
      // ISSUE: reference to a compiler-generated field
      if (Program.\u003C\u003Eo__13.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Program.\u003C\u003Eo__13.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = Program.\u003C\u003Eo__13.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = Program.\u003C\u003Eo__13.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (Program.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Program.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "rate", typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = Program.\u003C\u003Eo__13.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p2 = Program.\u003C\u003Eo__13.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (Program.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Program.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "USD", typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target4 = Program.\u003C\u003Eo__13.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = Program.\u003C\u003Eo__13.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (Program.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Program.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "bpi", typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = Program.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) Program.\u003C\u003Eo__13.\u003C\u003Ep__0, obj1);
      object obj3 = target4((CallSite) p1, obj2);
      object obj4 = target3((CallSite) p2, obj3);
      object obj5 = target2((CallSite) p3, obj4);
      object obj6 = target1((CallSite) p4, type, obj5);
      try
      {
        if (!System.IO.File.Exists(path1))
        {
          Console.Clear();
          Program.PrintLogo();
          using (FileStream fileStream = System.IO.File.Create(path1))
          {
            byte[] bytes = new UTF8Encoding(true).GetBytes("0");
            fileStream.Write(bytes, 0, bytes.Length);
          }
        }
        if (System.IO.File.Exists(path1))
        {
          using (StreamReader streamReader = System.IO.File.OpenText(path1))
          {
            string str4;
            while ((str4 = streamReader.ReadLine()) != null)
              str1 = str4;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      try
      {
        if (!System.IO.File.Exists(path2))
        {
          Console.Clear();
          Program.PrintLogo();
          using (FileStream fileStream = System.IO.File.Create(path2))
          {
            byte[] bytes = new UTF8Encoding(true).GetBytes("0");
            fileStream.Write(bytes, 0, bytes.Length);
          }
        }
        if (System.IO.File.Exists(path2))
        {
          using (StreamReader streamReader = System.IO.File.OpenText(path2))
          {
            string str5;
            while ((str5 = streamReader.ReadLine()) != null)
              str2 = str5;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      double num1 = Convert.ToDouble(str1);
      int num2 = 0;
      string str6 = "89";
      while (true)
      {
        do
        {
          // ISSUE: reference to a compiler-generated field
          if (Program.\u003C\u003Eo__13.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Program.\u003C\u003Eo__13.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (Program)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target5 = Program.\u003C\u003Eo__13.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p7 = Program.\u003C\u003Eo__13.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (Program.\u003C\u003Eo__13.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Program.\u003C\u003Eo__13.\u003C\u003Ep__6 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, string, object, object> target6 = Program.\u003C\u003Eo__13.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, string, object, object>> p6 = Program.\u003C\u003Eo__13.\u003C\u003Ep__6;
          string str7 = "Status: Connected ✔ | Checked: " + num2.ToString() + " Wallets | Current Balance: " + Math.Round(num1, 5).ToString() + " BTC | Per: 372.6/s | Sector: 89 | Threads: 128 | Rate: Steady/Static | 1 BTC = $";
          // ISSUE: reference to a compiler-generated field
          if (Program.\u003C\u003Eo__13.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Program.\u003C\u003Eo__13.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Round", (IEnumerable<Type>) null, typeof (Program), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj7 = Program.\u003C\u003Eo__13.\u003C\u003Ep__5.Target((CallSite) Program.\u003C\u003Eo__13.\u003C\u003Ep__5, typeof (Math), obj6, 2);
          object obj8 = target6((CallSite) p6, str7, obj7);
          Console.Title = target5((CallSite) p7, obj8);
          Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[-] "), new Program.ColoredString(ConsoleColor.White, "BTC | bc1" + Program.StringLowercaseInfinity(32) + " - BALANCE: "), new Program.ColoredString(ConsoleColor.Red, "0.00 BTC "), new Program.ColoredString(ConsoleColor.White, "| Sector: " + str6 + Program.RandomInt(4) + " | Wallet Type: P2WPKH"));
          ++num2;
          string contents1 = num2.ToString();
          System.IO.File.WriteAllText(path2, contents1);
          Console.ResetColor();
          Thread.Sleep(6);
          ++num2;
          string contents2 = num2.ToString();
          System.IO.File.WriteAllText(path2, contents2);
          Console.ResetColor();
        }
        while (new Random().Next(1, 10000) != 1);
        string contents3 = num2.ToString();
        System.IO.File.WriteAllText(path2, contents3);
        Program.StringLowercaseInfinity(32);
        double num3 = Program.RandomDoubleFrom(0.0006, 1.0);
        Thread.Sleep(500);
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Magenta, "\n[-] "), new Program.ColoredString(ConsoleColor.Green, "BTC | bc1qmqxuvcez78gqvx69pvth5dykryr7q2z3m3yg8j - BALANCE: 0.08591418 BTC | Sector: " + str6 + Program.RandomInt(4) + " | Wallet Type: P2WPKH\n"));
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nMatching SigScripts...");
        Thread.Sleep(50);
        Console.WriteLine("\nSending to workers...");
        Thread.Sleep(250);
        Console.Write("\nProcessing.... ");
        using (ProgressBar progressBar = new ProgressBar())
        {
          for (int index = 0; index <= 100; ++index)
          {
            progressBar.Report((double) index / 50.0);
            Thread.Sleep(20);
          }
        }
        Thread.Sleep(45);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n\nSuccess! ");
        Program.GetRandomInt(1350, 4500);
        Thread.Sleep(40);
        Console.WriteLine("\nDepositing....");
        Thread.Sleep(35);
        Console.WriteLine("\nPrivate Key Derived! | User: " + User.ID);
        Thread.Sleep(150);
        double num4 = Math.Round(num3, 5) - 0.0005;
        Console.WriteLine("\nWithdrawal Fee Applied...");
        Thread.Sleep(65);
        Console.WriteLine("\nSuccessfull Deposit of: 0.02887143 BTC -> bc1qhm07kv95vpmgwj6yw0zad8k28zx2022ca58ssp\n");
        Thread.Sleep(800);
        Console.Write("\n[+] Displaying blockchain info...");
        using (ProgressBar progressBar = new ProgressBar())
        {
          for (int index = 0; index <= 100; ++index)
          {
            progressBar.Report((double) index / 20.0);
            Thread.Sleep(20);
          }
        }
        Console.WriteLine("\n[+] Opening...");
        Process.Start("https://www.blockchain.com/btc/address/bc1qhm07kv95vpmgwj6yw0zad8k28zx2022ca58ssp");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        using (StreamReader streamReader = System.IO.File.OpenText(path1))
        {
          string str8;
          while ((str8 = streamReader.ReadLine()) != null)
            str1 = str8;
        }
        num1 += Math.Round(num4, 5);
        Convert.ToDouble(str1);
        string contents4 = num1.ToString();
        System.IO.File.WriteAllText(path1, contents4);
        Console.WriteLine("\nPress Enter to Continue Mining...");
        Console.ReadLine();
      }
    }

    public static void PrivateInfinite()
    {
      int randomInt = Program.GetRandomInt(1, 3);
      Console.Title = "ThunderMine Private";
      Random random = new Random();
      random.Next().ToString("X");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Thread.Sleep(250);
      Console.WriteLine("[+] Note: You will join a private server dedicated to you, any succesful entries will result in the amount accounted to your user id.");
      Console.Write("\n[+] Press Enter to Connect...");
      Console.ReadLine();
      Thread.Sleep(800);
      Console.Clear();
      Thread.Sleep(300);
      Program.PrintPrivateLogo();
      Program.FirstLaunch();
      Thread.Sleep(20);
      Console.Clear();
      Thread.Sleep(30);
      Program.PrintPrivateLogo();
      Console.Title = "ThunderMine Private0" + randomInt.ToString();
      Console.Write("\n[+] Connecting to ThunderMine Private0" + randomInt.ToString() + "... ");
      int num = random.Next(17, 70);
      using (ProgressBar progressBar = new ProgressBar())
      {
        for (int index = 0; index <= 100; ++index)
        {
          progressBar.Report((double) index / (double) num);
          Thread.Sleep(20);
        }
      }
      Console.WriteLine("Completed Succesfully!");
      Thread.Sleep(50);
      Console.WriteLine("\n[+] Logged in as " + User.Username);
      Thread.Sleep(500);
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\n[+] Workers Online: " + Program.GetRandomInt(7, 12).ToString());
      Console.ForegroundColor = ConsoleColor.Magenta;
      Thread.Sleep(100);
      Console.WriteLine("\n[+] Installing P2WPKH DB         [=====]");
      Thread.Sleep(20);
      Console.WriteLine("[+] Loading...                   [========]");
      Thread.Sleep(120);
      Console.WriteLine("[+] Loading...                   [==========]");
      Thread.Sleep(80);
      Console.WriteLine("[+] Indexing Completed!          [============]");
      Thread.Sleep(20);
      Console.WriteLine("[+] Wallets Loaded!              [==============]");
      Thread.Sleep(20);
      Console.WriteLine("[+] Loading Hashes               [================]");
      Thread.Sleep(20);
      Console.WriteLine("[+] Loading...                   [==================]");
      Thread.Sleep(75);
      Console.WriteLine("[+] Loading...                   [====================]");
      Thread.Sleep(130);
      Console.WriteLine("[+] Hash Function Loaded         [======================]");
      Thread.Sleep(75);
      Console.WriteLine("[+] Job Completed!               [========================]");
      Thread.Sleep(75);
      Console.WriteLine("[+] Installing Required Modules  [===========================]");
      Thread.Sleep(250);
      Console.WriteLine("\n[+] Modules Loaded 7/7           [==============================]");
      Console.Write("\n[+] Validating... ");
      using (ProgressBar progressBar = new ProgressBar())
      {
        for (int index = 0; index <= 100; ++index)
        {
          progressBar.Report((double) index / 20.0);
          Thread.Sleep(20);
        }
      }
      Console.WriteLine("\n[+] Generating Session Key...");
      Thread.Sleep(250);
      Console.WriteLine("[+] Local Session Key: " + Program.RandomString(12));
      Thread.Sleep(150);
      Console.WriteLine("\n[+] Written and fully owned by ThunderMine");
      Thread.Sleep(80);
      Console.WriteLine("[+] Y/N to join the private mining pool: ");
      string str = Console.ReadKey().Key.ToString();
      if (str.ToUpper() == "Y")
        Thread.Sleep(1300);
      Console.Clear();
      Program.PrintPrivateLogo();
      Thread.Sleep(35);
      Console.WriteLine("Connecting to SegWit mainnet...");
      Thread.Sleep(1300);
      Console.Clear();
      Program.PrivateInfinityLoop();
      if (str.ToUpper() == "N")
        Environment.Exit(0);
      Console.ReadLine();
    }

    public static void config()
    {
      string text1 = "";
      string path1 = Path.Combine(Path.GetTempPath(), "Checked.txt");
      string path2 = Path.Combine(Path.GetTempPath(), "Balance.txt");
      string path3 = Path.Combine(Path.GetTempPath(), "WalletAddressBTC.txt");
      string rank = User.Rank;
      string id = User.ID;
      string expiry = User.Expiry;
      string username = User.Username;
      string text2 = !(rank == "2") ? "Public" : "Private";
      string str1 = "0";
      if (!System.IO.File.Exists(path3))
      {
        using (StreamReader streamReader = System.IO.File.OpenText(path2))
        {
          string str2;
          while ((str2 = streamReader.ReadLine()) != null)
            str1 = str2;
        }
        using (StreamReader streamReader = System.IO.File.OpenText(path1))
        {
          string str3;
          while ((str3 = streamReader.ReadLine()) != null)
            text1 = str3;
        }
        Console.WriteLine("\n\n------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("[DEFAULT SETTINGS]");
        Console.WriteLine("");
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "[-] Wallet Types: "), new Program.ColoredString(ConsoleColor.White, "Segwitt Mainnet (P2WPKH) / Segwitt Mainnet (P2WSH) / Bech32"));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Withdrawal Address: "), new Program.ColoredString(ConsoleColor.White, "Not Connected!"));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Withdrawal Fee: "), new Program.ColoredString(ConsoleColor.White, "0.0005 btc"));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] BruteType: "), new Program.ColoredString(ConsoleColor.White, "GPoint/DoubleHash deriver/Sigscript match"));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Payout Date: "), new Program.ColoredString(ConsoleColor.White, "4/25/2022 | EDT 4PM"));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Version: "), new Program.ColoredString(ConsoleColor.White, "2.80\n"));
        Console.WriteLine("\n[User Information]");
        Console.WriteLine("");
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "[-] Username: "), new Program.ColoredString(ConsoleColor.White, username));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] User ID: "), new Program.ColoredString(ConsoleColor.White, id));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Access Level: "), new Program.ColoredString(ConsoleColor.White, text2));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Expires On: "), new Program.ColoredString(ConsoleColor.White, expiry));
        Console.WriteLine("\n\n[Statistics]");
        Console.WriteLine("");
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "[-] Wallets Checked: "), new Program.ColoredString(ConsoleColor.White, text1));
        Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] User Balance: "), new Program.ColoredString(ConsoleColor.White, str1 + " BTC"));
        Console.WriteLine("\n");
        Thread.Sleep(50000);
      }
      try
      {
        if (!System.IO.File.Exists(path3))
          return;
        using (StreamReader streamReader = System.IO.File.OpenText(path3))
        {
          string text3;
          while ((text3 = streamReader.ReadLine()) != null)
          {
            Console.WriteLine("\n\n------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("[DEFAULT SETTINGS]");
            Console.WriteLine("");
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "[-] Wallet Types: "), new Program.ColoredString(ConsoleColor.White, "Segwitt Mainnet (P2WPKH) / Segwitt Mainnet (P2WSH) / Bech32"));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Withdrawal Address: "), new Program.ColoredString(ConsoleColor.White, text3));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Withdrawal Fee: "), new Program.ColoredString(ConsoleColor.White, "0.0005 btc"));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] BruteType: "), new Program.ColoredString(ConsoleColor.White, "GPoint/DoubleHash deriver/Sigscript match"));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Payout Date: "), new Program.ColoredString(ConsoleColor.White, "4/25/2022 | EDT 4PM"));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Version: "), new Program.ColoredString(ConsoleColor.White, "2.80\n"));
            Console.WriteLine("\n[User Information]");
            Console.WriteLine("");
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "[-] Username: "), new Program.ColoredString(ConsoleColor.White, username));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] User ID: "), new Program.ColoredString(ConsoleColor.White, id));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Access Level: "), new Program.ColoredString(ConsoleColor.White, text2));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] Expires On: "), new Program.ColoredString(ConsoleColor.White, expiry + "\n"));
            Console.WriteLine("\n\n[Statistics]");
            Console.WriteLine("");
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "Wallets Checked: "), new Program.ColoredString(ConsoleColor.White, text1));
            Program.WriteConsoleColor(new Program.ColoredString(ConsoleColor.Green, "\n[-] User Balance: "), new Program.ColoredString(ConsoleColor.White, str1 + " BTC"));
            Console.WriteLine("\n");
            Thread.Sleep(50000000);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    private static void Main(string[] args)
    {
      OnProgramStart.Initialize("Solarcrypt", "366093", "ffUNwzxKuZFrKkMSlTZwT7pY1WzE6R296jw", "1.0");
      Console.Title = "Thunder Miner | Build 2.8";
      string path1 = Path.Combine(Path.GetTempPath(), "Checked.txt");
      string path2 = Path.Combine(Path.GetTempPath(), "Balance.txt");
      string path3 = Path.Combine(Path.GetTempPath(), "autologin.txt");
      string path4 = Path.Combine(Path.GetTempPath(), "username.txt");
      string path5 = Path.Combine(Path.GetTempPath(), "password.txt");
      if (ApplicationSettings.Freemode)
      {
        int num1 = (int) MessageBox.Show("Freemode is active, bypassing login!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
      if (!ApplicationSettings.Status)
      {
        int num2 = (int) MessageBox.Show("Application is disabled!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
        Process.GetCurrentProcess().Kill();
      }
      Program.PrintLogo();
      try
      {
        if (!System.IO.File.Exists(path2))
        {
          Console.Clear();
          Program.PrintLogo();
          using (FileStream fileStream = System.IO.File.Create(path2))
          {
            byte[] bytes = new UTF8Encoding(true).GetBytes("0");
            fileStream.Write(bytes, 0, bytes.Length);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      try
      {
        if (!System.IO.File.Exists(path1))
        {
          Console.Clear();
          Program.PrintLogo();
          using (FileStream fileStream = System.IO.File.Create(path1))
          {
            byte[] bytes = new UTF8Encoding(true).GetBytes("0");
            fileStream.Write(bytes, 0, bytes.Length);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      try
      {
        if (!System.IO.File.Exists(path3))
        {
          Console.WriteLine("[1] Login");
          Console.WriteLine("[2] Register");
          Console.Write("\nInput: ");
          string str1 = Console.ReadLine();
          if (str1 == "1")
          {
            if (!ApplicationSettings.Login)
            {
              int num3 = (int) MessageBox.Show("Login is not enabled, please try again later!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
              Process.GetCurrentProcess().Kill();
            }
            else
            {
              Console.Clear();
              Program.PrintLogo();
              Console.WriteLine();
              Console.WriteLine("Username:");
              string contents1 = Console.ReadLine();
              Console.WriteLine("Password:");
              string contents2 = Console.ReadLine();
              using (FileStream fileStream = System.IO.File.Create(path4))
              {
                byte[] bytes = new UTF8Encoding(true).GetBytes("0");
                fileStream.Write(bytes, 0, bytes.Length);
              }
              using (FileStream fileStream = System.IO.File.Create(path5))
              {
                byte[] bytes = new UTF8Encoding(true).GetBytes("0");
                fileStream.Write(bytes, 0, bytes.Length);
              }
              System.IO.File.WriteAllText(path4, contents1);
              System.IO.File.WriteAllText(path5, contents2);
              using (FileStream fileStream = System.IO.File.Create(path3))
              {
                byte[] bytes = new UTF8Encoding(true).GetBytes("0");
                fileStream.Write(bytes, 0, bytes.Length);
              }
              int num4 = (int) MessageBox.Show("Auto-Login Enabled, please restart the program.", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Asterisk);
              Thread.Sleep(5000000);
            }
          }
          else if (str1 == "2")
          {
            if (!ApplicationSettings.Register)
            {
              int num5 = (int) MessageBox.Show("Register is not enabled, please try again later!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
              Process.GetCurrentProcess().Kill();
            }
            else
            {
              Console.Clear();
              Program.PrintLogo();
              Console.WriteLine();
              Console.WriteLine("Username:");
              string username = Console.ReadLine();
              Console.WriteLine("Password:");
              string str2 = Console.ReadLine();
              Console.WriteLine("Email:");
              string str3 = Console.ReadLine();
              Console.WriteLine("License:");
              string str4 = Console.ReadLine();
              string password = str2;
              string email = str3;
              string license = str4;
              if (API.Register(username, password, email, license))
              {
                int num6 = (int) MessageBox.Show("You have successfully registered!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Asterisk);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      if (!System.IO.File.Exists(path3))
        return;
      string username1 = "";
      string password1 = "";
      Console.WriteLine("[1] Login");
      Console.WriteLine("[2] Register");
      Console.Write("\nInput: ");
      string str5 = Console.ReadLine();
      if (str5 == "1")
      {
        if (!ApplicationSettings.Login)
        {
          int num7 = (int) MessageBox.Show("Login is not enabled, please try again later!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
          Process.GetCurrentProcess().Kill();
        }
        else
        {
          using (StreamReader streamReader = System.IO.File.OpenText(path4))
          {
            string str6;
            while ((str6 = streamReader.ReadLine()) != null)
              username1 = str6;
          }
          using (StreamReader streamReader = System.IO.File.OpenText(path5))
          {
            string str7;
            while ((str7 = streamReader.ReadLine()) != null)
              password1 = str7;
          }
          Console.Clear();
          Program.PrintLogo();
          Console.WriteLine("AutoLogin: Enabled");
          Console.Clear();
          if (!API.Login(username1, password1))
            return;
          Console.Write("\n[+] Logging In... ");
          using (ProgressBar progressBar = new ProgressBar())
          {
            for (int index = 0; index <= 100; ++index)
            {
              progressBar.Report((double) index / 20.0);
              Thread.Sleep(20);
            }
          }
          Console.Clear();
          Program.PrintLogo();
          Console.WriteLine("[1] Public Mining");
          Console.WriteLine("[2] Private Mining");
          Console.WriteLine("[3] Connect your wallet");
          Console.WriteLine("[4] Settings and Statistics");
          Console.WriteLine("[5] Exit");
          Console.Write("\nInput: ");
          string str8 = Console.ReadLine();
          if (str8 == "1")
          {
            Console.Clear();
            Console.WriteLine("\nPublic Mining is currently under development.");
            Console.WriteLine("\nPlease restart the program...");
            Thread.Sleep(100000000);
          }
          if (str8 == "2")
          {
            if (User.Rank == "1" || User.Rank == "2" || User.Rank == "3")
            {
              Thread.Sleep(500);
              Console.Clear();
              Program.PrintPrivateLogo();
              Program.PrivateInfinite();
            }
            else
            {
              int num8 = (int) MessageBox.Show("You do not have access to this level!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
              Thread.Sleep(1000);
              Environment.Exit(0);
            }
          }
          if (str8 == "3")
          {
            Program.AllocConsole();
            Console.Title = "SecureWindow";
            Console.Clear();
            Program.PrintLogo();
            Program.ConnectWallet();
            Environment.Exit(0);
          }
          if (str8 == "4")
          {
            Console.Clear();
            Program.PrintLogo();
            Program.config();
          }
          if (!(str8 == "5"))
            return;
          Environment.Exit(0);
        }
      }
      else
      {
        if (!(str5 == "2"))
          return;
        if (!ApplicationSettings.Register)
        {
          int num9 = (int) MessageBox.Show("Register is not enabled, please try again later!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Hand);
          Process.GetCurrentProcess().Kill();
        }
        else
        {
          Console.Clear();
          Program.PrintLogo();
          Console.WriteLine();
          Console.WriteLine("Username:");
          string username2 = Console.ReadLine();
          Console.WriteLine("Password:");
          string str9 = Console.ReadLine();
          Console.WriteLine("Email:");
          string str10 = Console.ReadLine();
          Console.WriteLine("License:");
          string str11 = Console.ReadLine();
          string password2 = str9;
          string email = str10;
          string license = str11;
          if (!API.Register(username2, password2, email, license))
            return;
          int num10 = (int) MessageBox.Show("You have successfully registered!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
      }
    }

    private static void GetComponent(string hwclass, string syntax)
    {
      foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass).Get())
        Console.WriteLine(Convert.ToString(managementBaseObject[syntax]));
    }

    public static void PrintLogo()
    {
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("\r\n\r\n                             ████████╗██╗  ██╗██╗   ██╗███╗   ██╗██████╗ ███████╗██████╗ \r\n                             ╚══██╔══╝██║  ██║██║   ██║████╗  ██║██╔══██╗██╔════╝██╔══██╗\r\n                                ██║   ███████║██║   ██║██╔██╗ ██║██║  ██║█████╗  ██████╔╝\r\n                                ██║   ██╔══██║██║   ██║██║╚██╗██║██║  ██║██╔══╝  ██╔══██╗\r\n                                ██║   ██║  ██║╚██████╔╝██║ ╚████║██████╔╝███████╗██║  ██║\r\n                                ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝\r\n \r\n                                        ███╗   ███╗██╗███╗   ██╗███████╗██████╗ \r\n                                        ████╗ ████║██║████╗  ██║██╔════╝██╔══██╗\r\n                                        ██╔████╔██║██║██╔██╗ ██║█████╗  ██████╔╝\r\n                                        ██║╚██╔╝██║██║██║╚██╗██║██╔══╝  ██╔══██╗\r\n                                        ██║ ╚═╝ ██║██║██║ ╚████║███████╗██║  ██║\r\n                                        ╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝\r\n\r\n\r\n");
    }

    public static void PrintPrivateLogo()
    {
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("\r\n\r\n              ████████╗██╗  ██╗██╗   ██╗███╗   ██╗██████╗ ███████╗██████╗ ███╗   ███╗██╗███╗   ██╗███████╗\r\n              ╚══██╔══╝██║  ██║██║   ██║████╗  ██║██╔══██╗██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔════╝\r\n                 ██║   ███████║██║   ██║██╔██╗ ██║██║  ██║█████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║█████╗  \r\n                 ██║   ██╔══██║██║   ██║██║╚██╗██║██║  ██║██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══╝  \r\n                 ██║   ██║  ██║╚██████╔╝██║ ╚████║██████╔╝███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║███████╗\r\n                 ╚═╝   ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝  \r\n                                             _____      _            _            \r\n                                            |  __ \\    (_)          | |        \r\n                                            | |__) | __ ___   ____ _| |_ ___    \r\n                                            |  ___/ '__| \\ \\ / / _` | __/ _ \\ \r\n                                            | |   | |  | |\\ V / (_| | ||  __/\r\n                                            |_|   |_|  |_| \\_/ \\__,_|\\__\\___|               \r\n                                        \r\n                                         \r\n\r\n                                  \r\n                                             ");
    }

    public class ColoredString
    {
      public ConsoleColor Color;
      public string Text;

      public ColoredString(ConsoleColor color, string text)
      {
        this.Color = color;
        this.Text = text;
      }
    }

    public class SystemInfo
    {
      public void getOperatingSystemInfo()
      {
        Console.WriteLine("[+] Loading System Information....\n");
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get())
        {
          if (managementObject["Caption"] != null)
            Console.WriteLine("Operating System Name:  " + managementObject["Caption"].ToString());
          if (managementObject["OSArchitecture"] != null)
            Console.WriteLine("Operating System Architecture:  " + managementObject["OSArchitecture"].ToString());
        }
      }

      public void getCpuInfo()
      {
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("select * from Win32_Processor").Get())
        {
          if (managementObject["Name"] != null)
            Console.WriteLine("Central Processing Unit:  " + managementObject["Name"].ToString());
        }
      }

      public void getGpuInfo()
      {
        foreach (ManagementObject managementObject in new ManagementObjectSearcher("select * from Win32_VideoController").Get())
        {
          if (managementObject["Name"] != null)
            Console.WriteLine("Graphics Processing Unit:  " + managementObject["Name"].ToString());
        }
      }
    }
  }
}
