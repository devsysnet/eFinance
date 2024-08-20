<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="mParameterdiscView.aspx.cs" Inherits="eFinance.Pages.Master.View.mParameterdiscView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete data?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "deleteall";
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        function CheckAllData(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdCabang.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <asp:HiddenField runat="server" ID="hdnID" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                                <%--<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Delete / Delete all"  OnClientClick="return DeleteAll()" CausesValidation="false" />--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdCabang" DataKeyNames="noParamdisc" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCabang_PageIndexChanging"
                                        OnSelectedIndexChanged="grdCabang_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheckAll" runat="server" CssClass="px" onclick="return CheckAllData(this);" />
                                                    </div>
                                                </HeaderTemplate>
                                                 <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:CheckBox ID="chkCheck" runat="server" CssClass="px chkCheck" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="namaDiskon" SortExpression="Nama Discount" HeaderText="Nama Discount" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="jenisTransaksi" SortExpression="Nama Transaksi" HeaderText="Nama Transaksi" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="drtgl" SortExpression="Mulai Discount" HeaderText="Mulai Discount" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="sdtgl" SortExpression="Akhir Discount" HeaderText="Akhir Discount" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="jns" SortExpression="Jenis" HeaderText="Jenis" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="nilai" SortExpression="Nilai" HeaderText="Nilai" ItemStyle-Width="10%" />
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
                            <div class="form-horizontal">
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nama Discount</label>
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" class="form-control " ID="namaDiskon" placeholder="Nama Discount" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Transaksi</label>
                                <div class="col-sm-5">
                                     <asp:DropDownList ID="cbojnstransaksi" runat="server" Enabled="false">
                                     </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Mulai Berlaku</label>
                                <div class="col-sm-4">
                                   <asp:TextBox runat="server" class="form-control date" ID="dtsistem" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Selesai Berlaku</label>
                                <div class="col-sm-4">
                                   <asp:TextBox runat="server" class="form-control date" ID="dtsistem1" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Discount</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cboStatus" runat="server" CssClass="form-control"  Width="150" Enabled="false">
                                              <asp:ListItem Value="-">----</asp:ListItem>
                                              <asp:ListItem Value="Nilai">Nilai</asp:ListItem>
                                              <asp:ListItem Value="Persen">Persented</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai Discount<span class="mandatory">*</span></label>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control money" Text="0.00" ID="txtnilai" placeholder="Nilai Denda" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="pjs-ex1-fullname" class="col-sm-3 control-label">COA Rekening<span class="mandatory">*</span></label>
                                <div class="col-sm-2">
                                   <asp:DropDownList ID="cbonorek" runat="server" Width="200" Enabled="false">
                                         <asp:ListItem Value="">---Pilih COA ---</asp:ListItem>
                                     </asp:DropDownList>
                                </div>
                            </div>
                           </div>
                            </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </asp:Content>

