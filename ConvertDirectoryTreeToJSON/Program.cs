﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ConvertDirectoryTreeToJSON
{
    class DynatreeItem
    {
        public string label { get; set; }
        public bool isFolder { get; set; }
        public string key { get; set; }
        public List<DynatreeItem> children { get; set; }//child directory tree
        public DynatreeItem(FileSystemInfo fsi)
        {
            label = fsi.Name;
            children = new List<DynatreeItem>();

            if (fsi.Attributes == FileAttributes.Directory)
            {
                isFolder = true;
                foreach (FileSystemInfo f in (fsi as DirectoryInfo).GetFileSystemInfos())
                {
                    children.Add(new DynatreeItem(f));
                }
            }
            else
            {
                isFolder = false;
            }
            key = label.Replace(" ", "").ToLower();
        }

        public string JsonToDynatree()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            DynatreeItem di = new DynatreeItem(new DirectoryInfo(@"C:\JixGe\soft"));
            //string result = "[" + di.JsonToDynatree() + "]";
            string result = "[" + JsonConvert.SerializeObject(di, Formatting.Indented) + "]";
            
            Console.WriteLine(result);
        }

    }

    //class Program{
    //    public class Node
    //    {
    //        public Node() { Nodes = new List<Node>(); }
    //        public Node(string fileName)
    //        {
    //            Nodes = new List<Node>();
    //            Data = fileName;
    //        }

    //        public Node FindNode(string data)
    //        {
    //            if (this.Nodes == null || !this.Nodes.Any()) { return null; }

    //            // check Node list to see if there are any that already exist
    //            return this.Nodes
    //                .FirstOrDefault(n => String.Equals(n.Data, data, StringComparison.CurrentCultureIgnoreCase));
    //        }

    //        public string Data { get; set; }
    //        public List<Node> Nodes { get; set; }
    //    }

    //    public static Node AddNode(string filePath, Node rootNode)
    //    {
    //        // convenience method. this creates the queue that we need for recursion from the filepath for you
    //        var tokens = filePath.Split('\\').ToList();

    //        // if you split a folder ending with / it leaves an empty string at the end and we want to remove that
    //        if (String.IsNullOrWhiteSpace(tokens.Last())) { tokens.Remove(""); }

    //        return AddNode(new Queue<string>(tokens), rootNode);
    //    }

    //    private static Node AddNode(Queue<string> tokens, Node rootNode)
    //    {
    //        // base case -> node wasnt found and tokens are gone  :(
    //        if (tokens == null || !tokens.Any())
    //        {
    //            return null;
    //        }

    //        // get current token, leaving only unsearched ones in the tokens object
    //        string current = tokens.Dequeue();

    //        // create node if not already exists
    //        Node foundNode = rootNode.FindNode(current);
    //        if (foundNode != null)
    //        {
    //            // node exists! recurse
    //            return AddNode(tokens, foundNode);
    //        }
    //        else
    //        {
    //            // node doesnt exist! add it manually and recurse
    //            Node newNode = new Node() { Data = current };
    //            rootNode.Nodes.Add(newNode);
    //            return AddNode(tokens, newNode);
    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        Node root = new Node(@"C:\JixGe\soft");
    //        AddNode("New Text Document.txt", root);
    //        AddNode(@"New folder\", root);
    //        AddNode(@"New folder\README.txt", root);
    //        Console.WriteLine(JsonConvert.SerializeObject(root, Formatting.Indented));
    //        Console.ReadKey();
    //    }
    //}
    

    
}
