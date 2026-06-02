<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Plazoleta.aspx.cs"
    Inherits="EatMall.Vista.Plazoletas"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="container mt-4">

        <div class="d-flex align-items-center mb-4">
            <asp:HyperLink ID="btnVolverIndex"
                runat="server"
                Text="&larr; Volver"
                CssClass="btn btn-outline-dark btn-sm me-3" />

            <h4 class="mb-0">Plazoletas</h4>
        </div>

        <div class="row">
            <asp:Repeater ID="rptPlazoletas" runat="server">
                <ItemTemplate>

                    <div class="col-lg-3 col-md-4 col-sm-6 mb-4">
                        <div class="card shadow-sm p-2 h-100">

                            <img src='<%# Eval("Imagen") %>'
                                class="card-img-top"
                                style="height: 150px; object-fit: cover;"
                                onerror="this.src='/Vista/Assets/Img/CCDefault.png'" />

                            <div class="card-body d-flex flex-column">

                                <h6 class="fw-bold">
                                    <%# Eval("Nombre") %>
                                </h6>

                                <p class="small text-muted">
                                    <%# Eval("Descripcion") %>
                                </p>

                                <p class="small">
                                    <i class="bi bi-building"></i>
                                    <%# Eval("CentroComercial.Nombre") %>
                                </p>

                                <asp:Button ID="btnSeleccionar"
                                    runat="server"
                                    Text="Seleccionar"
                                    CssClass="btn btn-sm w-100 mt-auto fw-bold"
                                    Style="background-color: #F27F0D; color: white; border: none;"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnSeleccionar_Click" />

                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblMensaje"
                runat="server"
                CssClass="text-center text-muted d-block mt-4">
            </asp:Label>

        </div>
    </div>

</asp:Content>
