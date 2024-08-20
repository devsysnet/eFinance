<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="TransConfirmDO.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.TransConfirmDO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-body">
                            <%--START GRID--%>
                            <div id="tabGrid" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group ">
                                            <div class="form-inline">
                                                <div class="col-sm-12">
                                                    <label>DO number :</label>
                                                    <asp:TextBox ID="txtCariData" runat="server" CssClass="form-control" placeholder="Enter DO Number"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="table-responsive">
                                            <asp:HiddenField runat="server" ID="hdnRowIndexShipment" />
                                            <asp:HiddenField runat="server" ID="hdnNoDO" />
                                            <asp:GridView ID="grdDOView" DataKeyNames="noDO" SkinID="GridView" runat="server" OnSelectedIndexChanged="grdDOView_SelectedIndexChanged" OnPageIndexChanging="grdDOView_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <%-- <asp:HiddenField runat="server" ID="hdnNoMataUang" Value='<%#Eval("noMataUang") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnNoSup" Value='<%#Eval("noSup") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnNoDoc" Value='<%#Eval("noDocScreening") %>' />
                                                                <asp:HiddenField runat="server" ID="kdnKdEta" Value='<%#Eval("kodeEta") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnTglEta" Value='<%#Eval("tglShipment") %>' />
                                                                <asp:HiddenField runat="server" ID="hdnKurs" Value='<%#Eval("kurs") %>' />
                                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("kurs") %>' />--%>
                                                                <asp:HiddenField runat="server" ID="hdnNoSup" Value='<%#Eval("noCust") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="kdDO" HeaderStyle-CssClass="text-center" HeaderText="DO Number" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="tglDO" HeaderStyle-CssClass="text-center" HeaderText="DO Date" ItemStyle-Width="10%" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="kdCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Code" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="namaCust" HeaderStyle-CssClass="text-center" HeaderText="Customer Name" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <div class="text-center">
                                                                <asp:Button ID="btnDetail" runat="server" class="btn btn-primary btn-sm" Text="Confirm" CommandName="select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
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
                                <asp:HiddenField ID="hdnNoSupData" runat="server" />
                                <asp:HiddenField ID="hdnNoMataUangData" runat="server" />
                                <asp:HiddenField ID="hdnnoDocScreening" runat="server" />
                                <asp:HiddenField ID="hdnCountGridPIBDetail" runat="server" />
                                <asp:HiddenField ID="hdnNoCust" runat="server" />
                                <asp:HiddenField ID="hdnNoCabang" runat="server" />
                                <div class="row">
                                    <div class="form-horizontal">
                                        <div class="col-sm-12">
                                            <%--  <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label ">DO Confirm Code :</label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblSupCode" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">DO Confirm Date :</label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="dtDOConfirmDate" runat="server" CssClass="form-control date"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">DO Number</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblDONumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">DO Date</label>
                                                <div class="col-sm-7">
                                                    <asp:Label runat="server" ID="lblDoDate"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">Customer Code</label>
                                                <div class="col-sm-7">
                                                    <asp:Label runat="server" ID="lblCustomerCode"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="pjs-ex1-fullname" class="col-sm-5 control-label text-right">Customer Name</label>
                                                <div class="col-sm-7">
                                                    <asp:Label runat="server" ID="lblCustomer"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 overflow-x-table">
                                        <asp:GridView ID="grdDOConfirmDetail" DataKeyNames="noDOD" SkinID="GridView" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hdnNoDOD" runat="server" Value='<%# Bind("noDOD") %>' />
                                                            <asp:HiddenField ID="hdnNoProduk" runat="server" Value='<%# Bind("noproduct") %>' />

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKdProduk" runat="server" Text='<%# Bind("prodno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNamaProduk" runat="server" Text='<%# Bind("prodnm") %>' Width="150"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Satuan" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <div class="form-inline">
                                                    <asp:TextBox ID="txtQtyConfrim" runat="server" Width="70" class="form-control" Text='<%# Eval("qtyDO") %>' Enabled="false"></asp:TextBox>

                                                                <span style="font-weight: bold;">
                                                                    <asp:Label ID="lblPUnitShipment" runat="server" Text='<%# Bind("punit") %>'></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Satuan Besar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <div class="form-inline">
                                                                <asp:TextBox ID="txtQtySatuanBesarConfrim" runat="server" Width="70" class="form-control" Text='<%# Eval("qtyDOSatuanBesar") %>' Enabled="false"></asp:TextBox>
                                                                <span style="font-weight: bold;">
                                                                    <asp:Label ID="lblPUnitConfirm2" runat="server" Text='<%# Bind("satuanBesar") %>'></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Qty Satuan Besar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <div class="text-center">
                                                            <div class="form-inline">
                                                                <asp:TextBox ID="txtQtySatuanBesar1Confrim" runat="server" Width="70" class="form-control" Text='<%# Eval("qtyDOSatuanBesar1") %>' Enabled="false"></asp:TextBox>
                                                                <span style="font-weight: bold;">
                                                                    <asp:Label ID="lblPUnitConfirm3" runat="server" Text='<%# Bind("satuanbesar1") %>'></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-success" Text="Save" OnClick="btnSimpan_Click"></asp:Button>
                                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSimpan" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
