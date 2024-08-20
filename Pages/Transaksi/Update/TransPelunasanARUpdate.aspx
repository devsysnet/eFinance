<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransPelunasanARUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransPelunasanARUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteRow(noPiutang, noSiswa, kdBayar, namaSiswa, tgl) {
            bootbox.confirm({
                message: "Yakin untuk menghapus data <b>" + namaSiswa + "</b> tgl transaksi " + tgl + " ?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "delete";
                        document.getElementById('<%=hdnNoPiut.ClientID%>').value = noPiutang;
                        document.getElementById('<%=hdnNoSiswa.ClientID%>').value = noSiswa;
                        document.getElementById('<%=hdnkdBayar.ClientID%>').value = kdBayar;
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField ID="hdnNoPiut" runat="server"/>
    <asp:HiddenField ID="hdnNoSiswa" runat="server"/>
    <asp:HiddenField ID="hdnkdBayar" runat="server"/>
     <asp:HiddenField ID="hdncreatedBy" runat="server"/>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label text-left">Kode Bayar <span class="mandatory">*</span></label>
                                        <div class="col-sm-5">
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtKdBayar" runat="server" CssClass="form-control" placeholder="Masukan Kode Bayar"></asp:TextBox>
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                <asp:Button ID="btndelete1" runat="server" CssClass="btn btn-primary btn-sm" Text="Delete All" OnClick="btndelete_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdARSiswa" DataKeyNames="nopiutang" SkinID="GridView" runat="server" OnPageIndexChanging="grdARSiswa_PageIndexChanging"
                                         OnRowCommand="grdARSiswa_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                     </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nis" HeaderText="NIS" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="namasiswa" HeaderText="Nama Siswa" HeaderStyle-CssClass="text-center" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="kelas" HeaderText="Kelas" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                           <%-- <asp:BoundField DataField="jenisTransaksi" HeaderText="Tagihan" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left" />--%>
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="piutang" HeaderText="Piutang" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <asp:BoundField DataField="nilaiBayar" HeaderText="Bayar" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detail" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div id="tabForm" runat="server" visible="false">
                         <asp:HiddenField ID="hdnNoPiutang" runat="server"/>
                          <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Bayar</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtKodeBayar" placeholder="Kode Bayar" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                         <label for="pjs-ex1-fullname" class="col-sm-3 control-label text-right">Tgl Bayar :</label>
                                         <div class="col-sm-3">
                                             <asp:TextBox ID="txtTglBayar" runat="server" CssClass="form-control date" Enabled="true"></asp:TextBox>
                                         </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tagihan</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtTagihan" placeholder="Tagihan" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kelas</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtKelas" placeholder="Kelas" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Siswa</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control " ID="txtNamaSiswa" placeholder="Nama Siswa" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Piutang</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtPiutang" placeholder="Piutang" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                   <%-- <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Saldo</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtSaldo" placeholder="Saldo" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Bayar</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox runat="server" CssClass="form-control money" ID="txtBayar" placeholder="Bayar" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                   &nbsp;
                                </div>
                            </div>
                        </div>
                         <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" ></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-primary" Text="Back" OnClick="btnReset_Click" ></asp:Button>
                                </div>
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
