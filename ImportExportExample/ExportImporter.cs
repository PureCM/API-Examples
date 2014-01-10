using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using PureCM.Client;

namespace ImportExportExample
{
    class ExportImporter
    {
        public ExportImporter(PureCM.Client.Stream oExportStream, PureCM.Client.Stream oImportStream, UInt32 nConnectionID)
        {
            m_oExportStream = oExportStream;
            m_oImportStream = oImportStream;
            m_nConnectionID = nConnectionID;
        }

        public bool DoExportImport()
        {
            String strTempDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
            String strExportDir = System.IO.Path.Combine(strTempDir, "export");
            Workspace oImportWS;
            Workspace oExportWS;

            if (!CreateWorkspace(m_oExportStream, strExportDir, out oExportWS ))
            {
                return false;
            }

            String strImportDir = System.IO.Path.Combine(strTempDir, "import");

            if (!CreateWorkspace(m_oImportStream, strImportDir, out oImportWS ))
            {
                return false;
            }

            foreach(Changeset oExportChange in oExportWS.IntegratedChangesets )
            {
                if (oExportWS.Synchronise(oExportChange.IdString))
                {
                    Console.WriteLine("Synchronized to changeset '" + oExportChange.IdString + "'.");

                    // Copy all files from export workspace into import workspace
                    CopyFiles(strExportDir, strImportDir);

                    // Check consistent will check which files have been added, edited and deleted and will
                    // check them out as appropriate
                    if (oImportWS.CheckConsistency())
                    {
                        foreach (LocalChangeset oImportChange in oImportWS.LocalChangesets )
                        {
                            if (oImportChange.SubmitAsImport(oExportChange.Description, oExportChange.ClientName, oExportChange.Timestamp))
                            {
                                Console.WriteLine("Submitted " + oExportChange.IdString + " '" + oExportChange.Description + "'");
                            }
                            else
                            {
                                Console.WriteLine("Failed to submit changeset '" + oExportChange.IdString + "'. The change has not be imported.");
                            }

                            SDK.TPCMReturnCode tRetCode;

                            oImportWS.UpdateToLatest(out tRetCode);

                            if (tRetCode != SDK.TPCMReturnCode.pcmSuccess)
                            {
                                Console.WriteLine("Failed to update to latest after submitting '" + oExportChange.IdString + "'.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to check consistency for changeset '" + oExportChange.IdString + "'. The change will not be imported.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to synchronize to changeset '" + oExportChange.IdString + "'. The change will not be imported.");
                }
            }

            return true;
        }

        private bool CreateWorkspace(PureCM.Client.Stream oStream, String strPath, out Workspace oNewWS)
        {
            oNewWS = null;

            if ( !oStream.CreateWorkspace( "", strPath, "ExportImport Workspace" ) )
            {
                Console.WriteLine("Failed to create workspace");
                return false;
            }

            Workspaces oWSs = oStream.Repository.Workspaces;

            foreach (Workspace oWS in oWSs)
            {
                if (oWS.ManagesPath(strPath))
                {
                    oNewWS = oWS;
                    return true;
                }
            }

            Console.WriteLine("Failed to find created workspace!");
            return false;
        }

        private static void CopyFiles(String strSrcDir, String strDstDir)
        {
            DirectoryInfo oSrcInfo = new DirectoryInfo(strSrcDir);
            DirectoryInfo oDstInfo = new DirectoryInfo(strDstDir);

            DeleteAll(oDstInfo);
            CopyAll(oSrcInfo, oDstInfo);
        }

        public static void CopyAll(DirectoryInfo oSrcInfo, DirectoryInfo oDstInfo)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(oSrcInfo.FullName) == false)
            {
                Directory.CreateDirectory(oDstInfo.FullName);
            }

            // Copy each file
            foreach (FileInfo oSrcFileInfo in oSrcInfo.GetFiles())
            {
                oSrcFileInfo.CopyTo(Path.Combine(oDstInfo.FullName, oSrcFileInfo.Name), true);
            }

            // Copy each directory
            foreach (DirectoryInfo oSrcDirInfo in oSrcInfo.GetDirectories())
            {
                // _purecm is a special directory which contains the workspace database
                if (oSrcDirInfo.Name != "_purecm")
                {
                    DirectoryInfo oDstDirInfo = oDstInfo.CreateSubdirectory(oSrcDirInfo.Name);
                    CopyAll(oSrcDirInfo, oDstDirInfo);
                }
            }
        }

        public static void DeleteAll(DirectoryInfo oDirInfo)
        {
            // Delete each file
            foreach (FileInfo oFileInfo in oDirInfo.GetFiles())
            {
                if (oFileInfo.IsReadOnly )
                {
                    oFileInfo.IsReadOnly = false;
                }
                oFileInfo.Delete();
            }

            // Delete each directory
            foreach (DirectoryInfo oSubDirInfo in oDirInfo.GetDirectories())
            {
                // _purecm is a special directory which contains the workspace database
                if (oSubDirInfo.Name != "_purecm")
                {
                    DeleteAll(oSubDirInfo);
                    oSubDirInfo.Delete();
                }
            }
        }

        private bool RunCommand( String strCommand )
        {
            Console.WriteLine( strCommand );
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(strCommand);

            psi.RedirectStandardOutput = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.UseShellExecute = false;

            System.Diagnostics.Process listFiles = System.Diagnostics.Process.Start(psi);
            System.IO.StreamReader myOutput = listFiles.StandardOutput;

            listFiles.WaitForExit();

            Console.Write(myOutput.ReadToEnd());

            if (listFiles.ExitCode == 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Failed to perform command (" + listFiles.ExitCode + ")");
                return false;
            }
        }

        PureCM.Client.Stream m_oExportStream;
        PureCM.Client.Stream m_oImportStream;
        UInt32 m_nConnectionID;
    }
}
