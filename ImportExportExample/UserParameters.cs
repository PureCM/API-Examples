using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace ImportExportExample
{
    public class UserParameters
    {
        public String GetServerAddress()
        {
            if (m_strServer.Length == 0)
            {
                Console.Write("Enter Server Address: ");
                m_strServer = Console.ReadLine();
            }

            return m_strServer;
        }

        public int GetPort()
        {
            if (m_nPort == 0)
            {
                Console.Write("Enter Port: ");
                String strPort = Console.ReadLine();
                m_nPort = Convert.ToInt16(strPort);
            }

            return m_nPort;
        }

        public String GetUsername()
        {
            if (m_strUsername.Length == 0)
            {
                Console.Write("Enter username: ");
                m_strUsername = Console.ReadLine();
            }

            return m_strUsername;
        }

        public String GetPassword()
        {
            if (m_strPassword.Length == 0)
            {
                Console.Write("Enter password: ");
                m_strPassword = Console.ReadLine();
            }

            return m_strPassword;
        }

        public String GetRepository()
        {
            if (m_strRepository.Length == 0)
            {
                Console.Write("Enter Repository: ");
                m_strRepository = Console.ReadLine();
            }

            return m_strRepository;
        }

        public String GetStream()
        {
            if (m_strStream.Length == 0)
            {
                Console.Write("Enter Stream: ");
                m_strStream = Console.ReadLine();
            }

            return m_strStream;
        }

        public String GetNewStream()
        {
            if (m_strNewStream.Length == 0)
            {
                Console.Write("Enter New Stream Name: ");
                m_strNewStream = Console.ReadLine();
            }

            return m_strNewStream;
        }

        public String GetIssueType()
        {
            if (m_strIssueType.Length == 0)
            {
                Console.Write("Enter Issue Type: ");
                m_strIssueType = Console.ReadLine();
            }

            return m_strIssueType;
        }

        public void Reset()
        {
            m_strServer = "";
            m_nPort = 0;
            m_strUsername = "";
            m_strPassword = "";
            m_strRepository = "";
            m_strStream = "";
            m_strNewStream = "";
            m_strIssueType = "";
        }

        // Leave these blank and the console will ask for them
        String m_strServer = "localhost";
        int m_nPort = 2010;
        String m_strUsername = "LeadDeveloper1";
        String m_strPassword = "secret";
        String m_strRepository = "Example";
        String m_strStream = "Project 1/Version 1/Version 1";
        String m_strNewStream = "";
        String m_strIssueType = "defect";
    }
}
