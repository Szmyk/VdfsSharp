using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using NDesk.Options;

namespace VdfsSharp.Sample
{
    class Program
    {      
        static OptionSet options = new OptionSet()
             .Add("help", "shows this message", value => Options.ShowHelp = true)
             .Add("extract=", "extracts entries to directory", value => Options.ExtractPath = value)
             .Add("without-hierarchy", "extracts without hierarchy ", value => Options.WithHierarchy = false)
             .Add("print-tree", "prints entries tree in console", value => Options.PrintTree = true)
             .Add("save-tree=", "saves entries tree to file", value => Options.SaveTree = value);

        static void PrintHelp ()
        {
            var exeFileName = Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Console.WriteLine(
                "VdfsSharp Sample" + Environment.NewLine + Environment.NewLine + 
                "Usage: `" + exeFileName +  " [VDF archive] [<arguments>]`" + Environment.NewLine +
                "Example: `" + exeFileName + " Anims.vdf --extract=archives\\Anims`" + Environment.NewLine + Environment.NewLine + 
                "Arguments:"
            );

            options.WriteOptionDescriptions(Console.Out);
        }

        static void Main(string[] args)
        {
            List<string> extra;

            try
            {
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);

                return;
            }

            if (Options.ShowHelp || extra.Count == 0)
            {
                PrintHelp();
            }
            else
            {
                var filePath = extra[0];

                if (File.Exists(filePath) == false)
                {
                    Console.WriteLine("File `{0}` do not exists!", filePath);

                    return;
                }

                var reader = new VdfsReader(filePath);

                if (Options.ExtractPath != string.Empty)
                {
                    Console.WriteLine("Extracting archive `{0}` to directory `{1}` (with hierarchy: {2}).", filePath, Options.ExtractPath, Options.WithHierarchy);

                    using (var extractor = new VdfsExtractor(reader))
                    {
                        extractor.ExtractFiles(Options.ExtractPath,
                         ( Options.WithHierarchy == true )
                         ? ExtractOption.Hierarchy
                         : ExtractOption.NoHierarchy);

                        Console.WriteLine("Done.");
                    }                   
                }           
                else if (Options.PrintTree || Options.SaveTree != string.Empty)
                {
                    var entries = reader.ReadEntries(false).ToArray();

                    var treeGenerator = new VdfsEntriesTreeGenerator(entries);

                    var tree = treeGenerator.Generate();

                    var treeView = tree.GetTreeView();

                    if (Options.PrintTree)
                    {
                        Console.WriteLine();
                        Console.Write(treeView);
                    }
                    else
                    {
                        File.WriteAllText(Options.SaveTree, treeView);

                        Console.WriteLine("Entries tree saved to file `{0}`", Options.SaveTree);
                    }                   
                }
                else
                {
                    PrintHelp();
                }
            }

            return;
        }
    }

    static class Options
    {
        public static bool ShowHelp = false,
                           PrintTree = false,
                           WithHierarchy = true;

        public static string SaveTree = string.Empty,
                             ExtractPath = string.Empty;
    }
}
