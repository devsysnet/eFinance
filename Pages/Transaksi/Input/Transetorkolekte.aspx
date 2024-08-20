<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="Transetorkolekte.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.Transetorkolekte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
        <script type="text/javascript">
        function DeleteAll() {
            bootbox.confirm({
                message: "Are you sure to update data?",
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
            </script>
        <asp:Button ID="cmdMode" runat="server" OnClick="cmdMode_Click" Style="display: none;" Text="Mode" />
    <asp:HiddenField ID="hdnMode" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
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
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdSiswa" DataKeyNames="nokas" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSiswa_PageIndexChanging" OnRowCommand="grdSiswa_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="NO." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomorKode" HeaderText="Kode Transaksi" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd-MMM-yyyy}"/>
                                            <asp:BoundField DataField="JenisKolekte" HeaderText="Jenis Kolekte" HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="uraian" HeaderText="Uraian" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Nilai" HeaderText="Nilai" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N}"/>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSelectEdit" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Submit" CommandName="SelectEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
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
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Kode Transaksi</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="nomorKode" type="text" Enabled="false" Width="150"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Jenis Kolekte </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="JenisKolekte" type="text" Enabled="false" Width="250"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Kolekte</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="tgl" runat="server" CssClass="form-control date"  Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Tanggal Transaksi</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="tgl1" runat="server" CssClass="form-control date"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Uraian</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="uraian" TextMode="MultiLine"  Enabled="false" Width="250"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Nilai </label>
                                        <div class="col-sm-7">
                                            <asp:TextBox runat="server" class="form-control " ID="nilai" type="text" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Dari Rekening </label>
                                        <div class="col-sm-5">
                                             <asp:DropDownList runat="server" ID="cbobankasal" CssClass="form-control" Width="200"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="pjs-ex1-fullname" class="col-sm-3 control-label">Transfer Ke Rekening </label>
                                        <div class="col-sm-5">
                                             <asp:DropDownList runat="server" ID="cbobank" CssClass="form-control" Width="250"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <div class="col-md-12">
                                         <div class="text-center">
                                            <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Simpan"   UseSubmitBehavior="false" OnClientClick="DeleteAll()" />
                                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Kembali" OnClick="btnCancel_Click"></asp:Button>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
