using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gametek.Monogame.Content;

namespace Gametek.UI.Helper
{
    public class FpsCounter
    {
        private const int refreshesPerSec = 4;  // how many times do we calculate FPS & UPS every second
        private readonly TimeSpan RefreshTime = TimeSpan.FromMilliseconds(1000 / refreshesPerSec);
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private static int fps = 0, ups = 0;
        private int frameCounter = 0, updateCounter = 0;
        private Vector2 position;
        private static Process process = Process.GetCurrentProcess();
        private static List<string> messages = new List<string>();
        private static List<double> messageTimers = new List<double>();
        private StringBuilder outputSb = new StringBuilder();

        public static int FPS
        {
            get { return fps; }
        }
        public static int UPS
        {
            get { return ups; }
        }
        public static long MemAllocated
        {
            get { return process.PrivateMemorySize64; }
        }

        public FpsCounter(Vector2 pos)
        {
            this.position = pos;
        }

        public void Initialize()
        {
            
        }

        public static void ShowMessage(string msg, int milliseconds)
        {
            messages.Add(msg);
            messageTimers.Add(milliseconds);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            updateCounter++;

            if (elapsedTime > RefreshTime)
            {
                elapsedTime -= RefreshTime;
                fps = frameCounter * refreshesPerSec;
                ups = updateCounter * refreshesPerSec;
                frameCounter = 0;
                updateCounter = 0;
            }

            // Update message timers
            for (int i = 0; i < messageTimers.Count; i++)
            {
                messageTimers[i] -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (messageTimers[i] <= 0)
                {
                    messages.RemoveAt(i);   // remove timed out messages
                    messageTimers.RemoveAt(i);
                }
            }

            outputSb.Clear();
            outputSb.Append(fps.ToString() + " ");
            outputSb.Append("(" + (MemAllocated / 1024 / 1024).ToString() + " MB)" + Environment.NewLine);
            foreach (string msg in messages)
            {
                outputSb.Append(msg + Environment.NewLine);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            frameCounter++; // increment frame counter

            spriteBatch.DrawString(FontLoader.ControlFont, outputSb.ToString(), position + new Vector2(1, 1), Color.Black); // shadow
            spriteBatch.DrawString(FontLoader.ControlFont, outputSb.ToString(), position, Color.Fuchsia);
        }
    }
}
