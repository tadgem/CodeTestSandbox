using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestSandbox
{
    public class MergeNames
    {
        public static string[] UniqueNames(string[] names1, string[] names2)
        {
            List<string> uniques = new List<string>();

            foreach (string name in names1)
            {
                if (uniques.Contains(name))
                {
                    continue;
                }
                uniques.Add(name);

            }
            foreach (string name in names2)
            {
                if (uniques.Contains(name))
                {
                    continue;
                }
                uniques.Add(name);
            }
            return uniques.ToArray();
        }

        //public static void Main(string[] args)
        //{
        //    string[] names1 = new string[] { "Ava", "Emma", "Olivia" };
        //    string[] names2 = new string[] { "Olivia", "Sophia", "Emma" };
        //    Console.WriteLine(string.Join(", ", MergeNames.UniqueNames(names1, names2))); // should print Ava, Emma, Olivia, Sophia
        //}
    }

    public class MaxSum
    {

        public static int FindMaxSum(List<int> list)
        {
            int h1 = 0, h2 = 0;
            foreach (int i in list)
            {
                if (i > h2 && i <= h1)
                {
                    h2 = i;
                    continue;
                }

                if(i > h1)
                {
                    h2 = h1;
                    h1 = i; 
                }

            }
            return h1 + h2;
        }

        //public static void Main(string[] args)
        //{
        //    List<int> list = new List<int> { 5, 9, 7, 11 };
        //    Console.WriteLine(FindMaxSum(list));
        //}
    }

    public class ConstructionGame
    {
        public struct Level
        {
            public bool[,] Cubes;
            public int Width, Length;

            public Level(int width, int length)
            {
                Width = width;
                Length = length;
                Cubes = new bool[length, width];
            }

            public bool IsComplete()
            {
                bool allTrue = false;
                bool allFalse = false;
                for (int w = 0; w < Width; w++)
                {
                    for(int l = 0; l < Length; l++)
                    {
                        allTrue |= Cubes[l, w];
                        allFalse |= !Cubes[l, w];
                    }
                }
                return allTrue || allFalse;
            }
        }

        public List<Level> GameWorld;
        public int BoardWidth, BoardLength;

        public ConstructionGame(int length, int width)
        {
            GameWorld = new List<Level>();
            BoardLength = length;
            BoardWidth = width;
        }

        public void AddCubes(bool[,] cubes)
        {
            if(GameWorld.Count == 0)
            {
                GameWorld.Add(new Level(BoardWidth, BoardLength));
            }
            int currentLevelIndex = GameWorld.Count - 1;

            for (int w = 0; w < BoardWidth; w++)
            {
                for (int l = 0; l < BoardLength; l++)
                {
                    // We want to place a block at this w,l
                    if (cubes[l, w])
                    {
                        // Current level does not have a cube in this position
                        if (!GameWorld[currentLevelIndex].Cubes[l, w])
                        {
                            // find the first level (from bottom) that does not have a cube in this position;
                            int levelToPlace = currentLevelIndex;
                            for (int i = 0; i <= currentLevelIndex; i++)
                            {
                                if (!GameWorld[i].Cubes[l, w])
                                {  
                                    levelToPlace = i; 
                                    break; 
                                }
                                
                            }
                            GameWorld[levelToPlace].Cubes[l, w] = true;
                            continue;
                        }
                        // current level does have a cube in this position, create a new level
                        else
                        {
                            GameWorld.Add(new Level(BoardWidth, BoardLength));
                            currentLevelIndex = GameWorld.Count - 1;
                            GameWorld[currentLevelIndex].Cubes[l, w] = true;
                        }
                    }
                    // game world empty, ignore 
                    if (GameWorld.Count == 0)
                    {
                        continue;
                    }

                    // if the current level is complete, remove it from stack
                    if (GameWorld[currentLevelIndex].IsComplete())
                    {
                        GameWorld.RemoveAt(currentLevelIndex);
                        currentLevelIndex -= 1;
                        continue;
                    }

                }
            }
            // after adding a set of cubes, check if any levels are now deletable.
            List<int> deletionQueue = new List<int>();

            for (int i = GameWorld.Count - 1; i >= 0; i--)
            {
                if (GameWorld[i].IsComplete())
                {
                    deletionQueue.Add(i);
                }
            }

            foreach (int finishedLevelIndex in deletionQueue)
            {
                GameWorld.RemoveAt(finishedLevelIndex);
            }

        }

        public int GetHeight()
        {
            return GameWorld.Count;
        }

        public static void Main(string[] args)
        {
            ConstructionGame game = new ConstructionGame(2, 2);

            game.AddCubes(new bool[,]
            {
            { true, true },
            { true, true }
            });
            game.AddCubes(new bool[,]
            {
            { true, true },
            { true, true }
            });
            Console.WriteLine(game.GetHeight()); // should print 0

            game.AddCubes(new bool[,]
            {
            { true, true},
            { true, true }
            });
            Console.WriteLine(game.GetHeight()); // should print 0


            game.AddCubes(new bool[,]
            {
            { false, false},
            { false, false}
            });
            Console.WriteLine(game.GetHeight()); // should print 0
        }
    }
}

