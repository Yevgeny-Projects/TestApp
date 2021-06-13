using System;

namespace CopyTreeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var treeA = new TreeA
            {
                Payload = "1",
                LeftBranch = new TreeA { 
                    Payload = "2",
                    RightBranch = new TreeA
                    {
                        Payload = "02",
                        RightBranch = new TreeA
                        {
                            Payload = "02",

                        }
                    }
                },
                RightBranch = new TreeA {
                    LeftBranch = new TreeA
                    {
                        Payload = "2",
                        RightBranch = new TreeA
                        {
                            Payload = "02",
                            RightBranch = new TreeA
                            {
                                Payload = "02",

                            }
                        }
                    },
                }

            };

            var copy = QueueMapper.Map(treeA);
        }
    }
}
