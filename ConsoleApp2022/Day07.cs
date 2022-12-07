using System.ComponentModel.Design.Serialization;
using Common;

namespace ConsoleApp2022;

public class Day07 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        return input.directories.Where(d => d.Size <= 100000).Sum(d => d.Size);

    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        var total = 70000000;
        var needed = 30000000;

        var filled = input.root.Size;

        var available = total - filled;
        var cleanup = needed - available;

        var target = input.directories.Where(d => d.Size >= cleanup).OrderBy(d => d.Size).First();

        return target.Size;
    }

    private (IFileSystemItem root, Directory[] directories) Parse(string[] lines)
    {
        var root = new Directory("/");
        var current = root;
        string lastcommand = "";
   
        foreach (var line in  lines)
        {
            if (line.StartsWith('$'))
            {
                var (first, second, third) = line.Split(' ');
                if (second == "cd")
                {
                    if (third == "..")
                    {
                        current = current.Parent;
                    }
                    else if (third == "/")
                    {
                        current = root;
                    } 
                    else 
                    {
                        current = current.Items.OfType<Directory>().First(d => d.Name == third);
                    }
                }

                lastcommand = second;
            }
            else
            {
                if (lastcommand != "ls")
                    throw new Exception("Unexpected");

                var (first, second) = line.Split(' ');
                if (first == "dir")
                {
                    current.Items.Add(new Directory(second, current));
                }
                else
                {
                    current.Items.Add(new File(second, int.Parse(first)));
                }
            }
        }

        return (root, root.AllDirectories.ToArray());
    }

    interface IFileSystemItem
    {
        string Name { get; }
        public long Size { get; }
    }

    class Directory : IFileSystemItem
    {
        public Directory(string name)
        {
            Items = new List<IFileSystemItem>();
            Name = name;
        }

        public Directory(string name, Directory parent) : this(name)
        {
            Parent = parent;
        }

        public string Name { get; }

        public IEnumerable<Directory> AllDirectories =>
            (new List<Directory>() {this}).Concat(Items.OfType<Directory>().SelectMany(d => d.AllDirectories));

        public long Size => Items.Sum(i => i.Size);
        public List<IFileSystemItem> Items { get; }
        public Directory Parent { get; }
    }

    class File : IFileSystemItem
    {
        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public long Size { get; }
    }

}
