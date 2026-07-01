<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespuestaPago.aspx.cs" Inherits="EatMall.Vista.Pedido.RespuestaPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container text-center" style="margin-top: 80px; margin-bottom: 80px;">
		<div class="row justify-content-center">
			<div class="col-md-6">
				<div class="card shadow-sm p-5 border-0 rounded-3">

					<div class="mb-4">
						<asp:Literal ID="litIcono" runat="server"></asp:Literal>
					</div>

					<h2 class="fw-bold mb-3">
						<asp:Label ID="lblTituloEstado" runat="server" Text=""></asp:Label>
					</h2>

					<p class="text-muted fs-5 mb-4">
						<asp:Label ID="lblMensajeDetalle" runat="server" Text=""></asp:Label>
					</p>

					<div class="d-grid gap-2">
						<asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/Index.aspx" CssClass="btn btn-warning btn-lg text-white fw-bold" Style="background-color: #ff914d; border: none;">
                            Volver al Inicio
                        </asp:HyperLink>
					</div>

				</div>
			</div>
		</div>
	</div>
</asp:Content>

