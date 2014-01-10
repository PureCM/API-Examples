using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class AdminLists
    {
        public AdminLists(Connection oConnection)
        {
            m_oConnection = oConnection;
        }

        public void DisplayUsers()
        {
            Users oUsers = m_oConnection.Users;

            Console.WriteLine("\nList of Client Users");
            Console.WriteLine("====================\n");

            if (oUsers.Count > 0)
            {
                foreach (UserOrGroup oUser in oUsers)
                {
                    Console.WriteLine("Username: " + oUser.Name);
                }
            }
        }

        public void DisplayGroups()
        {
            Groups oGroups = m_oConnection.Groups;

            Console.WriteLine("\nList of Client Groups");
            Console.WriteLine("====================\n");

            if (oGroups.Count > 0)
            {
                foreach (UserOrGroup oGroup in oGroups)
                {
                    Console.WriteLine("Group Name: " + oGroup.Name);
                }
            }
        }

        public void Refresh()
        {
            m_oConnection.RefreshConnection();
        }

        public void DisplayLicenses()
        {
            Licenses oLicenses = m_oConnection.Licenses;

            Console.WriteLine("\nLicenses");
            Console.WriteLine("=========\n");

            if (oLicenses.Count > 0)
            {
                foreach (License oLicense in oLicenses)
                {
                    Console.WriteLine("License Name: " + oLicense.Name);
                    Console.WriteLine("Keys: " + oLicense.Key);
                    Console.WriteLine("Users: " + oLicense.Users);
                }
            }
        }

        public void DisplayConnections()
        {
            ClientConnections oConns = m_oConnection.ClientConnections;

            Console.WriteLine("\nConnections");
            Console.WriteLine("=============\n");

            if (oConns.Count > 0)
            {
                foreach (ClientConnection oConn in oConns)
                {
                    Console.WriteLine("Name: " + oConn.Name);
                    Console.WriteLine("Client: " + oConn.Client);
                    Console.WriteLine("IP Address: " + oConn.IPAddress);
                }
            }
        }

        public void DisplayEvents()
        {
            Console.WriteLine("\nEvents");
            Console.WriteLine("=============\n");

            Events oEvents = m_oConnection.Events;

            foreach (Event oEvent in oEvents)
            {
                Console.Write(oEvent.Id + " : " +
                              oEvent.User + " : " +
                              oEvent.Date + "\n" + oEvent.Description + "\n------------------------------\n");
            }
        }

        public void DisplayFileTypes()
        {
            Console.WriteLine("\nFile Types");
            Console.WriteLine("=============\n");

            FileTypes oFileTypes = m_oConnection.FileTypes;

            foreach (FileType oFileType in oFileTypes)
            {
                Console.Write(oFileType.Type + " (" + oFileType.Flags + ")\nDescription:\n" +
                              oFileType.Description + "\nFilters:\n" + oFileType.Filters +
                              "\n------------------------------\n");

            }
        }

        public void DisplayReposFileTypes(Repository oRepos)
        {
            Console.WriteLine("\nRepos File Types");
            Console.WriteLine("=============\n");

            FileTypes oFileTypes = oRepos.FileTypes;

            foreach (FileType oFileType in oFileTypes)
            {
                Console.Write(oFileType.Type + "(" + oFileType.Flags + ")\nDescription:\n" +
                              oFileType.Description + "\nFilters:\n" + oFileType.Filters +
                              "\n------------------------------\n");

            }
        }

        public void DisplayPolicies()
        {
            Console.WriteLine("\nPolicies");
            Console.WriteLine("=============\n");

            PolicySets oPolicySets = m_oConnection.PolicySets;

            foreach (PolicySet oPolicySet in oPolicySets)
            {
                Console.WriteLine("Name: " + oPolicySet.Name);
                Console.WriteLine("Description: " + oPolicySet.Description);

                Repository oRepos = null;
                Stream oStream = null;

                if (oPolicySet.ReposID > 0)
                {
                    oRepos = m_oConnection.Repositories.ById(oPolicySet.ReposID);

                    if ( (oRepos != null) && (oPolicySet.StreamID > 0) )
                    {
                        oStream = oRepos.Streams.ById(oPolicySet.StreamID);
                    }
                }

                if (oRepos != null)
                {
                    Console.WriteLine("Repos: " + oRepos.Name);
                }
                else
                {
                    Console.WriteLine("Repos: None");
                }

                if (oStream != null)
                {
                    Console.WriteLine("Stream: " + oStream.Name);
                }
                else
                {
                    Console.WriteLine("Stream: None");
                }

                UserOrGroup oUser = null;

                if (oPolicySet.UserID > 0)
                {
                    oUser = m_oConnection.Users.ById(oPolicySet.UserID);

                    if (oUser == null)
                    {
                        // Might be a group instead?
                        oUser = m_oConnection.Groups.ById(oPolicySet.UserID);
                    }
                }

                if (oUser != null)
                {
                    Console.WriteLine("User: " + oUser.Name);
                }
                else
                {
                    Console.WriteLine("User: None");
                }

                DisplayPolicysetPolicies(oPolicySet);
            }
        }

        private void DisplayPolicysetPolicies( PolicySet oPolicySet )
        {
            Console.WriteLine("Policies:");

            Policies oPolicies = oPolicySet.Policies;

            foreach (Policy oPolicy in oPolicies)
            {
                Console.WriteLine("    " + oPolicy.Name );
            }
        }

        private Connection m_oConnection;
    }
}
