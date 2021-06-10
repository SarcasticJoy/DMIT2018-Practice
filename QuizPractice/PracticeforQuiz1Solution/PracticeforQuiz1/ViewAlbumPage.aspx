<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAlbumPage.aspx.cs" Inherits="PracticeforQuiz1.ViewAlbumPage" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Title Albums info only bwecuas eno Artist COntroller</h1>

    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    <br />

    <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="Title" DataValueField="AlbumId" AppendDataBoundItems="true">
        <asp:ListItem Value="0">select an artist...</asp:ListItem>
    </asp:DropDownList>
    <asp:LinkButton ID="FetchAlbums" runat="server" OnClick="FetchAlbums_Click">Fetch Albums</asp:LinkButton>

    <asp:GridView ID="AlbumsofArtistList" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="AlbumsofArtistList_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label">
                <ItemTemplate>
                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            Artist has no Albums on file.
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Albums_List" TypeName="Quiz1System.BLL.AlbumController"></asp:ObjectDataSource>
</asp:Content>
