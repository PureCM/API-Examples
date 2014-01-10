using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            //Create instance of this class
            ConsoleApp oTestProgram = new ConsoleApp();
            UserParameters oDetails = new UserParameters();

            Console.WriteLine("PureCM .NET Simple Console Application");
            Console.WriteLine("This application demostrates using the .NET API for non-workspace commands");
            Console.WriteLine("Type 'help' for list of commands");

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
                                                                    oDetails.GetPassword(),
                                                                    0 );

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

                            break;
                        }
                    case "connectreal":
                        {
                            //Setup PureCM connection
                            const int PCM_CONNOPT_USE_USERDB = 0x00000002;

                            Connection oConnection = new Connection(oDetails.GetServerAddress(),
                                                                    oDetails.GetPort(),
                                                                    SDK.TPCMAuthType.pcmauthPassword,
                                                                    oDetails.GetUsername(),
                                                                    oDetails.GetPassword(),
                                                                    PCM_CONNOPT_USE_USERDB );

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
                case "changesets":
                    {
                        UserParameters oDetails = new UserParameters();

                        Repositories oRepositories = oConnection.Repositories;

                        Repository oRepos = oRepositories.ByName(oDetails.GetRepository());

                        if (oRepos == null)
                        {
                            Console.Write("The repository '" + oDetails.GetRepository() + "' does not exist\n\n");
                            break;
                        }

                        ChangesetLists oChanges = new ChangesetLists(oConnection, oRepos);

                        //Get the complete list of Streams
                        Streams oStreams = oRepos.Streams;

                        //Locate the correct stream EG. Development/Main
                        Stream oStream = oStreams.ByPath(oDetails.GetStream());

                        if (oStream == null)
                        {
                            Console.Write("The stream '" + oDetails.GetStream() + "' does not exist\n\n");
                            break;
                        }

                        oChanges.DisplayChangesets(oStream);
                        break;
                    }
                case "connect":
                    {
                        Console.WriteLine("You are already connected to the server");
                        break;
                    }
                case "connections":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayConnections();
                        break;
                    }
                case "events":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayEvents();
                        break;
                    }
                case "filetypes":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayFileTypes();
                        break;
                    }
                case "groups":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayGroups();
                        break;
                    }
                case "issueactions":
                    {
                        IssueAdminLists oIssueAdminLists = new IssueAdminLists(oConnection);
                        UserParameters oDetails = new UserParameters();
                        IssueTypes oIssueTypes = oConnection.IssueTypes;
                        IssueType oIssueType = oIssueTypes.ByName(oDetails.GetIssueType());

                        if (oIssueType != null)
                        {
                            oIssueAdminLists.DisplayIssueActions(oIssueType);
                        }
                        break;
                    }
                case "issuefields":
                    {
                        IssueAdminLists oIssueAdminLists = new IssueAdminLists(oConnection);
                        UserParameters oDetails = new UserParameters();
                        IssueTypes oIssueTypes = oConnection.IssueTypes;
                        IssueType oIssueType = oIssueTypes.ByName(oDetails.GetIssueType());

                        if (oIssueType != null)
                        {
                            oIssueAdminLists.DisplayIssueFields(oIssueType);
                        }
                        break;
                    }
                case "issuestates":
                    {
                        IssueAdminLists oIssueAdminLists = new IssueAdminLists(oConnection);
                        UserParameters oDetails = new UserParameters();
                        IssueTypes oIssueTypes = oConnection.IssueTypes;
                        IssueType oIssueType = oIssueTypes.ByName(oDetails.GetIssueType());

                        if (oIssueType != null)
                        {
                            oIssueAdminLists.DisplayIssueStates(oIssueType);
                        }
                        break;
                    }
                case "issuetypes":
                    {
                        IssueAdminLists oIssueAdminLists = new IssueAdminLists(oConnection);

                        oIssueAdminLists.DisplayIssueTypes();
                        break;
                    }
                case "issueviews":
                    {
                        IssueLists oIssues = new IssueLists(oConnection);

                        oIssues.DisplayIssueViews();
                        break;
                    }
                case "licenses":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayLicenses();
                        break;
                    }
                case "myissue":
                    {
                        IssueLists oIssues = new IssueLists(oConnection);

                        oIssues.DisplayMyIssues();
                        break;
                    }
                case "policysets":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayPolicies();
                        break;
                    }
                case "repos-filetypes":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);
                        UserParameters oDetails = new UserParameters();
                        Repositories oRepositories = oConnection.Repositories;
                        Repository oRepos = oRepositories.ByName(oDetails.GetRepository());

                        if (oRepos == null)
                        {
                            Console.Write("The repository '" + oDetails.GetRepository() + "' does not exist\n\n");
                            break;
                        }

                        oAdminLists.DisplayReposFileTypes(oRepos);
                        break;
                    }
                case "users":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.DisplayUsers();
                        break;
                    }

                case "adduser":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strNewUser = oDetails.GetTestUser();

                        oUserAndGroupExamples.AddUser(strNewUser);
                        break;
                    }

                case "deleteuser":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strUser = oDetails.GetTestUser();

                        oUserAndGroupExamples.DeleteUser(strUser);
                        break;
                    }

                case "mapgroup":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strUser = oDetails.GetTestUser();
                        String strGroup = oDetails.GetTestGroup();

                        oUserAndGroupExamples.MapGroup(strUser, strGroup, false);
                        break;
                    }

                case "removemapgroup":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strUser = oDetails.GetTestUser();
                        String strGroup = oDetails.GetTestGroup();

                        oUserAndGroupExamples.MapGroup(strUser, strGroup, true);
                        break;
                    }

                case "addgroup":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strNewGroup = oDetails.GetTestGroup();

                        oUserAndGroupExamples.AddGroup(strNewGroup);
                        break;
                    }

                case "deletegroup":
                    {
                        UserAndGroupExamples oUserAndGroupExamples = new UserAndGroupExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strGroup = oDetails.GetTestGroup();

                        oUserAndGroupExamples.DeleteGroup(strGroup); 
                        break;
                    }

                case "addworkspace":
                    {
                        WorkspaceExamples oWorkspaceExamples = new WorkspaceExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        Repositories oRepositories = oConnection.Repositories;
                        Repository oRepos = oRepositories.ByName(oDetails.GetRepository());

                        if (oRepos == null)
                        {
                            Console.Write("The repository '" + oDetails.GetRepository() + "' does not exist\n\n");
                            break;
                        }

                        Streams oStreams = oRepos.Streams;
                        Stream oStream = oStreams.ByPath(oDetails.GetStream());
                        String strWSPath = oDetails.GetWSPath();

                        oWorkspaceExamples.AddWorkspace(oStream, strWSPath);
                        break;
                    }
                case "deleteworkspace":
                    {
                        WorkspaceExamples oWorkspaceExamples = new WorkspaceExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strWSPath = oDetails.GetWSPath();
                        Workspace oWS = ConnectionFactory.WorkspaceForPath(strWSPath);

                        oWorkspaceExamples.DeleteWorkspace(oWS);
                        break;
                    }

                case "syncworkspace":
                    {
                        WorkspaceExamples oWorkspaceExamples = new WorkspaceExamples(oConnection);
                        UserParameters oDetails = new UserParameters();
                        String strWSPath = oDetails.GetWSPath();
                        Workspace oWS = ConnectionFactory.WorkspaceForPath(strWSPath);

                        oWorkspaceExamples.SyncWorkspace(oWS);
                        break;
                    }


                case "refreshconnection":
                    {
                        AdminLists oAdminLists = new AdminLists(oConnection);

                        oAdminLists.Refresh();

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

            arrStrCommands[nCommandNo] = "groups";
            arrStrDescriptions[nCommandNo++] = "Displays a list of all the groups you have.";

            arrStrCommands[nCommandNo] = "users";
            arrStrDescriptions[nCommandNo++] = "Display a list of all the users you have.";

            arrStrCommands[nCommandNo] = "issueviews";
            arrStrDescriptions[nCommandNo++] = "List all the issue views.";

            arrStrCommands[nCommandNo] = "myissue";
            arrStrDescriptions[nCommandNo++] = "List all the current issues.";

            arrStrCommands[nCommandNo] = "changesets";
            arrStrDescriptions[nCommandNo++] = "List all the Changesets for a Stream.";

            arrStrCommands[nCommandNo] = "licenses";
            arrStrDescriptions[nCommandNo++] = "List all the registered licenses.";

            arrStrCommands[nCommandNo] = "connections";
            arrStrDescriptions[nCommandNo++] = "List all the currently connected clients.";

            arrStrCommands[nCommandNo] = "events";
            arrStrDescriptions[nCommandNo++] = "List all the events in the PureCM Event Log.";

            arrStrCommands[nCommandNo] = "filetypes";
            arrStrDescriptions[nCommandNo++] = "List all the global file types.";

            arrStrCommands[nCommandNo] = "policysets";
            arrStrDescriptions[nCommandNo++] = "List all the policysets.";

            arrStrCommands[nCommandNo] = "repos-filetypes";
            arrStrDescriptions[nCommandNo++] = "List all the file types in the specified repository.";

            arrStrCommands[nCommandNo] = "issuetypes";
            arrStrDescriptions[nCommandNo++] = "List all the issue types.";

            arrStrCommands[nCommandNo] = "issueactions";
            arrStrDescriptions[nCommandNo++] = "List all the issue actions.";

            arrStrCommands[nCommandNo] = "issuestates";
            arrStrDescriptions[nCommandNo++] = "List all the issue states.";

            arrStrCommands[nCommandNo] = "issuefields";
            arrStrDescriptions[nCommandNo++] = "List all the issue fields.";

            arrStrCommands[nCommandNo] = "adduser";
            arrStrDescriptions[nCommandNo++] = "Add a new user";

            arrStrCommands[nCommandNo] = "deleteuser";
            arrStrDescriptions[nCommandNo++] = "Delete a user";

            arrStrCommands[nCommandNo] = "addgroup";
            arrStrDescriptions[nCommandNo++] = "Add a new group";

            arrStrCommands[nCommandNo] = "deletegroup";
            arrStrDescriptions[nCommandNo++] = "Delete a group";

            arrStrCommands[nCommandNo] = "mapgroup";
            arrStrDescriptions[nCommandNo++] = "map a user to a group";

            arrStrCommands[nCommandNo] = "removemapgroup";
            arrStrDescriptions[nCommandNo++] = "remove a user from a group";

            arrStrCommands[nCommandNo] = "addworkspace";
            arrStrDescriptions[nCommandNo++] = "Create a new workspace";

            arrStrCommands[nCommandNo] = "deleteworkspace";
            arrStrDescriptions[nCommandNo++] = "Delete a workspace";

            arrStrCommands[nCommandNo] = "syncworkspace";
            arrStrDescriptions[nCommandNo++] = "Synchronise a workspace";

            arrStrCommands[nCommandNo] = "refreshconnection";
            arrStrDescriptions[nCommandNo++] = "Refresh a connection";

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
