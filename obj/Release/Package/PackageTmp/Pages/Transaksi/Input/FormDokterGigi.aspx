<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="FormDokterGigi.aspx.cs" Inherits="eFinance.Pages.Transaksi.Input.FormDokterGigi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="hdnId" runat="server" />
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <%--START GRID--%>
                    <div id="tabGrid" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdRegister" DataKeyNames="noMedik" SkinID="GridView" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdRegister_PageIndexChanging" OnRowCommand="grdRegister_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="tglmedik" HeaderStyle-CssClass="text-center" HeaderText="Tgl Register" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:BoundField DataField="noRegister" HeaderStyle-CssClass="text-center" HeaderText="No Register" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="namaPasien" HeaderStyle-CssClass="text-center" HeaderText="Nama Pasien" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="uraian" HeaderStyle-CssClass="text-center" HeaderText="Keluhan" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Button ID="btnSelectDetil" runat="server" class="btn btn-xs btn-labeled btn-primary" Text="Detil" CommandName="SelectDetil" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CausesValidation="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabForm" runat="server" visible="false">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-left">No. Registrasi <span class="mandatory">*</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField runat="server" ID="hdnReg" />
                                            <div class="input-group-btn">
                                                <asp:TextBox ID="txtNoReg" Enabled="false" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Nama </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtNamaReg" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Alamat </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAlamatReg" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="form-inline">
                                        <label class="col-sm-3 control-label">Umur </label>
                                        <div class="col-sm-9">
                                            <asp:Textbox runat="server" ID="txtThn" CssClass="form-control" Placeholder="Thn" Width="50" Enabled="false"></asp:Textbox> Thn
                                            <asp:Textbox runat="server" ID="txtBln" CssClass="form-control" Placeholder="Bln" Width="50" Enabled="false"></asp:Textbox> Bln
                                            <%--<asp:Textbox runat="server" ID="txtHari" CssClass="form-control" Placeholder="Hr" Width="80"></asp:Textbox>--%>
                                        </div>
                                            </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tgl Periksa</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control date" ID="dtPeriksa"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-8">
                                    <div class="form-group">
                                        <label class="col-sm-0 control-label text-left">I. Keadaan Rongga Mulut</label>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Celah bibir/langit-langit </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo1" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Luka pada sudut mulut </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo2" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Stomatitis Aphtosa </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo3" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Lidah kotor </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo4" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Lesi-lesi lain </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo5" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Lokasi </label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLokasi" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="col-sm-8">
                                    <div class="form-group">
                                        <label class="col-sm-0 control-label text-left">II. Kondisi Gigi</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="grd1" SkinID="GridView" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="gigiSusu" HeaderStyle-CssClass="text-center" HeaderText="Gigi Susu" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="gigiTetap" HeaderStyle-CssClass="text-center" HeaderText="Gigi Tetap" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="statusGigi" HeaderStyle-CssClass="text-center" HeaderText="Status Gigi" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="form-horizontal">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">55</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">54</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">53</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">52</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">51</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">17</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">16</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">15</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">14</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">13</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">12</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">11</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <%--Left Top--%>
                                                            <asp:Textbox runat="server" ID="txtLT1" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT2" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT3" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT4" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT5" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT6" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLT7" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                        </div>
                                                     </div>
                                                </div>
                                                                                                <hr />
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <%--Left Bottom--%>
                                                            <asp:Textbox runat="server" ID="txtLB1" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB2" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB3" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB4" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB5" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB6" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtLB7" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                        </div>
                                                     </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">47</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">46</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">45</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">44</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">43</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">42</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">41</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">85</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">84</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">83</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">82</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">81</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">61</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">62</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">63</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">64</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">65</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">21</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">22</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">23</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">24</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">25</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">26</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">27</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <%--Right Top--%>
                                                            <asp:Textbox runat="server" ID="txtRT1" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT2" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT3" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT4" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT5" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT6" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRT7" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                        </div>
                                                     </div>
                                                </div>
                                                <hr />
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <%--Right Bottom--%>
                                                            <asp:Textbox runat="server" ID="txtRB1" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB2" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB3" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB4" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB5" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB6" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                            <asp:Textbox runat="server" ID="txtRB7" CssClass="form-control text-center" Width="50"></asp:Textbox>
                                                        </div>
                                                     </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">31</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">32</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">33</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">34</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">35</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">36</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">37</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-inline">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">71</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">72</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">73</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">74</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50">75</asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
                                                            <asp:Label runat="server" CssClass="form-label text-center" Width="50"></asp:Label>
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
                            <div class="form-horizontal">
                                <div class="col-sm-8">
                                    <div class="form-group">
                                        <label class="col-sm-0 control-label text-left">III. Keadaan gusi, kebersihan gigi dan Kondisi lainnya</label>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gusi sehat </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo6" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gusi mudah berdarah </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo7" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gusi bengkak/abses/fistel </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo8" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Gigi kotor (ada plak & sisa makanan) </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo9" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Karang gigi </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo10" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-5 control-label">Susunan gigi depan tidak teratur </label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbo11" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button runat="server" ID="btnSimpan" CssClass="btn btn-success" Text="Simpan" OnClick="btnSimpan_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"></asp:Button>
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
