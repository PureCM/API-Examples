using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureCM.Client;

namespace API_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("This application demonstrates basic PureCM.Client functionality");
                Console.WriteLine("Usage : pcm_api checkout [file_name]");
                return;
            }

            if ((args[0] == "checkout") && (args.Count() == 2))
            {
                CheckoutFile(args[1]);
            }

            Environment.Exit(0);
        }

        static void CheckoutFile(String strFile)
        {
            Workspace oWorkspace = ConnectionFactory.WorkspaceForPath(strFile);

            if (oWorkspace == null)
            {
                Console.WriteLine("Error : '" + strFile + "' is not contained within a workspace");
                return;
            }

            WorkspaceTxn oWorkspaceTxn = oWorkspace.BeginTransaction();

            if (oWorkspaceTxn == null)
            {
                Console.WriteLine("Error : Failed to create workspace transaction");
                return;
            }

            TWSTxnResult tRet = oWorkspaceTxn.CheckoutFile(strFile);

            if (tRet != TWSTxnResult.Success)
            {
                OutputFailure(tRet, strFile);
                return;
            }

            oWorkspaceTxn.Commit();
        }

        static void OutputFailure(TWSTxnResult tRet, String strFile)
        {
            switch (tRet)
            {
                case TWSTxnResult.AlreadyCheckedOut:
                    Console.WriteLine("Error : '" + strFile + "' is already checked out");
                    break;
                case TWSTxnResult.CheckedOutExclusive:
                    Console.WriteLine("Error : '" + strFile + "' is locked by another user");
                    break;
                case TWSTxnResult.UnderReview:
                    Console.WriteLine("Error : '" + strFile + "' is being reviewed. You must wait for the review to be accepted and update your workspace.");
                    break;
                case TWSTxnResult.NotControlled:
                    Console.WriteLine("Error : '" + strFile + "' has not been added to source control.");
                    break;
                case TWSTxnResult.Failure:
                default:
                    Console.WriteLine("Error : '" + strFile + "' has not been checked out. Unspecified failure. Please check the event log.");
                    break;
            }
        }
    }
}
