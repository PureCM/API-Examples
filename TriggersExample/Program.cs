using System;
using System.Collections.Generic;
using System.Text;
using PureCM.Client;

namespace PluginTest
{
    class Program
    {
        static void OnUserConnected(UserConnectedEvent oEvt)
        {
            System.Console.WriteLine("OnUserConnected()");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tClient='" + oEvt.ClientName + "'");
            System.Console.WriteLine("\tAddress='" + oEvt.Address + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnUserDisconnected(UserDisconnectedEvent oEvt)
        {
            System.Console.WriteLine("OnUserDisconnected()");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tClient='" + oEvt.ClientName + "'");
            System.Console.WriteLine("\tAddress='" + oEvt.Address + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReposCreated(ReposCreatedEvent oEvt)
        {
            System.Console.WriteLine("OnReposCreated()");
            System.Console.WriteLine("\tRepos='" + oEvt.RepositoryName + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReposDeleted(ReposDeletedEvent oEvt)
        {
            System.Console.WriteLine("OnReposDeleted()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repos + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnStreamCreated(StreamCreatedEvent oEvt)
        {
            System.Console.WriteLine("OnStreamCreated()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tStream='" + oEvt.Stream.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnStreamDeleted(StreamDeletedEvent oEvt)
        {
            System.Console.WriteLine("OnStreamDeleted()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tStream='" + oEvt.StreamName + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnChangeSubmitted(ChangeSubmittedEvent oEvt)
        {
            System.Console.WriteLine("OnChangeSubmitted()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tStream='" + oEvt.Stream.Name + "'");
            System.Console.WriteLine("\tChangeID='" + oEvt.Changeset.IdString + "'");
            System.Console.WriteLine("\tChange Description='" + oEvt.Changeset.BriefDescription + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReviewAssigned(ReviewAssignedEvent oEvt)
        {
            System.Console.WriteLine("OnReviewAssigned()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tPreChangeID='" + oEvt.PreChangeID + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReviewAccepted(ReviewAcceptedEvent oEvt)
        {
            System.Console.WriteLine("OnReviewAccepted()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tPreChangeID='" + oEvt.PreChangeID + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReviewRejected(ReviewRejectedEvent oEvt)
        {
            System.Console.WriteLine("OnReviewRejected()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tPreChangeID='" + oEvt.PreChangeID + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnReviewFailed(ReviewFailedEvent oEvt)
        {
            System.Console.WriteLine("OnReviewFailed()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tPreChangeID='" + oEvt.PreChangeID + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnIssueAction(IssueActionEvent oEvt)
        {
            System.Console.WriteLine("OnIssueAction()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tIssueRef='" + oEvt.Issue.Ref + "'");
            System.Console.WriteLine("\tIssueType='" + oEvt.Issue.Type.Name + "'");
            System.Console.WriteLine("\tIssueAction='" + oEvt.IssueAction.Name + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void OnEventCreated(EventCreatedEvent oEvt)
        {
            System.Console.WriteLine("OnEventCreated()");
            System.Console.WriteLine("\tEventID='" + oEvt.EventID + "'");
        }

        static void OnCustomAction(ref CustomActionEvent oEvt)
        {
            oEvt.Handled = true;
            oEvt.Success = false;

            System.Console.WriteLine("OnCustomAction()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tStream='" + oEvt.Stream.Name + "'");
            System.Console.WriteLine("\tAction='" + oEvt.Action + "'");
            System.Console.WriteLine("\tUser='" + oEvt.User.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");

            Connection oConn = oEvt.Connection;

            if (oConn.IsValid())
            {
                if (oEvt.Repository.Name.Length == 0)
                {
                    System.Console.WriteLine("\tGlobal Custom Properties");

                    PureCM.Client.CustomProperties oProperties = oConn.CustomProperties;

                    foreach (CustomProperty oProp in oProperties)
                    {
                        System.Console.WriteLine("\t\t" + oProp.Name + " - " + oProp.Value);
                    }
                }
                else
                {
                    PureCM.Client.Repository oRepos = oEvt.Repository;

                    if (oRepos != null)
                    {
                        if (oEvt.Stream.Name.Length == 0)
                        {
                            System.Console.WriteLine("\t" + oRepos.Name + " Repository Properties");

                            PureCM.Client.CustomProperties oProperties = oRepos.CustomProperties;

                            foreach (CustomProperty oProp in oProperties)
                            {
                                System.Console.WriteLine("\t\t" + oProp.Name + " - " + oProp.Value);
                            }
                        }
                        else
                        {
                            PureCM.Client.Stream oStream = oRepos.Streams.ByPath(oEvt.Stream.Name);

                            if (oStream != null)
                            {
                                System.Console.WriteLine("\t" + oStream.Name + " Stream Properties");

                                PureCM.Client.CustomProperties oProperties = oStream.CustomProperties;

                                foreach (CustomProperty oProp in oProperties)
                                {
                                    System.Console.WriteLine("\t\t" + oProp.Name + " - " + oProp.Value);
                                }
                            }
                            else
                            {
                                oEvt.Success = false;
                                oEvt.Message = "Failed to find repository";
                            }
                        }
                    }
                    else
                    {
                        oEvt.Success = false;
                        oEvt.Message = "Failed to find repository";
                    }
                }
            }
            else
            {
                oEvt.Success = false;
                oEvt.Message = "Failed to get a valid connection";
            }
        }

        static void OnAutoMergeFailed(MergeEvent oEvt)
        {
            System.Console.WriteLine("OnAutoMergeFailed()");
            System.Console.WriteLine("\tRepos='" + oEvt.Repository.Name + "'");
            System.Console.WriteLine("\tStream='" + oEvt.Stream.Name + "'");
            System.Console.WriteLine("\tChangeID='" + oEvt.Changeset.IdString + "'");
            System.Console.WriteLine("\tChange Description='" + oEvt.Changeset.Description + "'");
            System.Console.WriteLine("\tMerge Stream='" + oEvt.MergeStream.Name + "'");
            System.Console.WriteLine("\tTimestamp='" + oEvt.DateAndTime.ToString() + "'");
        }

        static void Main(string[] args)
        {
            Connection oConn = new Connection("localhost", 2010, SDK.TPCMAuthType.pcmauthPassword, "LeadDeveloper1", "secret");

            oConn.OnUserConnected = OnUserConnected;
            oConn.OnUserDisconnected = OnUserDisconnected;
            oConn.OnReposCreated = OnReposCreated;
            oConn.OnReposDeleted = OnReposDeleted;
            oConn.OnStreamCreated = OnStreamCreated;
            oConn.OnStreamDeleted = OnStreamDeleted;
            oConn.OnChangeSubmitted = OnChangeSubmitted;
            oConn.OnReviewAssigned = OnReviewAssigned;
            oConn.OnReviewAccepted = OnReviewAccepted;
            oConn.OnReviewRejected = OnReviewRejected;
            oConn.OnReviewFailed = OnReviewFailed;
            oConn.OnIssueAction = OnIssueAction;
            oConn.OnEventCreated = OnEventCreated;
            oConn.OnCustomAction = OnCustomAction;
            oConn.OnAutoMergeFailed = OnAutoMergeFailed;

            oConn.WaitForSysEvents();

            oConn.WakeUpSysEvents();
        }
    }
}
