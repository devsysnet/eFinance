<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MaterVAsiswa.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.MaterVAsiswa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function OpenReport() {
            //            OpenReportViewer();
            window.open('../../../Report/ReportViewer.aspx', 'name', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,modal=yes,width=1000,height=600');
        }
        function money(money) {
            var num = money;
            if (num != "") {
                var array = num.toString().split('');
                var index = -3;
                while (array.length + index > 0) {
                    array.splice(index, 0, ',');
                    index -= 4;
                }

                money = array.join('') + '.00';
            } else {
                money = '0.00';
            }
            return money;
        }


    </script>
    <asp:HiddenField ID="hdnId" runat="server" Value="0" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                                                 
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Cabang <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="cboCabang" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Jenis VA <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                              <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Kategori VA---</asp:ListItem>
                                                    <asp:ListItem Value="Close">Close</asp:ListItem>
                                                    <asp:ListItem Value="Open">Open</asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Kelas <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboKelas" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboKelas_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Jenis VA <span class="mandatory">*</span></label>
                                        <div class="col-sm-9">
                                              <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Kategori VA---</asp:ListItem>
                                                    <asp:ListItem Value="Close">Close</asp:ListItem>
                                                    <asp:ListItem Value="Open">Open</asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right"></label>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click" ></asp:Button>  
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                                <asp:Button runat="server" Visible="false" ID="btnPrint" CssClass="btn btn-success" Text="Print" OnClick="print"></asp:Button>

                                    <br />
                                    <asp:GridView ID="grdARSiswa"  DataKeyNames="noSiswa" SkinID="GridView" runat="server" AllowPaging="false" PageSize="10"  OnRowCommand="grdARSiswa_RowCommand" OnPageIndexChanging="grdARSiswa_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                        <asp:HiddenField ID="hdnIdPrint" runat="server" value='<%# Eval("noSiswa") %>'/>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="namaSiswa" HeaderText="Nama Siswa" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="noVA" HeaderText="No.VA" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="text-center" />
                                                 <asp:TemplateField HeaderText="" Visible ="false" HeaderStyle-CssClass="text-center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-success" Text="Print" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


