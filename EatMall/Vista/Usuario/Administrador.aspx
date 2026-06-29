<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Admin.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="EatMall.Vista.Usuario.Administrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" runat="server">
    <%-- Este va vacío o con meta tags si necesitas --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <style>
        .bienvenida-titulo {
            font-size: 26px;
            font-weight: 700;
            color: #006948;
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
                    background: #006948;
                    flex-shrink: 0;
                    margin-top: 8px;
                }

        .sugerencia-box {
            background: #f0fdf4;
            border: 1px solid #adedd3;
            border-left: 4px solid #006948;
            border-radius: 8px;
            padding: 16px 20px;
        }

        .sugerencia-titulo {
            font-size: 13px;
            font-weight: 700;
            color: #006948;
            margin-bottom: 6px;
        }

        .divider {
            border: none;
            border-top: 1px solid #bccac0;
            margin: 28px 0;
        }
    </style>

    <h2 class="bienvenida-titulo">Bienvenido al Panel de Administración General</h2>

    <p class="seccion-titulo">Administrador General</p>
    <p class="texto">
        El rol de Administrador General tiene acceso completo a la plataforma EatMall.
        Es el responsable de mantener la estructura del sistema operando correctamente,
        gestionando los centros comerciales, usuarios, plazoletas y locales registrados.
    </p>

    <ul class="lista-descripcion">
        <li>Gestionar centros comerciales — crear, editar y desactivar.</li>
        <li>Administrar los usuarios del sistema y asignar roles.</li>
        <li>Configurar las plazoletas dentro de cada centro comercial.</li>
        <li>Supervisar los locales y su estado (Abierto / Cerrado).</li>
        <li>Visualizar la actividad general de la plataforma.</li>
    </ul>

    <hr class="divider" />

    <div class="sugerencia-box">
        <div class="sugerencia-titulo">Sugerencia</div>
        <p class="texto" style="margin: 0;">
            Para comenzar, dirígete a <strong>Centros Comerciales</strong> desde el menú lateral
            para revisar los registros actuales. Si necesitas agregar nuevos usuarios al sistema,
            usa la opción <strong>Usuarios</strong> del menú lateral.
        </p>
    </div>

</asp:Content>
