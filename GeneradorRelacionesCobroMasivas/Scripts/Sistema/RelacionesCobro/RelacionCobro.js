

function GenerarTodas() {
    if ($('#todas').prop('checked')) {
        $('input[name=coordinacion]').prop("checked", true);
    } else {
        $('input[name=coordinacion]').prop("checked", false);
    }
}

function GenerarRelaciones() {
    document.getElementById('btn-generar-relaciones').disabled = true;
    var allCoord = document.getElementsByName("coordinacion");
	
	generaCoord = new Array();
	for (var i = 0; i < allCoord.length; i++) {
		if (allCoord[i].checked == true) {
			generaCoord.push(allCoord[i].getAttribute("id"));
		}
	}

	if (generaCoord.length == 0) {
		swal("¡Advertencia!", "Selecciona al menos una coordinación!", "warning");
		document.getElementById("btn-generar-relaciones").disabled = false;
    } else {
        $('#btn-generar-relaciones').prop('disabled', true);

        generaCoord.forEach(function (data, index) {
            var datosSucCoord = data.split("-");
            var url = "Home/GenerarRelaciones";
            $.ajax({
                url: url,
                async: false,
                data: {
                    ID_SUCURSAL: datosSucCoord[0],
                    COORDINACION: datosSucCoord[2],
                    NOMBRE_SUCURSAL: datosSucCoord[1]
                },
                type: 'GET',
                dataType: 'text',
                success: function (data) {
                    document.getElementById(datosSucCoord[0] + '-' + datosSucCoord[1] + '-' + datosSucCoord[2]).disabled = true;
                    document.getElementById(datosSucCoord[0] + '-' + datosSucCoord[1] + '-' + datosSucCoord[2]).checked = false;
                    let img = "ok-" + datosSucCoord[0] + '-' + datosSucCoord[2];
                    $('#' + img).removeClass("hidden");
                    $('#' + img).addClass("block");
                },
                error: function () {
                    swal("¡Error!", "No se genero la Relación Cobro", "danger");
                }
            });

        });
        swal("¡Bien!", "Se generaron las relaciones de cobro", "success");
        $('#btn-generar-relaciones').prop('disabled', false);
	}
}