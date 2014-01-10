using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace ImportExportExample
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            //Create instance of this class
            ConsoleApp oTestProgram = new ConsoleApp();
            UserParameters oDetails = new UserParameters();

            Console.WriteLine("PureCM .NET Import-Export Console Application");
            Console.WriteLine("This application demonstrates exporting all changes from a stream and importing them into another stream");

            String strUserInput;

            bool bExit = false;

            do
            {
                strUserInput = Console.ReadLine();

                strUserInput = strUserInput.ToLower();

                switch (strUserInput)
                {
                    case "connect":
                        {
                            //Setup PureCM connection
                            Connection oConnection = new Connection(oDetails.GetServerAddress(),
                                                                    oDetails.GetPort(),
                                                                    SDK.TPCMAuthType.pcmauthPassword,
                                                                    oDetails.GetUsername(),
                                                                    oDetails.GetPassword());

                            if (oConnection != null)
                            {
                                if (oConnection.Connect())
                                {
                                    Console.WriteLine("Successfully Connected to " + oConnection.ConnectionString);

                                    do
                                    {
                                        strUserInput = Console.ReadLine();
                                        strUserInput = strUserInput.ToLower();

                                        bExit = oTestProgram.ProcessUserCommand(oConnection, strUserInput);

                                    } while (bExit == false);
                                }
                                else
                                {
                                    Console.WriteLine("Unable to connect to " + oConnection.ConnectionString);
                                    oDetails.Reset();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unable to connect. Have you specified a default connection?");
                                oDetails.Reset();
                            }

                            break;
                        }
                    case "help":
                        {
                            oTestProgram.DisplayHelp();
                            break;
                        }
                    case "exit":
                        {
                            bExit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please Connect to the server before issuing any commands...");
                            break;
                        }
                }

            } while (bExit == false);


            Console.WriteLine("\n\nPress any key to terminate the program......");
            Console.ReadKey();
            Environment.Exit(0);
        }

        bool ProcessUserCommand(Connection oConnection, String strUserInput)
        {
            bool bExit = false;

            switch (strUserInput)
            {
                case "export-import":
                    {
                        UserParameters oDetails = new UserParameters();
                        Repository oRepos = oConnection.Repositories.ByName(oDetails.GetRepository());

                        if (oRepos == null)
                        {
                            Console.Write("The repository '" + oDetails.GetRepository() + "' does not exist\n\n");
                            break;
                        }

                        Stream oExportStream = oRepos.Streams.ByPath(oDetails.GetStream());

                        if (oExportStream == null)
                        {
                            Console.Write("The export stream '" + oDetails.GetStream() + "' does not exist\n\n");
                            break;
                        }

                        Stream oImportStream = oRepos.Streams.ByPath(oDetails.GetNewStream());

                        if (oImportStream == null)
                        {
                            oImportStream = oRepos.Streams.CreateNew(oRepos.Streams.ById(oExportStream.ParentId), null, null, oDetails.GetNewStream(), "Imported from stream '" + oExportStream.Name + "'");

                            if (oImportStream == null)
                            {
                                Console.Write("Failed to create import stream '" + oDetails.GetNewStream() + "'. Have you specified a valid stream name?\n\n");
                                break;
                            }
                        }
                        else
                        {
                            // Currently do not support importing to an existing stream
                            Console.Write("The import stream '" + oDetails.GetNewStream() + "' already exists\n\n");
                            break;
                        }

                        ExportImporter oExportImporter = new ExportImporter(oExportStream, oImportStream, oConnection.Id);

                        if (oExportImporter.DoExportImport())
                        {
                            Console.Write("Stream '" + oExportStream.Name + "' has been successfully imported into '" + oImportStream.Name + "'\n\n");
                        }
                        else
                        {
                            Console.Write("Failed to export '" + oExportStream.Name + "' into '" + oImportStream.Name + "'\n\n");
                        }
                        break;
                    }
                case "help":
                    {
                        DisplayHelp();
                        break;
                    }
                case "exit":
                    {
                        bExit = true;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unrecognised Command : " + strUserInput);
                        break;
                    }
            }

            return bExit;
        }

        void DisplayHelp()
        {
            Console.WriteLine("List of Commands");
            Console.WriteLine("================");

            String[] arrStrCommands = new String[100];
            String[] arrStrDescriptions = new String[100];
            int nCommandNo = 0;

            arrStrCommands[nCommandNo] = "connect";
            arrStrDescriptions[nCommandNo++] = "Connect to the Server.";

            arrStrCommands[nCommandNo] = "exit";
            arrStrDescriptions[nCommandNo++] = "Exit Program";

            for (int i = 0; i < arrStrCommands.Length; i++)
            {
                if (arrStrCommands[i] != null)
                {
                    Console.WriteLine("Command: " + arrStrCommands[i] + "\nDescription: " + arrStrDescriptions[i] + "\n");
                }
            }
        }
    }
}
