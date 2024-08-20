<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="jurnalBPJS.aspx.cs" Inherits="eFinance.Pages.Transaksi.Upload.jurnalBPJS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="form-inline">
                                    <label class="col-sm-3 control-label">Upload File .txt / .csv <span class="mandatory">*</span></label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="cboflUpload" runat="server" CssClass="form-control" Width="120">
                                            <asp:ListItem Value="csv/txt" Selected="True">CSV / TXT</asp:ListItem>
                                            <%--<asp:ListItem Value="xls/xlsx">XLS / XLSX</asp:ListItem>--%>
                                        </asp:DropDownList>    
                                        <asp:FileUpload ID="flUpload" Class="fileupload" runat="server" />
                                        <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" Text="Upload" OnClick="btnUpload_Click"></asp:Button>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" runat="server" id="gridUpload">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdUploadVA" SkinID="GridView" runat="server" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="namacabang" HeaderText="Unit" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="tgl" HeaderText="Tanggal" HeaderStyle-CssClass="text-center" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd-MMM-yyyy}"  />
                                     <asp:BoundField DataField="JHT37" HeaderText="JHT 3,7%" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n2}"/>
                                    <asp:BoundField DataField="JKMJKK" HeaderText="JKM+JKK" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n2}"/>
                                    <asp:BoundField DataField="JHT2" HeaderText=" JHT 2%" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <asp:BoundField DataField="JP1" HeaderText="JP1%" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n2}"/>
                                    <asp:BoundField DataField="TotalIuran" HeaderText="TOTAL IURAN" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n2}"/>
                                    <asp:BoundField DataField="BPJS5" HeaderText="BPJS 5%" HeaderStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:n2}"/>
                               </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomContent" runat="server">
</asp:Content>
