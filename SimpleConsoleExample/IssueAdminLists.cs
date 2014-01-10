using System;
using System.Collections.Generic;
using System.Text;

using PureCM.Client;

namespace SimpleConsoleExample
{
    public class IssueAdminLists
    {
        public IssueAdminLists(Connection oConnection)
        {
            m_oConnection = oConnection;
        }

        public void DisplayIssueTypes()
        {
            Console.WriteLine("\nIssue Types");
            Console.WriteLine("=============\n");

            IssueTypes oTypes = m_oConnection.IssueTypes;

            foreach (IssueType oType in oTypes)
            {
                Console.WriteLine("Name: " + oType.Name);
                Console.WriteLine("IssueRef: " + oType.IssueRefFormat);

                IssueAction oCreateAction = oType.CreationAction;

                if (oCreateAction != null )
                {
                    Console.WriteLine("Creation Action: " + oCreateAction.Name);
                }
                else
                {
                    Console.WriteLine("Creation Action: [UNKNOWN]");
                }

                IssueActions oActions = oType.Actions;
                IssueAction oSubmitAction = oActions.ById(oType.DefaultSubmissionAction);

                if (oSubmitAction != null)
                {
                    Console.WriteLine("Default Submit Action: " + oSubmitAction.Name);
                }
                else
                {
                    Console.WriteLine("Default Submit Action: [UNKNOWN]");
                }

                IssueField oDisplayField = oType.DisplayField;

                if (oDisplayField != null)
                {
                    Console.WriteLine("Display Field: " + oDisplayField.Name);
                }
                else
                {
                    Console.WriteLine("Display Field: [UNKNOWN]");
                }

                Console.WriteLine("XRC: " + oType.XrcString);
                Console.WriteLine("------------------------------");
            }
        }

        public void DisplayIssueActions(IssueType oType)
        {
            Console.WriteLine("\nIssue Actions for " + oType.Name);
            Console.WriteLine("=============\n");

            IssueActions oActions = oType.Actions;
            
            foreach (IssueAction oAction in oActions)
            {
                Console.WriteLine("Name: " + oAction.Name);
                Console.WriteLine("Label: " + oAction.Label);
                Console.WriteLine("End State: " + oAction.EndState.Name);
                Console.WriteLine("Needs Description: " + oAction.NeedsDescription);
                Console.WriteLine("Reject Changes: " + oAction.RejectChanges);
                Console.WriteLine("Requires Changes: " + oAction.RequiresChanges);
                Console.WriteLine("Requires Time: " + oAction.RequiresTime);
                Console.WriteLine("Mandatory Fields:");

                IssueFields oFields = oType.Fields;

                foreach (IssueField oField in oAction.MandatoryFields)
                {
                    if (oField != null)
                    {
                        Console.WriteLine("    " + oField.Name);
                    }
                    else
                    {
                        Console.WriteLine("    [UNKNOWN]");
                    }
                }

                switch (oAction.EndUserType)
                {
                    case SDK.TPCMIssueActionEndUserType.pcmEnduserAll:
                        Console.WriteLine("End User Type: All");
                        break;
                    case SDK.TPCMIssueActionEndUserType.pcmEnduserUser:
                        Console.WriteLine("End User Type: User");
                        break;
                    case SDK.TPCMIssueActionEndUserType.pcmEnduserGroup:
                        Console.WriteLine("End User Type: Group");
                        break;
                }

                Users oUsers = m_oConnection.Users;
                Groups oGroups = m_oConnection.Groups;

                Console.WriteLine("Valid End Users:");

                foreach (UserOrGroup oUser in oAction.ValidEndUsers)
                {
                    if (oUser != null)
                    {
                        Console.WriteLine("    " + oUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("    [UNKNOWN]");
                    }
                }

                Console.WriteLine("Valid Operators:");

                foreach (UserOrGroup oUser in oAction.ValidOperators)
                {
                    if (oUser != null)
                    {
                        Console.WriteLine("    " + oUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("    [UNKNOWN]");
                    }
                }

                Console.WriteLine("Notified Users:");

                foreach (UserOrGroup oUser in oAction.NotifiedUsers)
                {
                    if (oUser != null)
                    {
                        Console.WriteLine("    " + oUser.Name);
                    }
                    else
                    {
                        Console.WriteLine("    [UNKNOWN]");
                    }
                }

                Console.WriteLine("------------------------------");
            }


        }

        public void DisplayIssueStates(IssueType oType)
        {
            Console.WriteLine("\nIssue States for " + oType.Name);
            Console.WriteLine("=============\n");

            IssueStates oStates = oType.States;

            foreach (IssueState oState in oStates)
            {
                Console.WriteLine("Name: " + oState.Name);
                Console.WriteLine("Label: " + oState.Label);
                Console.WriteLine("Can Be Active: " + oState.Active);
                Console.WriteLine("Accept Reviews: " + oState.ChangeAccept);

                String strActivationAction = "[None]";
                IssueActions oActions = oType.Actions;

                IssueAction oActivationAction = oState.ActivationAction;

                if (oActivationAction != null)
                {
                    strActivationAction = oActivationAction.Name;
                }

                Console.WriteLine("Activation Action: " + strActivationAction);
                Console.WriteLine("Modifiable Fields:");

                foreach(IssueField oField in oState.ModifiableFields)
                {
                    if (oField == null)
                        break;

                    Console.WriteLine("    " + oField.Name);
                }

                Console.WriteLine("Valid Actions:");

                foreach (IssueAction oAction in oState.ValidActions)
                {
                    if (oAction != null)
                    {
                        Console.WriteLine("    " + oAction.Name);
                    }
                    else
                    {
                        Console.WriteLine("    [UNKNOWN]");
                    }
                }

                Console.WriteLine("------------------------------");
            }
        }

        public void DisplayIssueFields(IssueType oType)
        {
            Console.WriteLine("\nIssue Fields for " + oType.Name);
            Console.WriteLine("=============\n");

            IssueFields oFields = oType.Fields;

            foreach (IssueField oField in oFields)
            {
                Console.WriteLine("Name: " + oField.Name);
                Console.WriteLine("Label: " + oField.Label);

                switch (oField.ValueType)
                {
                    case SDK.TPCMIssueValueType.pcmIssuevalueString:
                        {
                            Console.WriteLine("Type: String");
                            Console.WriteLine("Default: " + oField.Default);
                            Console.WriteLine("Max Chars: " + oField.MaxChars);
                            Console.WriteLine("Multi-line: " + oField.MultiLine);
                        }
                        break;
                    case SDK.TPCMIssueValueType.pcmIssuevalueInteger:
                        {
                            Console.WriteLine("Type: Integer");
                            Console.WriteLine("Default: " + oField.Default);
                            Console.WriteLine("Min Value: " + oField.MinValue);
                            Console.WriteLine("Max Value: " + oField.MaxValue);
                        }
                        break;
                    case SDK.TPCMIssueValueType.pcmIssuevalueBool:
                        {
                            Console.WriteLine("Type: Bool");
                            Console.WriteLine("Default: " + oField.Default);
                        }
                        break;
                    case SDK.TPCMIssueValueType.pcmIssuevalueEnum:
                        {
                            Console.WriteLine("Type: Custom");
                            Console.WriteLine("Default: " + oField.Default);
                            Console.WriteLine("Custom Values: ");

                            foreach (String strValue in oField.Values)
                            {
                                Console.WriteLine("    " + strValue);
                            }
                        }
                        break;
                    case SDK.TPCMIssueValueType.pcmIssuevalueDate:
                        {
                            Console.WriteLine("Type: Date");
                            Console.WriteLine("Default: " + oField.Default);
                        }
                        break;
                    case SDK.TPCMIssueValueType.pcmIssuevalueNone:
                        {
                            Console.WriteLine("Type: None");
                        }
                        break;
                }
                Console.WriteLine("------------------------------");
            }
        }        

        private Connection m_oConnection;
    }
}
