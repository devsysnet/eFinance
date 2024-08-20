﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebMaster.Master" AutoEventWireup="true" CodeBehind="AdmGroupInput.aspx.cs" Inherits="eFinance.Pages.Master.Input.AdmGroupInput" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12 overflow-x-table">
                            <asp:GridView ID="grdGrup" SkinID="GridView" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="No." ItemStyle-Width="1%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <%# Container.DataItemIndex + 1 %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Group User" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtNamaGroup" runat="server" CssClass="form-control" MaxLength="30" placeholder="Group Name"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Type" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                               <asp:DropDownList runat="server" ID="cboType">
                                                   <asp:ListItem Value="group">Group</asp:ListItem>
                                                   <asp:ListItem Value="user">User</asp:ListItem>
                                               </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Description" ItemStyle-Width="60%">
                                        <ItemTemplate>
                                            <div class="text-center">
                                                <asp:TextBox ID="txtKeterangan" runat="server" CssClass="form-control" MaxLength="200" placeholder="Description"></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="text-center">
                                <asp:Button ID="btnAddRow" runat="server" CssClass="btn btn-success" Text="Add Row" OnClick="btnAddRow_Click" />
                                <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSimpan_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" OnClick="btnReset_Click"/>
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
