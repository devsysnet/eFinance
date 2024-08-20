<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="eFinance.GlobalPage.Header" %>
<script type="text/javascript">
    init.push(function () {
        $('#ui-bootbox-confirm').on('click', function () {
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
<div id="main-navbar" class="navbar navbar-inverse" role="navigation" >
    <button type="button" id="main-menu-toggle"><i class="navbar-icon fa fa-bars icon"></i><span class="hide-menu-text">HIDE MENU</span></button>

    <div class="navbar-inner" style="background:#0288D1 !important;">
        <div class="navbar-header" style="background:#0288D1 !important;">
            <a href="Home.aspx" class="navbar-brand">
                <div>
                    <img alt="Pixel Admin" src="<%=Func.BaseUrl%>assets/images/logo/logo-4.png" width="18" />
                </div>
                SAK 4.0
            </a>
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#main-navbar-collapse"><i class="navbar-icon fa fa-bars"></i></button>
        </div>
        <div id="main-navbar-collapse" class="collapse navbar-collapse main-navbar-collapse">
            <div>
                <%--<ul class="nav navbar-nav">
                    <li>
                        <a href="javascript:void()">Beranda</a>
                    </li>
                </ul>--%>
                <div class="right clearfix">
                    <ul class="nav navbar-nav pull-right right-navbar-nav">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle user-menu" data-toggle="dropdown">
                                 <svg style="margin-top:9px" xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-info-circle" viewBox="0 0 16 16">
  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
  <path d="m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533L8.93 6.588zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"/>
</svg>
                            </a>
                            <ul class="dropdown-menu" style="width:300px">
                                <li>
                                    		<a href="<%=Func.BaseUrl%>assets/javascripts/pdfjs/web/viewer.html?file=<%=Func.BaseUrl%>assets/document/manual_book/MANUAL BOOK SAK_PENGADAAN.pdf"><span style="margin-left:10px;font-size:12pt;color:black">Manual Book SAK Pengadaan</span></a>
                                    		<a href="<%=Func.BaseUrl%>assets/javascripts/pdfjs/web/viewer.html?file=<%=Func.BaseUrl%>assets/document/manual_book/MANUAL_BOOK_PENERIMAAN_DAN_BAYAR.pdf"><span style="margin-left:10px;font-size:12pt;color:black">Manual Book Penerimaan Dan Bayar</span></a>
                                    		<a href="<%=Func.BaseUrl%>assets/javascripts/pdfjs/web/viewer.html?file=<%=Func.BaseUrl%>assets/document/manual_book/MANUAL_BOOK_APLIKASI_HRD.pdf"><span style="margin-left:10px;font-size:12pt;color:black">Manual Book HRD</span></a>
                                    

                                </li>
                               
                            </ul>
                        </li>
                      
                        <li>
                            <div style="width:30px">&nbsp;</div>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle user-menu" data-toggle="dropdown">
                                <asp:Image ID="imgUser" runat="server" />
                                <span><asp:Label ID="lblNameUser" runat="server"></asp:Label></span>
                            </a>
                            <ul class="dropdown-menu">
                                <%--<li><a href="javascript:void()">Profile</a></li>--%>
                                <li><a href="<%=Func.BaseUrl%>Setting.aspx"><i class="dropdown-icon fa fa-cog"></i>&nbsp;&nbsp;Setting</a></li>
                                <li class="divider"></li>
                                <li><a href="javascript:void()" id="ui-bootbox-confirm"><i class="dropdown-icon fa fa-power-off"></i>&nbsp;&nbsp;Log Out</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
