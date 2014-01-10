using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class IssueLists
    {
        public IssueLists(Connection oConnection)
        {
            m_oConnection = oConnection;
        }

        public void DisplayMyIssues()
        {
            Console.WriteLine("\nMy Issues");
            Console.WriteLine("=============\n");

            UserParameters oDetails = new UserParameters();
            Repositories oRepositories = m_oConnection.Repositories;
            Repository oRepos = oRepositories.ByName(oDetails.GetRepository());
            Issues oIssues = new Issues(oRepos);

            foreach (Issue oIssue in oIssues)
            {
                Console.WriteLine("Issue ID: " + oIssue.Ref + "\n" +
                                   "State: " + oIssue.State.Name + "\n" +
                                   "Description: " + oIssue.FieldByName("Description", true).Value + "\n\n");
            }
        }

        public void DisplayIssueViews()
        {
            Console.WriteLine("\nIssue Views");
            Console.WriteLine("=============\n");

            UserParameters oDetails = new UserParameters();

            Repositories oRepositories = m_oConnection.Repositories;
            Repository oRepos = oRepositories.ByName(oDetails.GetRepository());

            DisplayIssueViewFolderViews(oRepos, 0, "");
        }

        private void DisplayIssueViewFolderViews(Repository oRepos, uint nParentID, String strIndentation)
        {
            IssueViews oIssueViews = new IssueViews(oRepos, nParentID);

            foreach (IssueView oIssueView in oIssueViews)
            {
                Console.Write(strIndentation + oIssueView.Name + "\n");

                if (oIssueView.ViewType == SDK.TPCMIssueViewType.pcmIssueviewFolder)
                {
                    DisplayIssueViewFolderViews(oRepos, oIssueView.Id, strIndentation + "    ");
                }
            }
        }

        private Connection m_oConnection;
    }
}
