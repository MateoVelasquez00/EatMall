<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EatMall.Index" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style>
		.hero-image {
			height: 60vh;
			background-position: center;
			background-repeat: no-repeat;
			background-size: cover;
			position: relative;
			width: 100%;
			border-radius: 12px;
		}

		.hero-text {
			position: absolute;
			top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);
			color: white;
			text-align: center;
			width: 100%;
		}

		.card {
			border-radius: 12px;
			overflow: hidden;
			width: 100%;
		}

			.card img {
				border-radius: 10px;
			}

		.card-body h5 {
			font-size: 16px;
		}

		.contenedor-mapa {
			background: #ffffff;
			padding: 12px;
			border-radius: 20px;
			box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
		}

		#map {
			height: 500px;
			width: 100%;
			border-radius: 16px;
			z-index: 1;
		}

		.card-CC {
			font-family: 'Segoe UI', Tahoma, Verdana, Geneva, sans-serif;
			max-width: 220px;
		}

			.card-CC h3 {
				font-size: 1.1rem;
				font-weight: 700;
				color: #333;
				margin-bottom: 6px;
				margin-top: 8px;
			}

			.card-CC p {
				font-size: 0.85rem;
				color: #666;
				margin-bottom: 12px;
			}

			.card-CC .card-img {
				width: 100%;
				height: 120px;
				object-fit: cover;
				border-radius: 8px;
			}

		.leaflet-popup-content-wrapper {
			border-radius: 12px !important;
			padding: 4px !important;
			box-shadow: 0 4px 20px rgba(0,0,0,0.15) !important;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="container mt-4">

		<!-- CAROUSEL -->
		<div id="carouselEatMall"
			class="carousel slide mb-5 shadow-lg"
			data-bs-ride="carousel"
			style="border-radius: 15px; overflow: hidden;">

			<div class="carousel-inner">
				<asp:Repeater ID="rptCarousel" runat="server">
					<ItemTemplate>
						<div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">

							<div class="hero-image"
								style="background-image: linear-gradient(rgba(0,0,0,0.6), rgba(0,0,0,0.6)), url('<%# Eval("Imagen") %>');">

								<div class="hero-text">
									<h1 class="display-3 fw-bold">
										<%# Eval("Nombre") %>
									</h1>

									<p class="fs-4">
										<%# Eval("Ciudad.NombreCiudad") %> -
                                        <%# Eval("Ubicacion") %>
									</p>

									<a href='Vista/Plazoleta/Plazoleta.aspx?id=<%# Eval("Id") %>'
										class="btn btn-lg px-5 py-3 rounded-pill fw-bold"
										style="background-color: #F27F0D; color: white; border: none;">Explorar
									</a>
								</div>

							</div>
						</div>
					</ItemTemplate>
				</asp:Repeater>
			</div>

			<button class="carousel-control-prev"
				type="button"
				data-bs-target="#carouselEatMall"
				data-bs-slide="prev">
				<span class="carousel-control-prev-icon"></span>
			</button>

			<button class="carousel-control-next"
				type="button"
				data-bs-target="#carouselEatMall"
				data-bs-slide="next">
				<span class="carousel-control-next-icon"></span>
			</button>
		</div>

		<!-- TITULO -->
		<h4 class="mb-3 mt-2"
			style="font-weight: 700; color: #1a1a1a;">Centros Comerciales
		</h4>

		<!-- LISTADO -->
		<div class="row">
			<asp:Repeater ID="rptCentrosComerciales" runat="server">
				<ItemTemplate>

					<div class="col-lg-3 col-md-3 mb-4">

						<div class="card shadow-sm p-3 h-100 position-relative">

							<img src='<%# Eval("Imagen") %>'
								class="card-img-top"
								style="height: 150px; object-fit: cover;"
								onerror="this.src='Vista/Assets/Img/CCDefault.png'" />

							<div class="card-body text-start">

								<h5 class="fw-bold">
									<%# Eval("Nombre") %>
								</h5>

								<p class="text-muted mb-1">
									<i class="bi bi-geo-alt-fill"></i>
									<%# Eval("Ciudad.NombreCiudad") %> -
                                    <%# Eval("Ciudad.Departamento.Nombre") %>
								</p>

								<p class="small text-secondary">
									<%# Eval("Ubicacion") %>
								</p>

								<a href='Vista/Plazoleta/Plazoleta.aspx?id=<%# Eval("Id") %>'
									class="btn w-100 mt-2 fw-bold"
									style="background-color: #F27F0D; color: white; border: none;">Ver Detalles
								</a>

							</div>
						</div>
					</div>

				</ItemTemplate>
			</asp:Repeater>
		</div>
	</div>

	<!--Mapa -->
	<div class="container my-5">
		<div class="row mb-3">
			<div class="col-12 text-center text-md-start">
				<h2 class="fw-bold text-dark d-flex align-items-center gap-2">
					<i class="bi bi-map-fill text-warning"></i>
					Explora los Centros Comerciales
				</h2>
				<p class="text-muted small m-0">Encuentra la mejor opción para ti</p>
			</div>
		</div>

		<div class="row">
			<div class="col-12">
				<div class="contenedor-mapa">
					<div id="map"></div>
				</div>
			</div>
		</div>
	</div>

	<script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"
		integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo=" crossorigin="">
	</script>

	<script>
		var mapa = L.map('map').setView([4.7109886, -74.072092], 13);

		L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
			maxZoom: 19,
			attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
		}).addTo(mapa);

		function CargarCentrosComerciales() {
			$.ajax({
				type: "POST",
				url: "/Index.aspx/MtObtenerPuntos",
				contentType: "application/json; charset=utf-8",
				dataType: "json",

				success: function (resultado) {
					let lista = resultado.d;

					lista.forEach(CC => {
						L.marker([CC.Latitud, CC.Longitud]).addTo(mapa)
							.bindPopup(`<div class="card-CC"> 
                 <img src="${CC.Imagen}" class="card-img"/>
                 <div class="card-body">
                 <h3>${CC.Nombre}</h3>
                 <p><strong>Dirección:</strong>${CC.Ubicacion}</p>
				 <p>
					<a href="${CC.UbicacionUrl}" target="_blanck" rel="noopener noreferrer" class="btn btn-sm btn-primary text-white">
						<i class="bi bi-geo-alt-fill"></i> Ver en Google Maps
					</a>
				 </p>
                 </div>
                 </div>
                 `);
					});
				},

				error: function () {
					alert("Error al cargar los Centros Comerciales");
				}
			});
		}

		navigator.geolocation.getCurrentPosition(function (respuesta) {
			var lat = respuesta.coords.latitude;
			var long = respuesta.coords.longitude;

			mapa.setView([lat, long], 13);

			var pinRojo = L.icon({
				iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
				shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png',
				iconSize: [25, 41],
				iconAnchor: [12, 41],
				popupAnchor: [1, -34],
				shadowSize: [41, 41]
			});


			L.marker([lat, long], { icon: pinRojo }).addTo(mapa)
				.bindPopup("Tu Ubicación")
				.openPopup();
		});

		CargarCentrosComerciales();
	</script>

</asp:Content>
