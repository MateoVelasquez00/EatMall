<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificarCuenta.aspx.cs" Inherits="EatMall.Vista.Auth.VerificarCuenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

	<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" />
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
	<link href="/Vista/Assets/CSS/StyleLR.css" rel="stylesheet" />
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div class="login-container">
			<h2>Verifica tu correo</h2>
			<p>Hemos enviado un código a tu email.</p>
			<asp:TextBox ID="txtCodigo" runat="server" placeholder="000000" MaxLength="6"></asp:TextBox>
			<asp:Button ID="btnVerificar" runat="server" Text="Activar Cuenta" OnClick="btnVerificar_Click" CssClass="btn-ingresar" />
			<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
		</div>
	</form>
</body>
</html>
