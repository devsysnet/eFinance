<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Mstgajikaryawan.aspx.cs" Inherits="eFinance.Pages.Master.Input.Mstgajikaryawan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group ">                      
                                    <div class="col-sm-4">
                                        <div class="form-group ">
                                            <asp:DropDownList ID="cboCabang" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboCabang_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                        <div class="form-group ">
                                            <asp:DropDownList ID="cboUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group ">
                                            <div class="form-inline">
                                                <label>Filter :</label>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Cari" OnClick="btnCari_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdGajiKar" DataKeyNames="noKaryawan" SkinID="GridView" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdGajiKar_PageIndexChanging"
                                        OnSelectedIndexChanged="grdGajiKar_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nik" HeaderText="NIK" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="nama" HeaderText="Nama" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="Alamat" HeaderText="Alamat" ItemStyle-Width="20%" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="departemen" HeaderText="Departemen" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center" />
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
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">NUPTK</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNUPTK" placeholder="NUPTK" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdnnoKaryawan" runat="server" />
                                            <asp:HiddenField ID="hdnnocabang" runat="server" />
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
                                            <asp:DropDownList ID="cboStskaryawan" CssClass="form-control" runat="server">
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
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtmasuk" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Golongan</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cboGolPegawai" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label text-right">Departemen</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cboDepartemen" CssClass="form-control" runat="server"></asp:DropDownList>
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
                        <div class="row" style="margin-left:-90px">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <%--<div class="form-group">
                                            <label class="col-sm-3 control-label">Aksi <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboAksi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboAksi_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Aksi---</asp:ListItem>
                                                    <asp:ListItem Value="Import">Upload</asp:ListItem>
                                                    <asp:ListItem Value="Export">Download</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                       <%-- <div class="form-group" runat="server" id="showhidestatus">
                                            <label class="col-sm-3 control-label">Status <span class="mandatory">*</span></label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="">---Pilih Status Budget---</asp:ListItem>
                                                    <asp:ListItem Value="New">New</asp:ListItem>
                                                    <asp:ListItem Value="Revisi">Revisi</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                      <%--  <div class="form-group" runat="server" id="showhidetahun">
                                            <label class="col-sm-3 control-label">Tahun Ajaran<span class="mandatory">*</span></label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>--%>
                                       <%-- <div class="form-group" runat="server" id="showhidefile">
                                            <label class="col-sm-3 control-label">Pilih File <span class="mandatory">*</span></label>
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="divtombol">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-9">
                                                <asp:Button runat="server" ID="btnImport" CssClass="btn btn-success" Text="Upload" OnClick="btnImport_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                <asp:Button runat="server" ID="btnExport" CssClass="btn btn-primary" Text="Download" OnClick="btnExport_Click"></asp:Button>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSaldoGL" DataKeyNames="noKomponengaji" SkinID="GridView" runat="server"  autoGenerateColumns="false"  >
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
                                                        <asp:TextBox ID="txtDebet" runat="server" CssClass="form-control money" value="0.00" Width="180" onblur="return Calculate()" onchange="return Calculate()"></asp:TextBox>
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
                                        <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses Simpan';"></asp:Button>
                                        <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Batal" OnClick="btnReset_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tabView" runat="server" visible="false">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


