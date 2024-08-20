<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RRejectBudget.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RRejectBudget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <script type="text/javascript">
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdHarianGL.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[5].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
        <asp:HiddenField ID="hdnnoYysn" runat="server" />

    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                             <div class="form-group">
                                <div class="form-inline">
                                        <div class="col-sm-10">
                        <label>Perwakilan : </label>

                                            <asp:DropDownList ID="cboPerwakilan" runat="server" Width="250" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>

                        <label>Unit : </label>
                        <asp:DropDownList ID="cboCabang" Width="200" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdHarianGL" DataKeyNames="thn" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10" OnPageIndexChanging="grdHarianGL_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="thn" SortExpression="thn" HeaderText="Tahun Budget" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="namauser" SortExpression="namauser" HeaderText="User_Reject" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="keteranganreject" SortExpression="keteranganreject" HeaderText="Keterangan reject" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" />
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <%--<asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Posting" OnClick="btnPosting_Click"></asp:Button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
