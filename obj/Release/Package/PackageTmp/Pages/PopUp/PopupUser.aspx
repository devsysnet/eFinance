<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/WebMaster.Master" CodeBehind="PopupUser.aspx.cs" Inherits="eFinance.Pages.PopUp.PopupUser" %>

<link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/bootstrap.css">
<link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/site.css">
<link href="<%=Func.BaseUrl%>Assets/stylesheets/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="<%=Func.BaseUrl%>Assets/stylesheets/Custom.css">
<body style="padding-top: 1px;">
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdnTipe" />
        <div class="row">
            <div class="col-sm-8">
            </div>
            <div class="col-sm-4">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" placeholder="Search..."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <asp:GridView ID="grdSup" DataKeyNames="noUser" ShowFooter="true" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" Font-Size="9" OnPageIndexChanging="grdSup_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                        <ItemTemplate>
                            <div class="text-center">
                                <%# Container.DataItemIndex + 1 %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="namauser" SortExpression="kdrek" HeaderText="Nama" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="emailuser" SortExpression="namaSup" HeaderText="Email" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="">
                        <ItemTemplate>
                            <div class="text-center">
                                <a href="javascript:void()" class="btn btn-primary btn-sm" onclick="return SetID('<%#Eval("namauser") %>')">Add</a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">

        function SetID(namauser) {
            var tipe = document.getElementById("<%=hdnTipe.ClientID%>").value;
            if (window.opener != null && !window.opener.closed) {
                //// PO Inden
                if (tipe == 1) {
                    window.opener.document.getElementById("BodyContent_txtSuratJalan").value = namauser;
                }
                if (tipe == 2) {
                    window.opener.document.getElementById("BodyContent_txtDO").value = namauser;
                }
                if (tipe == 3) {
                    window.opener.document.getElementById("BodyContent_txtInvoicee").value = namauser;
                }
                if (tipe == 4) {
                    window.opener.document.getElementById("BodyContent_txtPOLocalSignature").value = namauser;
                }
            }
            window.close();
        }
    </script>
</body>
