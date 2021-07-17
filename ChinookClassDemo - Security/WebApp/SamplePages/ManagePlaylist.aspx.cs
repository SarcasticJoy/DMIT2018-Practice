﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using System.Configuration;
using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;

            //using form security
            //check to see if the user is logged in
            if (Request.IsAuthenticated) // Remember request from Razor?
            {
                //check to see if the logged user is in the allowable rolee(s)
                // to have access to this page

                //if (User.IsInRole(ConfigurationManager.AppSettings["customerRole"])
                //    || User.IsInRole("Administrator"))


                if (User.IsInRole(ConfigurationManager.AppSettings["customerRole"]))
                {
                    //LoggedUser.Text = User.Identity.Name;

                    //obtain the customer ID from the ApplicationUser record
                    SecurityController secmgr = new SecurityController();
                    int customerid = secmgr.GetCurrentUserCustomerId(User.Identity.Name)
                                        ?? 0; //swaps the null with 0
                    LoggedUser.Text = customerid.ToString();

                    //to get the Customer personal data using the pkey value 
                    // obtained from the ApplicationUser record, simply
                    // make a query to your database with the pkey value.
                }
                else
                {
                    Response.Redirect("~/SamplePages/AccessDenied");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
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

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Artist";
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("You did not supply an artist name");
                //the value parametre field is a HiddenField
                //access to a HiddenField is by .Value NOT .Text
                SearchArg.Value = "garbagevalue"; //this is simply a junk value
            }
            else
            {
                SearchArg.Value = ArtistName.Text;
            }
            // to force the re-execution of an ODS attached to a display control
            // rebind the display control
            TracksSelectionList.DataBind();
          }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";
            // In this example there is NO prompt line od the ddl.
            // This means that no validation for selection needs to be done. 
            SearchArg.Value = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Album";
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("You did not supply an album title");
                //the value parametre field is a HiddenField
                //access to a HiddenField is by .Value NOT .Text
                SearchArg.Value = "garbagevalue"; //this is simply a junk value
            }
            else
            {
                SearchArg.Value = AlbumTitle.Text;
            }
            // to force the re-execution of an ODS attached to a display control
            // rebind the display control
            TracksSelectionList.DataBind();
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            //username is coming from the system via security
            //since security has yet to be installed, a default will be 
            // setup for the username value
            //string username = "HansenB"; //default

            //Security has now been installed for the ssystem
            string username = User.Identity.Name;

            if (string.IsNullOrWhiteSpace(PlaylistName.Text.Trim()))
            {
                MessageUserControl.ShowInfo("Playlist Search", "No playlist name was supplied");
            }
            else
            {
                //validate data present
                //do a standard lookup
                //assign results to a gridView
                //use some user friendly error handling
                // the way we are doing the error handling is using 
                //MessageUserControl instead of try\catch
                // the control logic
                // Within the control there exists a method called
                // .TryRun()
                // syntax:
                // MessageUserControl.TryRun( () => {

                //       your coding logic
                //  }[,"Message title", "success message"]
                // );
                //
                MessageUserControl.TryRun(() =>
                {
                    //your code
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //the attachment of the playlist to the web control
                    //  will be used throughout the web page; as such
                    //  the code for refreshing is in its own method
                    RefreshPlaylist(sysmgr, username);

                }, "Playlist Search", "View the requested playlist below.");
            }

        }

        protected void RefreshPlaylist(PlaylistTracksController sysmgr, string username)
        {
            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
            PlayList.DataSource = info;
            PlayList.DataBind();
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            // validate data present: playlistname
            // validate rows present: is there something to move
            // validate single row: only one row allowed to move
            // validate can it move: is not the last row
            // if ok Move_Track
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name and Fetch playlist.");

            }
            else
            {
                if (PlayList.Rows.Count == 0) //somtimes Count is a property, and sometimes  a method (A property here)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist visible to use a track for movement. Search for your playlist or add a tracks to your selected playlist.");
                }
                else
                {
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //Traverse the gridview control Playlist
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackID = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            //the above only points to the label, rather than getting its contents
                            moveTrack.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        
                        }
                    }

                    //singly row?
                    switch(rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select one song to move.");
                                break;
                            }
                        case 1:
                            {
                                //can it move down?
                                if (moveTrack.TrackNumber == PlayList.Rows.Count)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "The song selected is already the last song. It cannot be moved down.");
                                }
                                else
                                {
                                    moveTrack.Direction = "down";
                                    MoveTrack(moveTrack);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select only one song to move.");
                                break;
                            }
                    }
                }
            }
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            // validate data present: playlistname
            // validate rows present: is there something to move
            // validate single row: only one row allowed to move
            // validate can it move: is not the first row
            // if ok Move_Track
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name and Fetch playlist.");

            }
            else
            {
                if (PlayList.Rows.Count == 0) //somtimes Count is a property, and sometimes  a method (A property here)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist visible to use a track for movement. Search for your playlist or add a tracks to your selected playlist.");
                }
                else
                {
                    MoveTrackItem moveTrack = new MoveTrackItem();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //Traverse the gridview control Playlist
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            moveTrack.TrackID = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            //the above only points to the label, rather than getting its contents
                            moveTrack.TrackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);

                        }
                    }

                    //singly row?
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select one song to move.");
                                break;
                            }
                        case 1:
                            {
                                //can it move down?
                                if (moveTrack.TrackNumber == 1)
                                {
                                    MessageUserControl.ShowInfo("Track Movement", "The song selected is already the first song. It cannot be moved up.");
                                }
                                else
                                {
                                    moveTrack.Direction = "up";
                                    MoveTrack(moveTrack);
                                }
                                break;
                            }
                        default:
                            {
                                MessageUserControl.ShowInfo("Track Movement", "You must select only one song to move.");
                                break;
                            }
                    }
                }
            }

        }

        protected void MoveTrack(MoveTrackItem moveTrack)
        {
            //call BLL to move track
            string username = "HansenB"; //until security is implemented
            moveTrack.UserName = username;
            moveTrack.PlaylistName = PlaylistName.Text;

            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(moveTrack);
                RefreshPlaylist(sysmgr, username);
            }, "Track Movement", "Track has nbeen moved.");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name and Fetch playlist.");

            }
            else
            {
                if (PlayList.Rows.Count == 0) //somtimes Count is a property, and sometimes  a method (A property here)
                {
                    MessageUserControl.ShowInfo("Track Removal", "You must have a playlist visible to use a track for movement. Search for your playlist or add a tracks to your selected playlist.");
                }
                else
                {
                    List<int> removeTracks = new List<int>();
                    int rowsSelected = 0;
                    CheckBox trackSelection = null;
                    //Traverse the gridview control Playlist
                    //you could do this same code using a foreach()
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        //point to the checkbox control on the gridview row
                        trackSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        //test the setting of the checkbox
                        if (trackSelection.Checked)
                        {
                            rowsSelected++;
                            removeTracks.Add(int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text));
                        }
                    }

                    //singly row?
                    switch (rowsSelected)
                    {
                        case 0:
                            {
                                MessageUserControl.ShowInfo("Track Removal", "You must select at least one song to remove.");
                                break;
                            }
                        default:
                            {
                                string username = "HansenB"; //until security is implemented
                                MessageUserControl.TryRun(() =>
                                {
                                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                                    sysmgr.DeleteTracks(username, PlaylistName.Text, removeTracks);
                                    RefreshPlaylist(sysmgr, username);
                                }, "Track Movement", "Track has been moved.");
                                break;
                            }
                    }
                }
            }
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            string username = "HansenB"; // until security is implemented
            
            //form event validation: Presence
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name.");
            }
            else
            {
                //access the contents of a control on the selected listview row
                //new stuff, eh
                string song = (e.Item.FindControl("Namelabel") as Label).Text; //Item is the listview row
                int trackid = int.Parse(e.CommandArgument.ToString());

                MessageUserControl.TryRun(() => {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist
                        (PlaylistName.Text, username, trackid, song);
                    RefreshPlaylist(sysmgr, username);
                }, "Add Track to Playlist", "Track has been added to playlist"); // The () indicates there is a method to follow
            }
        }

    }
}