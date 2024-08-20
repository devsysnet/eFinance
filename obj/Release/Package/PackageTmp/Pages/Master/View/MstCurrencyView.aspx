<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="MstCurrencyView.aspx.cs" Inherits="eFinance.Pages.Master.View.MstCurrencyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function EditRow(id) {
            document.getElementById('<%=hdnMode.ClientID%>').value = "edit";
            document.getElementById('<%=hdnId.ClientID%>').value = id;
            eval("<%=execBind%>");
            return false;
        }
    </script>
    <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group ">
                                    <div class="form-inline">
                                        <div class="col-sm-12">
                                            <label>Search :</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" CausesValidation="false" OnClick="btnCari_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grdCurrency" DataKeyNames="noMataUang" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCurrency_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <%# Container.DataItemIndex + 1 %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="kodeMataUang" SortExpression="kodeMataUang" HeaderText="Kode Mata Uang" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="namaMataUang" SortExpression="namaMataUang" HeaderText="Nama Mata Uang" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Negara" SortExpression="negara" HeaderText="Country" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <div class="text-center">
                                            <a href="javascript:void()" class="btn btn-primary btn-sm" onclick="return EditRow('<%#Eval("noMataUang")%>')">Detail</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Name :</label>
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" CssClass="form-label" ID="txtCurrencyName"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Code :</label>
                                        <div class="col-sm-4">
                                            <asp:Label for="pjs-ex1-fullname" runat="server" CssClass="form-label" ID="txtCode"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Country :</label>
                                        <div class="col-sm-4">
                                            <asp:Label for="pjs-ex1-fullname" runat="server" CssClass="form-label" ID="txtCountry"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Currency Status :</label>
                                        <div class="col-sm-4">
                                            <asp:Label for="pjs-ex1-fullname" ID="lblSts" runat="server" CssClass="form-label"></asp:Label>
                                            <asp:HiddenField ID="hdnSts" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="pjs-ex1-fullname" class="col-sm-4 control-label text-right">Set As Default :</label>
                                        <div class="col-sm-4">
                                            <asp:Label for="pjs-ex1-fullname" ID="lblSet" runat="server" CssClass="form-label">No</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md-12">
                                    <div class="text-center">
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnReset_Click" />
                                    </div>
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

</asp:Content>
