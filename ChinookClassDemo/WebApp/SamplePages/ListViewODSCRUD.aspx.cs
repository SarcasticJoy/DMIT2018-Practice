using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class ListViewODSCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Error Handling

        protected void SelectCheckForException(object sender,
                                        ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e); // Implementing Dans Nuget.  Dont forget to tell the asp webpage side to use this method
                                                            // I mean this: <asp:ObjectDataSource ID="ArtistListODS" OnSelected="SelectCheckForException"
        }

        protected void InsertCheckForException(object sender,
                                        ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been added");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void DeleteCheckForException(object sender,
                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been removed");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void UpdateCheckForException(object sender,
                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Process Success", "Album has been updated");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        #endregion

    }
}