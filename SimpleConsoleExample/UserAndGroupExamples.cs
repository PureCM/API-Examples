using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class UserAndGroupExamples
    {
        public UserAndGroupExamples(Connection oConnection)
        {
            m_oConnection = oConnection;
        }


        public void AddUser( String strName)
        {
            Users oUsers = m_oConnection.Users;

            UserOrGroup oUser = oUsers.Add(strName, "secret", "One of the team", "jim@purecm.com", "", "", true, false);

            if (oUser != null)
            {
                Console.WriteLine("\nAdding User");
                Console.WriteLine("=============\n");


                Console.WriteLine("Added user : " + oUser.Name);
            }

        }

        public void DeleteUser( String strName )
        {
            Users oUsers = m_oConnection.Users;

            UserOrGroup oUser = oUsers.ByName(strName);

            if (oUser != null)
            {
                bool bDeleted = oUser.Delete();

                Console.WriteLine("\nDeleteing User");
                Console.WriteLine("===============\n");

                if (bDeleted)
                {
                    Console.WriteLine("Successfull delete");
                }
                else
                {
                    Console.WriteLine("Failed to delete ");
                }
            }
        }

        public void MapGroup( String strUserName, String strGroupName, bool bRemove )
        {
            Users oUsers = m_oConnection.Users;

            UserOrGroup oUser = oUsers.ByName(strUserName);

            if (oUser != null)
            {
                bool bMapped = oUser.MapUserGroup(strGroupName, bRemove);

                Console.WriteLine("\nMapping User to Group");
                Console.WriteLine("=======================\n");

                if (bMapped)
                {
                    Console.WriteLine("Successfull");
                }
                else
                {
                    Console.WriteLine("Failed ");
                }
            }
        }

        public void AddGroup( String strName)
        {
            Groups oGroups = m_oConnection.Groups;

            UserOrGroup oGroup = oGroups.Add(strName, "The first team");

            if (oGroup != null)
            {
                Console.WriteLine("\nAdding Group");
                Console.WriteLine("=============\n");


                Console.WriteLine("Added group : " + oGroup.Name);
            }

        }

        public void DeleteGroup( String strName )
        {
            Groups oGroups = m_oConnection.Groups;
            UserOrGroup oGroup = oGroups.ByName(strName);

            if ( oGroup != null )
            {
                bool bDeleted = oGroup.Delete();

                Console.WriteLine("\nDeleteing Group");
                Console.WriteLine("===============\n");

                if (bDeleted)
                {
                    Console.WriteLine("Successfull delete");
                }
                else
                {
                    Console.WriteLine("Failed to delete ");
                }
            }
        }

        private Connection m_oConnection;
    }
}
