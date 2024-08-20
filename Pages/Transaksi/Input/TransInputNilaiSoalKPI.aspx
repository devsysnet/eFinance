<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransInputNilaiSoalKPI.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransInputNilaiSoalKPI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="card">
                            <div class="card-body" style="margin-top: 5px;">
                                <div class="row">
                                   <%-- <div class="col-sm-12" >
                                        <div class="form-group" style="display:none">
                                            <div class="form-inline">
                                                <div class="col-sm-7">
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="form-group ">
                                                        <div class="form-inline">
                                                            <div class="col-sm-12">
                                                                <label>Search :</label>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search.."></asp:TextBox>
                                                                <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnCari_Click" CausesValidation="false" />
                                                                 <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary btn-sm" Text="Download" OnClick="btnExport_Click" CausesValidation="false" />
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                 <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-2 control-label">Tanggal</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="dttgl" runat="server" CssClass="form-control date"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-2 control-label">Unit</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cboUnit" Width="250" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboUnit_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-2 control-label">Karyawan</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cboKaryawan" Width="250" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-2 control-label">Unsur Penilaian</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="cboSoal" Width="250" runat="server" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="cbosoal_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="grdAccount" DataKeyNames="nosoal" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Bind("nosoal") %>' />

                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kode Soal" SortExpression="kdRek" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="kdRek" runat="server" Text='<%# Eval("kodesoal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uraian Unsur Yang Dinilai" SortExpression="Ket" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("uraian").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%" HeaderText="Nilai">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:TextBox ID="txtNilai" runat="server" CssClass="form-control  " value="0" Width="180" OnSelectedIndexChanged="jenis_SelectedIndexChanged" AutoPostBack="true" ></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       </Columns>
                                </asp:GridView>

                                 <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="form-inline">
                                                <label class="col-sm-1 control-label">Catatan</label>
                                                <div class="col-sm-11">
                                                      <asp:TextBox ID="txtKeterangan" Width="350" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="row">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" CausesValidation="false" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';" />
                            </div>
                        </div>
                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>