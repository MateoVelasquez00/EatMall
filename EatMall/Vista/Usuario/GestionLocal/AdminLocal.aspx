<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="AdminLocal.aspx.cs" Inherits="EatMall.Vista.Usuario.AdminLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">

    <style>
        .bienvenida-titulo {
            font-size: 26px;
            font-weight: 700;
            color: var(--color-rol);
            margin-bottom: 28px;
        }

        .seccion-titulo {
            font-size: 18px;
            font-weight: 600;
            color: #0b1c30;
            margin-bottom: 10px;
        }

        .texto {
            font-size: 14px;
            color: #3d4a42;
            line-height: 1.7;
            margin-bottom: 16px;
        }

        .lista-descripcion {
            list-style: none;
            padding: 0;
            margin: 0 0 20px;
        }

            .lista-descripcion li {
                display: flex;
                align-items: flex-start;
                gap: 8px;
                font-size: 14px;
                color: #3d4a42;
                padding: 4px 0;
                line-height: 1.6;
            }

                .lista-descripcion li::before {
                    content: '';
                    width: 6px;
                    height: 6px;
                    border-radius: 50%;
                    background: var(--color-rol);
                    flex-shrink: 0;
                    margin-top: 8px;
                }

        .sugerencia-box {
            background: color-mix(in srgb, var(--color-rol-light) 35%, white);
            border: 1px solid var(--color-rol-light);
            border-left: 4px solid var(--color-rol);
            border-radius: 8px;
            padding: 16px 20px;
            box-shadow: 0 2px 12px rgba(0,0,0,.04);
        }

        .sugerencia-titulo {
            font-size: 13px;
            font-weight: 700;
            color: var(--color-rol);
            margin-bottom: 6px;
        }

        .divider {
            border: none;
            border-top: 1px solid #bccac0;
            margin: 28px 0;
        }
    </style>

    <h2 class="bienvenida-titulo">Bienvenido al Panel de Administración del Local</h2>

    <p class="seccion-titulo">Administrador de Local</p>

    <p class="texto">
        Como administrador de un local, eres el encargado de administrar tu establecimiento dentro de EatMall,
        manteniendo actualizada su información, el menú de productos y la gestión de los pedidos realizados por los clientes.
    </p>

    <ul class="lista-descripcion">
        <li>Mantener actualizada la información de tu local.</li>
        <li>Administrar el menú de productos disponibles para los clientes.</li>
        <li>Gestionar los pedidos recibidos y hacer seguimiento a su estado.</li>
        <li>Consultar el historial de pedidos procesados.</li>
        <li>Actualizar la información de tu perfil y cambiar tu contraseña cuando sea necesario.</li>
    </ul>

    <hr class="divider" />

    <div class="sugerencia-box">
        <div class="sugerencia-titulo">Sugerencia</div>

        <p class="texto" style="margin: 0;">
            Para comenzar, revisa la información de <strong>Mi Local</strong> y verifica que los datos de tu establecimiento
            estén actualizados. Después puedes administrar tu <strong>Menú</strong> y gestionar los
            <strong>Pedidos</strong> desde las opciones disponibles en el menú lateral.
        </p>
    </div>

</asp:Content>
