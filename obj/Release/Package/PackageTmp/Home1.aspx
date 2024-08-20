<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Home1.aspx.cs" Inherits="eFinance.Home1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="stat-panel">
                <div class="stat-row">
                    <div class="stat-cell col-sm-4 padding-sm-hr bordered no-border-r valign-top">
                        <h4 class="padding-sm no-padding-t padding-xs-hr"><i class="fa fa-star text-primary"></i>&nbsp;&nbsp;Task List Alert</h4>
                        <ul class="list-group no-margin">
                            <asp:Label ID="lblAlertList" runat="server"></asp:Label>
                        </ul>
                    </div>
                    <div class="stat-cell col-sm-8 bg-primary" style="padding:15px; background:#fff;">
                        <asp:Image ID="imgHome" runat="server" Width="100%"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
