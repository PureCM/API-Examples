using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class ChangesetLists
    {
        public ChangesetLists(Connection oConnection, Repository oRepos)
        {
            m_oConnection = oConnection;
            m_oRepos = oRepos;
        }

        public void DisplayChangesets(Stream oStream)
        {
            Console.WriteLine("Changesets for : " + oStream.Name);

            Changesets oChangesets = oStream.SubmittedChangesets;

            foreach (Changeset oChangeset in oChangesets)
            {
                Console.Write("Changeset '" + oChangeset.IdString + "'\n" +
                              "Description: " + oChangeset.Description + "\n" +
                              "Client: " + oChangeset.ClientName + " " + "Date: " + oChangeset.Timestamp + "\n" +
                              "Files:\n");
                DisplayChangesetItems(oChangeset);
                Console.Write("\n\n");
            }
        }

        void DisplayChangesetItems(Changeset oChangeset)
        {
            ChangeItems oItems = oChangeset.Items;
            
            foreach( ChangeItem oItem in oItems )
            {
                Console.Write("   ");

                switch(oItem.Type)
                {
                    case SDK.TPCMChangeItemType.pcmAdd:
                        Console.Write("a  ");
                        break;
                    case SDK.TPCMChangeItemType.pcmEdit:
                        Console.Write("e  ");
                        break;
                    case SDK.TPCMChangeItemType.pcmAddFolder:
                        Console.Write("af ");
                        break;
                    case SDK.TPCMChangeItemType.pcmDelete:
                        Console.Write("d  ");
                        break;
                    case SDK.TPCMChangeItemType.pcmDeleteFolder:
                        Console.Write("df ");
                        break;
                    default:
                        Console.Write("?? ");
                        break;
                }

                Console.Write(oItem.Path);
                Console.Write(" ");
                Console.Write(oItem.FileType.Type);
                Console.Write("\n");
            }
        }

        Connection m_oConnection;
        Repository m_oRepos;
    }
}
