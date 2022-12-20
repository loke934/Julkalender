using System;
using System.Collections.Generic;

namespace CatAI
{
    public struct Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class CatAI
    {
        public string Main(string input)
        {
            string[] rows = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int gridSizeX = int.Parse(rows[0]);
            string target = rows[2];
            string[] positions = rows[3].Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            Vector2 playerPos = new Vector2(0,0);
            string dir = "Stop";
            List<Vector2> targets = new List<Vector2>();
            List<Vector2> obstacles = new List<Vector2>();

            foreach (string pos in positions)
            {
                if (pos.Contains("P"))
                {
                    playerPos.X = int.Parse(pos[1].ToString());
                    playerPos.Y = int.Parse(pos[3].ToString());
                    Console.WriteLine("player pos: " + playerPos.X + "," + playerPos.Y);
                    continue;
                }
                if (pos.Contains(target))
                {
                    Vector2 targetPos = new Vector2(int.Parse(pos[1].ToString()), int.Parse(pos[3].ToString()));
                    targets.Add(targetPos);
                    continue;
                }
                Vector2 obstacle = new Vector2(int.Parse(pos[1].ToString()), int.Parse(pos[3].ToString()));
                obstacles.Add(obstacle);
            }

            if (targets.Count > 0 ) 
            {
                if (targets[0].X < playerPos.X)
                {
                    dir = "Left";
                }
                if (targets[0].X > playerPos.X)
                {
                    dir = "Right";
                }
                if (targets[0].X == playerPos.X)
                {
                    dir = "Stop";
                }
            }

            Vector2 nextPos = new Vector2(playerPos.X, playerPos.Y + 1);
            switch (dir)
            {
                case "Right":
                    nextPos.X++;
                    break;
                case "Left":
                    nextPos.X--;
                    break;
                case "Stop":
                    break;
            }

            if (obstacles.Contains(nextPos))//Check if next position will become occupied by an obstacle
            {
                switch (dir)
                {
                    case "Right":
                        dir = "Left";
                        break;
                    case "Left":
                        dir = "Right";
                        break;
                    case "Stop":
                        if (nextPos.X -1 < 0 || obstacles.Contains(new Vector2(nextPos.X - 1, nextPos.Y + 1 ))) //At left end or obstacle will be to left
                        {
                            Console.WriteLine("At left end or obstacle will be to left");
                            dir = "Right";
                        }
                        else if (nextPos.X + 1 > gridSizeX -1 || obstacles.Contains(new Vector2(nextPos.X + 1, nextPos.Y + 1 ))) //At right end or obstacle will be to right
                        {
                            Console.WriteLine("At right end or obstacle will be to right");
                            dir = "Left";
                        }
                        else
                        {
                            Console.WriteLine("Free to move to both sides");
                            dir = "Right";
                        }
                        break;
                }
            }
            return dir;
        }

        public bool ExpertMode()
        {
            return false;
        }
    }
}
