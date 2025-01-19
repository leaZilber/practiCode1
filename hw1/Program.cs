//using System.CommandLine;
//using System.ComponentModel.Design;
//using System.Text.Encodings.Web;

//namespace hw1
//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            var dict = new Dictionary<string, string>
//            {
//            { "C#", ".cs" },
//            { "Java", ".java" },
//            { "JavaScript", ".js" },
//            { "Python", ".py" },
//            { "TypeScript", ".ts" }

//            };
//            var rootCommand = new RootCommand();
//            var bundle = new Command("bundle");
//            var createRsp = new Command("creat-rsp");
//            rootCommand.AddCommand(bundle);
//            var optionLanguage = new Option<string[]>(new[] { "--language", "--l" });
//            optionLanguage.AllowMultipleArgumentsPerToken = true;
//            var optionOutput = new Option<FileInfo>(new[] { "--output", "--o" });
//            var optionNote = new Option<bool>(new[] { "--note", "--n" });
//            var optionSort = new Option<string>(new[] { "--sort", "--s" });
//            var optionRemoveEmptyLines = new Option<bool>(new[] { "--Remove-empty-lines", "--rel" });
//            var optionAuthor = new Option<string>(new[] { "--author", "--a" });
//            bundle.AddOption(optionOutput);
//            bundle.AddOption(optionLanguage);
//            bundle.AddOption(optionNote);
//            bundle.AddOption(optionSort);
//            bundle.AddOption(optionAuthor);
//            bundle.AddOption(optionRemoveEmptyLines);
//            createRsp.AddGlobalOption(optionOutput);
//            rootCommand.AddCommand(createRsp);
//            bundle.SetHandler((Dictionary<string,string> language, FileInfo output, bool note, string sort, bool removeEmptyLines, string author) =>
//            {
//                string[] languages=language
//                if (languages == null || languages.Length == 0 || output == null || string.IsNullOrWhiteSpace(sort) || string.IsNullOrWhiteSpace(author))
//                {
//                    Console.WriteLine("Error, one or more parameters are missing.");
//                    return;
//                }
//                try
//                {
//                    List<string> Locations = new List<string>();
//                    if (languages[0] == "all")
//                    {
//                        Locations.AddRange(Directory.GetFiles(".\\", "*", SearchOption.AllDirectories));
//                    }
//                    else
//                    {
//                        foreach (var lang in languages)
//                        {
//                            Locations.AddRange(Directory.GetFiles(".\\", $"*.{lang}", SearchOption.AllDirectories));
//                        }
//                    }
//                    if (Locations.Count == 0)
//                    {
//                        Console.WriteLine("Error: There is no files to show");
//                        return;
//                    }
//                    if (sort == "abc")
//                    {
//                        Locations = Locations.OrderBy(l => l).ToList();
//                    }
//                    else if (sort == "lan")
//                    {
//                        Locations = Locations.OrderBy(l => Path.GetExtension(l)).ToList();
//                    }
//                    try
//                    {
//                        using (var fileText = File.CreateText(output.FullName))
//                        {
//                            fileText.WriteLine("//The author is: " + author);
//                            foreach (var location in Locations)
//                            {
//                                if (note)
//                                {
//                                    fileText.WriteLine("//file name: " + Path.GetFileName(location));
//                                    fileText.WriteLine("//file path: " + Path.GetFullPath(location));
//                                    fileText.WriteLine();//להתחלת שורה חדשה בקובץ
//                                }
//                                var context = File.ReadAllText(location);
//                                if (removeEmptyLines)
//                                {
//                                    context = string.Join(Environment.NewLine, context.Split('\n').Where(l => !string.IsNullOrEmpty(l)));
//                                }
//                                fileText.WriteLine(context);
//                                fileText.WriteLine();//להתחלת שורה חדשה בקובץ
//                            }
//                        }

//                    }
//                    catch (Exception ex)
//                    {
//                        throw new Exception($"Error, An error occurred, check the input {ex.Message}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception($"Error, try again {ex.Message}");
//                }
//            }, optionLanguage, optionOutput, optionNote, optionSort, optionRemoveEmptyLines, optionAuthor);

//            try
//            {
//                var responseFile = new FileInfo("responseFile.rsp");
//                Console.WriteLine("Enter values for the bundle command:");
//                using (StreamWriter rspWriter = new StreamWriter(responseFile.FullName))
//                {
//                    Console.Write("Output file path: ");
//                    var Output = Console.ReadLine();
//                    while (string.IsNullOrWhiteSpace(Output))
//                    {
//                        Console.Write("Enter the output file path: ");
//                        Output = Console.ReadLine();
//                    }
//                    rspWriter.WriteLine($"--output {Output}");
//                    Console.Write("Languages (comma-separated): ");
//                    var languages = Console.ReadLine();
//                    while (string.IsNullOrWhiteSpace(languages))
//                    {
//                        Console.Write("Please enter at least one programming language: ");
//                        languages = Console.ReadLine();
//                    }
//                    rspWriter.WriteLine($"--language {languages}");
//                    Console.Write("Add note (y/n): ");
//                    rspWriter.WriteLine(Console.ReadLine().Trim().ToLower() == "y" ? "--note" : "");
//                    Console.Write("Sort by (abc or language): ");
//                    rspWriter.WriteLine($"--sort {Console.ReadLine()}");
//                    Console.Write("Remove empty lines (y/n): ");
//                    rspWriter.WriteLine(Console.ReadLine().Trim().ToLower() == "y" ? "--remove-empty-lines" : "");
//                    Console.Write("Author: ");
//                    rspWriter.WriteLine($"--author {Console.ReadLine()}");
//                }
//                Console.WriteLine("Response file created successfully: " + responseFile.FullName);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error creating response file: " + ex.Message);
//            }
//            rootCommand.InvokeAsync(args).Wait();
//        }
//    }
//}

using System.CommandLine;
using System.ComponentModel.Design;
using System.Text.Encodings.Web;

namespace hw1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, string>
            {
                { "C#", ".cs" },
                { "Java", ".java" },
                { "JavaScript", ".js" },
                { "Python", ".py" },
                { "TypeScript", ".ts" }
            };

            var rootCommand = new RootCommand();
            var bundle = new Command("bundle");
            var createRsp = new Command("creat-rsp");
            rootCommand.AddCommand(bundle);
            var optionLanguage = new Option<string[]>(new[] { "--language", "--l" });
            optionLanguage.AllowMultipleArgumentsPerToken = true;
            var optionOutput = new Option<FileInfo>(new[] { "--output", "--o" });
            var optionNote = new Option<bool>(new[] { "--note", "--n" });
            var optionSort = new Option<string>(new[] { "--sort", "--s" });
            var optionRemoveEmptyLines = new Option<bool>(new[] { "--Remove-empty-lines", "--rel" });
            var optionAuthor = new Option<string>(new[] { "--author", "--a" });
            bundle.AddOption(optionOutput);
            bundle.AddOption(optionLanguage);
            bundle.AddOption(optionNote);
            bundle.AddOption(optionSort);
            bundle.AddOption(optionAuthor);
            bundle.AddOption(optionRemoveEmptyLines);
            createRsp.AddGlobalOption(optionOutput);
            rootCommand.AddCommand(createRsp);
            bundle.SetHandler((string[] languages, FileInfo output, bool note, string sort, bool removeEmptyLines, string author) =>
            {
                if (languages == null || languages.Length == 0 || output == null || string.IsNullOrWhiteSpace(sort) || string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Error, one or more parameters are missing.");
                    return;
                }

                try
                {
                    List<string> Locations = new List<string>();
                    if (languages[0].Equals("all", StringComparison.OrdinalIgnoreCase))
                    {
                        Locations.AddRange(Directory.GetFiles(".\\", "*.*", SearchOption.AllDirectories));
                    }
                    else
                    {
                        foreach (var lang in languages)
                        {
                            if (dict.TryGetValue(lang, out var extension))
                            {
                                Locations.AddRange(Directory.GetFiles(".\\", $"*{extension}", SearchOption.AllDirectories));
                            }
                            else
                            {
                                Console.WriteLine($"Warning: '{lang}' is not a recognized language. Skipping.");
                            }
                        }
                    }

                    if (Locations.Count == 0)
                    {
                        Console.WriteLine("Error: There are no files to show");
                        return;
                    }

                    if (sort == "abc")
                    {
                        Locations = Locations.OrderBy(l => l).ToList();
                    }
                    else if (sort == "lan")
                    {
                        Locations = Locations.OrderBy(l => Path.GetExtension(l)).ToList();
                    }

                    using (var fileText = File.CreateText(output.FullName))
                    {
                        fileText.WriteLine("//The author is: " + author);
                        foreach (var location in Locations)
                        {
                            if (note)
                            {
                                fileText.WriteLine("//File name: " + Path.GetFileName(location));
                                fileText.WriteLine("//File path: " + Path.GetFullPath(location));
                                fileText.WriteLine(); // Start a new line in the file
                            }

                            var context = File.ReadAllText(location);
                            if (removeEmptyLines)
                            {
                                context = string.Join(Environment.NewLine, context.Split('\n').Where(l => !string.IsNullOrEmpty(l)));
                            }
                            fileText.WriteLine(context);
                            fileText.WriteLine(); // Start a new line in the file
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error, try again: {ex.Message}");
                }
            }, optionLanguage, optionOutput, optionNote, optionSort, optionRemoveEmptyLines, optionAuthor);

            try
            {
                var responseFile = new FileInfo("responseFile.rsp");
                Console.WriteLine("Enter values for the bundle command:");
                using (StreamWriter rspWriter = new StreamWriter(responseFile.FullName))
                {
                    Console.Write("Output file path: ");
                    var Output = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(Output))
                    {
                        Console.Write("Enter the output file path: ");
                        Output = Console.ReadLine();
                    }
                    rspWriter.WriteLine($"--output {Output}");

                    Console.Write("Languages (comma-separated): ");
                    var languages = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(languages))
                    {
                        Console.Write("Please enter at least one programming language: ");
                        languages = Console.ReadLine();
                    }
                    rspWriter.WriteLine($"--language {languages}");

                    Console.Write("Add note (y/n): ");
                    rspWriter.WriteLine(Console.ReadLine().Trim().ToLower() == "y" ? "--note" : "");

                    Console.Write("Sort by (abc or language): ");
                    rspWriter.WriteLine($"--sort {Console.ReadLine()}");

                    Console.Write("Remove empty lines (y/n): ");
                    rspWriter.WriteLine(Console.ReadLine().Trim().ToLower() == "y" ? "--remove-empty-lines" : "");

                    Console.Write("Author: ");
                    rspWriter.WriteLine($"--author {Console.ReadLine()}");
                }
                Console.WriteLine("Response file created successfully: " + responseFile.FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating response file: " + ex.Message);
            }
            rootCommand.InvokeAsync(args).Wait();
        }
    }
}