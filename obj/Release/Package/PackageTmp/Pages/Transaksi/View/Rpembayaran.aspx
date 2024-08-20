<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Rpembayaran.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.Rpembayaran" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        .table {
            margin-bottom: 0;
        }
        #BodyContent_grdAccountPanelHeaderContentFreeze {
            height: auto !important;
        }
    </style>
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }

    </script>
    <asp:HiddenField ID="hdnYayasan" runat="server" Value="0" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Filter :   </label>
                         <asp:DropDownList ID="cboPerwakilan" runat="server" Width="230" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>

                         <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control"  Width="210" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                         <asp:DropDownList ID="cbothnajaran"  AutoPostBack="true" OnSelectedIndexChanged="cboThnAjaran_SelectedIndexChanged" runat="server" CssClass="form-control" Width="120"></asp:DropDownList>
                         <asp:DropDownList ID="cboKelas" runat="server" CssClass="form-control" Width="150"></asp:DropDownList>
                         <asp:DropDownList ID="cboJnsTrans" runat="server" CssClass="form-control" Width="160"></asp:DropDownList>
                         <asp:Button runat="server" ID="Button1" CssClass="btn btn-primary" Text="Search" OnClick="btnPosting_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12 overflow-x-table">
                <asp:GridView ID="grdAccount" DataKeyNames="namaSiswa" SkinID="GridView" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Nama Siswa" SortExpression="Ket" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                               <asp:Label ID="Ket" runat="server" Text='<%# Eval("namaSiswa").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kelas" SortExpression="Ket" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                              <asp:Label ID="Ket" runat="server" Text='<%# Eval("kelas").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tunggakan" SortExpression="Tunggakan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                              <asp:Label ID="Ket" runat="server" Text='<%# Eval("tunggakan").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tagihan" SortExpression="Tagihan" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="tagiha" runat="server" Text='<%# Eval("nilai") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Thn Ajaran lalu" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="thnlalu" runat="server" Text='<%# Eval("thnlalu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Jul" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan1" runat="server" Text='<%# Eval("Jul") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ags" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan2" runat="server" Text='<%# Eval("Ags") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sept" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan3" runat="server" Text='<%# Eval("Sept") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Okt" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan4" runat="server" Text='<%# Eval("Okt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Nov" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan5" runat="server" Text='<%# Eval("Nov") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Des" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan6" runat="server" Text='<%# Eval("Des") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jan" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan7" runat="server" Text='<%# Eval("Jan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Feb" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan8" runat="server" Text='<%# Eval("Feb") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mar" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan9" runat="server" Text='<%# Eval("Mar") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Apr" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan10" runat="server" Text='<%# Eval("Apr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mei" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan11" runat="server" Text='<%# Eval("Mei") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jun" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="penerimaan12" runat="server" Text='<%# Eval("Jun") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="diskon" SortExpression="diskon" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="diskon" runat="server" Text='<%# Eval("diskon") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Saldo" SortExpression="ptd" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                                <asp:Label ID="Grup" runat="server" Text='<%# Eval("saldo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
     
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $('#BodyContent_grdAccount').gridviewScroll({
                width: '99%',
                height: 400,
                freezesize: 3, // Freeze Number of Columns. 
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
