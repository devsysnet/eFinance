<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadMenu.ascx.cs" Inherits="eFinance.GlobalPage.HeadMenu" %>
<div class="site-user">
    <div class="media align-items-center">
        <a href="javascript:void(0)" style="margin-right:5px;">
            <asp:Image ID="imgUser" runat="server" Width="50" Height="50" />
        </a>
        <div class="media-body hidden-fold">
            <h6 class="mborder-a-0" style="color:#fff;">
                <asp:Label runat="server" ID="lblNama"></asp:Label>
            </h6>
            <div class="dropdown">
                <a href="javascript:void(0)" class="dropdown-toggle usertitle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    <small style="color:#fff;">
                        <asp:Label runat="server" ID="lblJabatan"></asp:Label></small>
                    <span class="caret"></span>
                </a>
                <div class="dropdown-menu animated scaleInDownRight">

                    <div role="separator" class="divider">
                    </div>
                    <a class="dropdown-item" href="<%=Func.BaseUrl%>Logout.aspx?confirm=ok">
                        <span class="mr-1">
                            <i class="fa fa-power-off"></i>
                        </span>
                        <span>Sign Outv</span>
                    </a>
                </div>
            </div>
        </div>
        <!-- /.media-body -->
    </div>
    <!-- ./media -->
</div>
