<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransBudgetUpdate.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.TransBudgetUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
   <style>
       .blm{
               background-color: #6c757d;
    border-color: #6c757d;
    border:none;
    padding:6px;
    border-radius:20px;
    color:white;
    font-size:8pt;
    width:100px;
       }
       .reject{
           background-color: #dc3545;
            border-color: #dc3545;
            border:none;
            padding:6px;
            border-radius:20px;
            color:white;
            font-size:8pt;
    width:100px;

       }
       .appr{
           background-color: #198754;
            border-color: #198754;
            border:none;
            padding:6px;
            border-radius:20px;
            color:white;
            font-size:8pt;
    width:100px;

       }
   </style>
    <asp:HiddenField ID="hdnThn" runat="server" />
    <asp:HiddenField ID="hdnCbg" runat="server" />
    <div id="tabGrid" runat="server">
        <div class="row">
            <div class="col-sm-5">
                <div class="form-inline">
                    <div class="col-sm-10">
                        <label>Periode :   </label>
                        <asp:DropDownList ID="cboYear" runat="server" Width="150">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-7">
                <div class="form-inline">
                    <div class="col-sm-12">
                        <label>Cabang : </label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang" Width="270"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div class="table-responsive">
                    <asp:GridView ID="grdBudgetAwal" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdBudgetAwal_PageIndexChanging" OnRowCommand="grdBudgetAwal_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:HiddenField ID="hdnThn" Value='<%# Eval("thn") %>' runat="server" />
                                        <asp:HiddenField ID="hdnCbg" Value='<%# Eval("nocabang") %>' runat="server" />
                                        <asp:HiddenField ID="hdnsts" Value='<%# Eval("stsAppr") %>' runat="server" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="thn" HeaderText="Tahun" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="namaCabang" HeaderText="Cabang" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField  HeaderStyle-CssClass="text-center" HeaderText="Status" ItemStyle-Width="10%">
                                <ItemTemplate >
                                    <div class="text-center">
                                        <asp:Button ID="stsApprove" runat="server" Visible="false" class="btn btn-xs btn-labeled btn-success" cssClass="appr" Text="Approve" />
                                        <asp:Button ID="stsReject" runat="server" Visible="false" class="btn btn-xs btn-labeled btn-danger" cssClass="reject" Text="Reject" />
                                        <asp:Button ID="stsBlmAppr" runat="server" Visible="false" class="btn btn-xs btn-labeled btn-dark" cssClass="blm" Text="Belum Approve" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="reasson" HeaderText="Reasson" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                              
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">

                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Ubah" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <div class="text-center">
                                        <asp:Button ID="btnSelectDel" runat="server" class="btn btn-xs btn-labeled btn-danger" Text="Hapus" CommandName="SelectDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div id="tabForm" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="table-responsive overflow-x-table" id="pajak" visible="false" runat="server">
                                <asp:GridView ID="grdBudget" DataKeyNames="ket" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Eval("noBudgetD") %>' />
                                                            <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Eval("noBudget") %>' />
                                                            <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                            <asp:Label runat="server" ID="txtkdRek" Text='<%# Eval("kdRek") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Januari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jan") %>' ID="txtJanuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Februari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("feb") %>' ID="txtFebuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Maret" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mar") %>' ID="txtMaret"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="April" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("apr") %>' ID="txtApril"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mei" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mei") %>' ID="txtMei"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juni" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jun") %>' ID="txtJuni"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juli" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jul") %>' ID="txtJuli"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Agustus" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("ags") %>' ID="txtAgustus"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="September" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("sep") %>' ID="txtSeptember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Oktober" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("okt") %>' ID="txtOktober"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="November" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("nov") %>' ID="txtNovember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Desember" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("dese") %>' ID="txtDesember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="table-responsive overflow-x-table" id="tahunAjaran" visible="false" runat="server">
                                <asp:GridView ID="GridView1" DataKeyNames="ket" SkinID="GridView" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Akun" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="text-center">
                                                            <asp:HiddenField ID="hdnnoBudgetD" runat="server" Value='<%# Eval("noBudgetD") %>' />
                                                            <asp:HiddenField ID="hdnnoBudget" runat="server" Value='<%# Eval("noBudget") %>' />
                                                            <asp:HiddenField ID="hdnNoRek" runat="server" Value='<%# Eval("noRek") %>' />
                                                            <asp:Label runat="server" ID="txtkdRek" Text='<%# Eval("kdRek") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Nama Akun" SortExpression="Ket" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="Ket" runat="server" Text='<%# Eval("ket").ToString().Replace(" ", "&nbsp;") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juli" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jul") %>' ID="txtJuli"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Agustus" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("ags") %>' ID="txtAgustus"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="September" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("sep") %>' ID="txtSeptember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Oktober" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("okt") %>' ID="txtOktober"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="November" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("nov") %>' ID="txtNovember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Desember" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("dese") %>' ID="txtDesember"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Januari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jan") %>' ID="txtJanuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Februari" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("feb") %>' ID="txtFebuari"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Maret" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mar") %>' ID="txtMaret"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="April" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("apr") %>' ID="txtApril"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Mei" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("mei") %>' ID="txtMei"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Juni" SortExpression="ptd" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" class="form-control money width-100" Text='<%# Eval("jun") %>' ID="txtJuni"></asp:TextBox>
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
        <br />
        <div class="form-group row" runat="server" id="button">
            <div class="col-md-12">
                <div class="text-center">
                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-primary" Text="Simpan" OnClick="btnSimpan_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>


