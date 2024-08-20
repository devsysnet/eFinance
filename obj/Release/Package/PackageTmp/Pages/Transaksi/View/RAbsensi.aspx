<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="RAbsensi.aspx.cs" Inherits="eFinance.Pages.Transaksi.View.RAbsensi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
        .ChildGrid {
            background: #fff;
            margin-left: 4%;
            width: 96%;
        }

            .ChildGrid td {
                font-size: 11px;
            }

                .ChildGrid td .form-control {
                    padding: 1px 3px;
                    height: 20px;
                    font-size: 11px;
                }

            .ChildGrid th {
                color: #fff;
                font-size: 12px;
                background: #aaaaaa !important;
                border: #aaaaaa;
            }
    </style>
    <script type="text/javascript">
        function expandCollapse(name) {
            var div = document.getElementById(name);
            var img = document.getElementById('img' + name);
            if (div.style.display == 'block') {
                div.style.display = 'none';
                img.src = '<%=Func.BaseUrl%>assets/images/plus.png';
            } else {
                div.style.display = 'block';
                img.src = '<%=Func.BaseUrl%>assets/images/minus.png';
            }
        }
    </script>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Tanggal : </label>
                                            <div class="col-sm-8">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="dtMulai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp; 
                                                    <asp:TextBox ID="dtSampai" runat="server" CssClass="form-control date"></asp:TextBox>&nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Cabang : </label>
                                            <div class="col-sm-7">
                                                <div class="input-group">
                                                    <div class="form-inline">
                                                        <div class="input-group-btn">
                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="cboCabang"></asp:DropDownList>&nbsp;
                                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Cari" OnClick="btnSearch_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = true; this.value = 'Proses...';"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdviewAbsen" SkinID="GridView" runat="server" DataKeyNames="tgl" OnRowDataBound="grdviewAbsen_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <a href="JavaScript:expandCollapse('<%# Eval("tgl") %>');">
                                                        <img src="<%=Func.BaseUrl%>assets/images/plus.png" id="img<%# Eval("tgl") %>" />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <div class="text-center">
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-CssClass="text-center" DataField="tgl" HeaderText="Tanggal" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100">
                                                            <div id="<%# Eval("tgl") %>" style="display: none;">
                                                                <asp:GridView ID="grdviewAbsenDetil" runat="server" CssClass="ChildGrid" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                                                            <ItemTemplate>
                                                                                <div class="text-center">
                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="nik" HeaderText="NIK" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="nama" HeaderText="Nama Karyawan" HeaderStyle-CssClass="text-center" />
                                                                        <asp:BoundField DataField="jamMasuk" HeaderText="Jam Masuk" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="jamKeluar" HeaderText="Jam Keluar" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>

