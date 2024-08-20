<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="verifikasigaji.aspx.cs" Inherits="eFinance.Pages.Transaksi.Approval.verifikasigaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="card">
            <div class="card-body" style="margin-top: 5px;">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group ">
                            <div class="form-inline">
                                <div class="col-sm-8">
                                    <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return DeleteAll()" Text="Delete all selected" />--%>
                                    <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" Width="350" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group ">
                                        <div class="form-inline">
                                            <div class="col-sm-12">
                                                <label>Search :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdCustomer" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdCustomer_PageIndexChanging"
                    OnSelectedIndexChanged="grdCustomer_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <%# Container.DataItemIndex + 1 %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nik" SortExpression="nik" HeaderText="NIK Karyawan" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="nama" SortExpression="Nama" HeaderText="Nama" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Alamat" SortExpression="Alamat" HeaderText="Alamat" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="departemen" SortExpression="departemen" HeaderText="Departemen" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <div class="text-center">
                                    <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Detail" CommandName="Select" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="tabForm" runat="server" visible="false">
        <div class="card">
            <div class="panel panel-default">
                <div id="Tabs" role="tabpanel">
                    <!-- Nav tabs -->
                     <!-- Tab panes -->
                    <div class="tab-content tab-content-bordered">
                          <div class="row">
                                     <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">NUPTK</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNUPTK" placeholder="NUPTK" Enabled="false"></asp:TextBox>
                                                     <asp:HiddenField ID="hdnnoKaryawan" runat="server"/>
                                                    <asp:HiddenField ID="hdnnocabang" runat="server"/>
                                                    <asp:HiddenField ID="hdnIndex" runat="server"/>
                                                 </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Nama Karyawan</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNama" placeholder="Nama" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                           <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Status</label>
                                                <div class="col-sm-5">
                                                     <asp:DropDownList ID="cboStskaryawan" CssClass="form-control" runat="server" Enabled="false">
                                                           <asp:ListItem Value="1">Pegawai Tetap</asp:ListItem>
                                                           <asp:ListItem Value="0">Pegawai Tidak Tetap</asp:ListItem>
                                                      </asp:DropDownList>
                                                  </div>
                                            </div>                                 
                                         
                                        </div>
                                    </div>
                                    <%--pindah kolom--%>
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Tanggal Masuk</label>
                                                <div class="col-sm-3">
                                                        <asp:TextBox runat="server" CssClass="form-control date" ID="dtlahir" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Golongan</label>
                                                 <div class="col-sm-3">
                                                     <asp:DropDownList ID="cboGolPegawai" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                 </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Departemen</label>
                                                <div class="col-sm-4">
                                                     <asp:DropDownList ID="cboDepartemen" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                 </div>
                                            </div>
                                             <div class="form-group">
                                                <label class="col-sm-5 control-label text-right">Gaji Pokok</label>
                                                <div class="col-sm-4">
                                                     <asp:TextBox runat="server" CssClass="form-control money" ID="txtNilai" Enabled="true"></asp:TextBox>
                                                 </div>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div> 
                     <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdSaldoGL" DataKeyNames="noKomponengaji" SkinID="GridView" runat="server" ShowFooter="true" OnRowDataBound="grdSaldoGL_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Bind("noKomponengaji") %>' />
                                                   
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" HeaderText="Komponen Gaji">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("komponengaji") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Nilai">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtDebet" runat="server" CssClass="form-control money" Width="180" Text='<%# Bind("Nilai","{0:N2}") %>'></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">         
                      <div class="form-group row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="OK" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';"></asp:Button>
                                <asp:Button runat="server" ID="btnReject" CssClass="btn btn-primary" Text="Reject" OnClick="btnSimpan1_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';"></asp:Button>
                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="tabView" runat="server" visible="false">
    </div>
</asp:Content>
