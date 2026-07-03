<%@ Page Title="Nueva Promoción" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="NuevaPromocion.aspx.cs" Inherits="EatMall.Vista.GestionCajero.NuevaPromocion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <div class="d-flex align-items-center gap-3 mb-4">
        <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-outline-secondary d-flex align-items-center gap-1" PostBackUrl="~/Vista/GestionCajero/Promociones.aspx">
            ← Volver a Promociones
        </asp:LinkButton>
        <h3 class="fw-bold m-0">Crear Nueva Promoción / Combo</h3>
    </div>

    <div class="row justify-content-center">
        <div class="col-xl-11">
            <div class="card shadow-sm border-0 bg-white p-4">
                <div class="card-body">
                    
                    <div class="row">
                        <%-- COLUMNA DE DATOS BÁSICOS --%>
                        <div class="col-lg-5 border-end">
                            <h5 class="fw-bold mb-4 text-primary">Información General</h5>

                            <div class="mb-3">
                                <label class="form-label fw-semibold small text-secondary">Nombre de la Promoción *</label>
                                <asp:TextBox ID="txtNombrePromo" runat="server" CssClass="form-control" placeholder="Ej: Mega Combo Familiar" />
                            </div>

                            <div class="row">
                                <div class="col-sm-6 mb-3">
                                    <label class="form-label fw-semibold small text-secondary">Fecha de Inicio *</label>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                                <div class="col-sm-6 mb-3">
                                    <label class="form-label fw-semibold small text-secondary">Fecha de Fin *</label>
                                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                            </div>

                            <div class="mb-3">
                                <label class="form-label fw-semibold small text-secondary">Precio Total del Combo ($) *</label>
                                <asp:TextBox ID="txtPrecioPromo" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" />
                            </div>

                            <div class="mb-4">
                                <label class="form-label fw-semibold small text-secondary">Imagen Promocional *</label>
                                <asp:FileUpload ID="fuImagenPromo" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <%-- COLUMNA DE SELECCIÓN DE PRODUCTOS CON CANTIDADES --%>
                        <div class="col-lg-7 ps-lg-4">
                            <h5 class="fw-bold mb-2 text-primary">Productos y Cantidades</h5>
                            <p class="text-muted small mb-3">Asigna la cantidad de cada producto que compone físicamente este combo (Deja en 0 los que no apliquen):</p>

                            <div class="border rounded bg-white" style="max-height: 350px; overflow-y: auto;">
                                <asp:GridView ID="gvSeleccionarProductos" runat="server"
                                    CssClass="table table-hover table-striped align-middle m-0 border-0"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id,Nombre,Descripcion,Precio">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Producto" HeaderStyle-CssClass="bg-light small" />
                                        <asp:BoundField DataField="Precio" HeaderText="Precio Unit." DataFormatString="{0:C}" HeaderStyle-CssClass="bg-light small" />
                                        
                                        <%-- COLUMNA PARA DIGITAR LA CANTIDAD --%>
                                        <asp:TemplateField HeaderText="Cantidad" HeaderStyle-CssClass="bg-light small text-center" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" 
                                                    Text="0" 
                                                    TextMode="Number" 
                                                    min="0" 
                                                    max="50" 
                                                    CssClass="form-control form-control-sm text-center fw-bold text-primary" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <hr class="text-muted my-4" />

                   
                    <div class="d-flex justify-content-end gap-2">
                        <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-light fw-semibold px-4" PostBackUrl="~/Vista/GestionCajero/Promociones.aspx">Cancelar</asp:LinkButton>
                        <asp:Button ID="btnGuardarPromocion" runat="server" Text="Guardar Promoción" CssClass="btn btn-primary fw-semibold px-4 shadow-sm" OnClick="btnGuardarPromocion_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>