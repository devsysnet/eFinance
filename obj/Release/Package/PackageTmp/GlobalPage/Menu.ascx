<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="eFinance.GlobalPage.Menu" %>
<style type="text/css">
    body {
        font-family: Arial;
        font-size: 10pt;
    }

    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }

    .modalPopup {
        background-color: #FFFFFF;
        width: 300px;
        border: 3px solid #0DA9D0;
        padding: 0;
    }

        .modalPopup .header {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .body {
            padding: 10px;
            min-height: 50px;
            text-align: center;
            font-weight: bold;
        }

        .modalPopup .footer {
            padding: 6px;
        }
</style>
<script type="text/javascript">
    init.push(function () {
        $('#confirm-logout').on('click', function () {
            bootbox.confirm({
                message: "Are you sure to quit?",
                callback: function (result) {
                    if (result == true) {
                        window.location.href = '<%=Func.BaseUrl%>Logout.aspx?confirm=ok';
                    }
                },
                className: "bootbox-sm"
            });
        });
    });
</script>
<div id="main-menu" role="navigation">
    <div id="main-menu-inner">
        <div class="menu-content top" id="menu-content-demo">
            <div class="row">
                <div class="text-bg">
                    <span class="text-slim">Welcome ,</span>
                    <br />
                    <span class="text-semibold">
                        <asp:Label ID="lblNameUser" runat="server"></asp:Label></span>
                </div>

                <asp:Image ID="imgUser" runat="server" />
                <%--<div class="btn-group">
                    <a href="javascript:void()" class="btn btn-xs btn-primary btn-outline dark"><i class="fa fa-user"></i></a>
                    <a href="<%=Func.BaseUrl%>Setting.aspx" class="btn btn-xs btn-primary btn-outline dark"><i class="fa fa-cog"></i></a>
                    <a href="javascript:void()" id="confirm-logout" class="btn btn-xs btn-danger btn-outline dark"><i class="fa fa-power-off"></i></a>
                </div>--%>
                <%--<a href="javascript:void()" class="close">&times;</a>--%>
            </div>
            <%--<div class="clearfix"></div>
            
            <div class="row">
                <br />
                <span class="text-semibold" style="color: #fff;">
                    <asp:Label ID="lblCabang" runat="server" Text="Jakarta"></asp:Label>
                </span>
                <br />
                <span class="text-semibold" style="color: #fff;">
                    <asp:Label ID="lblIp" runat="server" Text="Your IP : 192.18.2.1"></asp:Label></span>
                <br />
                <span class="text-semibold" style="color: #fff; font-size: 10px;">Session time left:&nbsp;<span id="secondsIdle"></span>&nbsp;Second.
                </span>
            </div>--%>
        </div>
        <script type="text/javascript">
            <%--function SessionExpireAlert(timeout) {
                var seconds = timeout / 1000;
                document.getElementsByName("secondsIdle").innerHTML = seconds;
                document.getElementsByName("seconds").innerHTML = seconds;
                setInterval(function () {
                    seconds--;
                    document.getElementById("seconds").innerHTML = seconds;
                    document.getElementById("secondsIdle").innerHTML = seconds;
                }, 1000);
                setTimeout(function () {
                    //Show Popup before 20 seconds of timeout.
                    $find("mpeTimeout").show();
                }, timeout - 20 * 1000);
                setTimeout(function () {
                    window.location = "<%=Func.BaseUrl%>Expired.html";
                }, timeout);
            };
            function ResetSession() {
                //Redirect to refresh Session.
                window.location = window.location.href;
            }--%>
        </script>
        <div class="menu-content top">
            <span style="color: #fff;">
                <asp:Label ID="lblCabang" runat="server" Text="-"></asp:Label>
            </span>
        </div>
        
        
        <asp:Label ID="lblMenu" runat="server"></asp:Label>

    </div>
</div>
<asp:LinkButton ID="lnkFake" runat="server" />
<asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup"
    TargetControlID="lnkFake" OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modalBackground"
    OnOkScript="ResetSession()">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    <div class="header">
        Session Expiring!
    </div>
    <div class="body">
        Your Session will expire in&nbsp;<span id="seconds"></span>&nbsp;seconds.<br />
        Do you want to reset?
    </div>
    <div class="footer" align="right">
        <asp:Button ID="btnYes" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Yes" CssClass="yes" />
        <asp:Button ID="btnNo" runat="server" class="btn btn-xs btn-labeled btn-warning" Text="No" CssClass="no" />
    </div>
</asp:Panel>


