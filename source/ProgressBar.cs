// Decompiled with JetBrains decompiler
// Type: ProgressBar
// Assembly: ThunderMine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B3B4670-1E5E-4A41-9FA7-10B6F3C19FB1
// Assembly location: C:\Users\roman\Downloads\Thunder_Miner\Thunder Miner\ThunderMine.exe

using System;
using System.Text;
using System.Threading;

public class ProgressBar : IDisposable, IProgress<double>
{
  private const int blockCount = 10;
  private readonly TimeSpan animationInterval = TimeSpan.FromSeconds(1.0 / 30.0);
  private const string animation = "|/-\\";
  private readonly Timer timer;
  private double currentProgress;
  private string currentText = string.Empty;
  private bool disposed;
  private int animationIndex;

  public ProgressBar()
  {
    this.timer = new Timer(new TimerCallback(this.TimerHandler));
    if (Console.IsOutputRedirected)
      return;
    this.ResetTimer();
  }

  public void Report(double value)
  {
    value = Math.Max(0.0, Math.Min(1.0, value));
    Interlocked.Exchange(ref this.currentProgress, value);
  }

  private void TimerHandler(object state)
  {
    lock (this.timer)
    {
      if (this.disposed)
        return;
      int count = (int) (this.currentProgress * 10.0);
      int num = (int) (this.currentProgress * 100.0);
      this.UpdateText(string.Format("[{0}{1}] {2,3}% {3}", (object) new string('#', count), (object) new string('-', 10 - count), (object) num, (object) "|/-\\"[this.animationIndex++ % "|/-\\".Length]));
      this.ResetTimer();
    }
  }

  private void UpdateText(string text)
  {
    int num1 = 0;
    int num2 = Math.Min(this.currentText.Length, text.Length);
    while (num1 < num2 && (int) text[num1] == (int) this.currentText[num1])
      ++num1;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append('\b', this.currentText.Length - num1);
    stringBuilder.Append(text.Substring(num1));
    int repeatCount = this.currentText.Length - text.Length;
    if (repeatCount > 0)
    {
      stringBuilder.Append(' ', repeatCount);
      stringBuilder.Append('\b', repeatCount);
    }
    Console.Write((object) stringBuilder);
    this.currentText = text;
  }

  private void ResetTimer() => this.timer.Change(this.animationInterval, TimeSpan.FromMilliseconds(-1.0));

  public void Dispose()
  {
    lock (this.timer)
    {
      this.disposed = true;
      this.UpdateText(string.Empty);
    }
  }
}
