using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class WorkspaceExamples
    {
        public WorkspaceExamples(Connection oConnection)
        {
            m_oConnection = oConnection;
        }

        public void AddWorkspace(Stream oStream, String strWSPath)
        {
            bool bsuccess = oStream.CreateWorkspace("", strWSPath, "");

        }

        public void DeleteWorkspace(Workspace oWS)
        {
            bool bsuccess = oWS.Delete(true, true);
        }

        public void SyncWorkspace(Workspace oWS)
        {
            bool bsuccess = oWS.Synchronise("Bygfoot/3");
        }

        private Connection m_oConnection;
    }
}
