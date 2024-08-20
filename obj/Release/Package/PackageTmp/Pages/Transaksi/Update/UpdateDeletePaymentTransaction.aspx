<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="UpdateDeletePaymentTransaction.aspx.cs" Inherits="eFinance.Pages.Transaksi.Update.UpdateDeletePaymentTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function EditRow(id) {
            document.getElementById('<%=hdnMode.ClientID%>').value = "edit";
            document.getElementById('<%=hdnId.ClientID%>').value = id;
            eval("<%=execBind%>");
            return false;
        }
        function DeleteRow(id, name) {
            bootbox.confirm({
                message: "Anda yakin untuk menghapus data <b>" + name + "</b> ?",
                callback: function (result) {
                    if (result == true) {
                        document.getElementById('<%=hdnMode.ClientID%>').value = "delete";
                        document.getElementById('<%=hdnId.ClientID%>').value = id;
                        eval("<%=execBind%>");
                    }
                },
                className: "bootbox-sm"
            });
            return false;
        }
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to delete all selected data?",
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
            var GridVwHeaderChckbox = document.getElementById("<%=grdPTKP.ClientID %>");
            for (var i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
        <asp:HiddenField ID="hdnnoYysn" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return DeleteAll()" Text="Delete all selected" />
                            </div>
                            <div class="col-sm-5">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                            <span>Filter Cari</span>
                                        <asp:DropDownList ID="cboPerwakilan" runat="server" Width="200" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="cboPerwakilan_SelectedIndexChanged"></asp:DropDownList>
                                         <asp:DropDownList ID="cboUnit" runat="server" Width="200" CssClass="form-control"   ></asp:DropDownList>

                                        </div>
                                 </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-inline">
                                    <div class="col-sm-12">
                                        <label>Periode : </label>
                                        <asp:TextBox Visible="false" ID="txtSearch" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="PTKP Name"></asp:TextBox>
                                         <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                                        <asp:Button ID="btnCari" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnCari_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdPTKP" DataKeyNames="payment_transaction_id" SkinID="GridView" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
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
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="va" SortExpression="va" HeaderText="VA" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Nama Siswa" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="amount" SortExpression="sts" HeaderText="Nilai" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="payment_date" SortExpression="sts" HeaderText="Tanggal" ItemStyle-Width="10%"  DataFormatString="{0:dd MMM yyyy}" />
                                            <asp:BoundField DataField="reference_id" SortExpression="sts" HeaderText="Nama Cabang" ItemStyle-Width="15%" />
                                            <asp:BoundField DataField="code" SortExpression="sts" HeaderText="Kode" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="status" SortExpression="sts" HeaderText="Status" ItemStyle-Width="5%" />
                                            <asp:TemplateField HeaderText="EDIT" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <a href="javascript:void()" class="btn btn-xs btn-labeled btn-primary" onclick="return EditRow('<%#Eval("payment_transaction_id")%>')">Edit</a>
                                                        <%--<a href="javascript:void()" class="btn btn-xs btn-labeled btn-danger" onclick="return DeleteRow('<%#Eval("noPTKP")%>', '<%#Eval("PTKP")%>')">Delete</a>--%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END GRID--%>
                    <%--START FORM--%>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Nama Cabang </label>
                                <div class="col-sm-3">
                                    <asp:Label runat="server" ID="lblCabang" ></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">VA  </label>
                                <div class="col-sm-3">
                                    <asp:Label runat="server" ID="lblva" ></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Nama Siswa  </label>
                                <div class="col-sm-3">
                                    <asp:Label runat="server" ID="lblnama" ></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Tanggal </label>
                                <div class="col-sm-3">
                                    <asp:Label runat="server" ID="lbltgl" ></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Kode </label>
                                <div class="col-sm-3">
                                    <asp:Label runat="server" ID="lblCode" ></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Amount</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNominal" runat="server" CssClass="form-control  "  MaxLength="50" placeholder="Nominal"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-sm-3 control-label"></label>
                                <div class="col-sm-5">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--END FORM--%>
                    <%--START VIEW--%>
                    <div id="tabView" runat="server" visible="false">
                    </div>
                    <%--END VIEW--%>
                </div>
            </div>
        </div>
    </div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
        <asp:PostBackTrigger ControlID="btnCari" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
