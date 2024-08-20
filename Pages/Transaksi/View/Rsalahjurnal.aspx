<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rsalahjurnal.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rsalahjurnal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }

    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>
                        <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cari : </label>
                        <%--<asp:DropDownList runat="server" Width="180" CssClass="form-control" AutoPostBack="true" ID="cboBrand"></asp:DropDownList>--%>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnPosting" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdharianGL" DataKeyNames="kdtran" SkinID="GridView" runat="server" AllowPaging="false" PageSize="100" OnPageIndexChanging="grdharianGL_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="kdtran" SortExpression="kdtran" HeaderText="Kode Transaksi" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="tgltransaksi" SortExpression="tgltransaksi" HeaderText="Tanggal Transaksi" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="jenistran" SortExpression="jenistran" HeaderText="Jenis" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="kdrek" SortExpression="kdrek" HeaderText="Kode AKUN" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="ket" SortExpression="ket" HeaderText="AKUN" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="uraian" SortExpression="uraian" HeaderText="Uraian" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="debet" SortExpression="debet" HeaderText="Debet" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" HeaderStyle-CssClass="text-center" />
                        <asp:BoundField DataField="Kredit" SortExpression="Kredit" HeaderText="Kredit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" HeaderStyle-CssClass="text-center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
