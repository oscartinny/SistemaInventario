let datatable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    datatable = $("#tblDatos").DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar pagina _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url":"/Admin/Marca/ObtenerTodos" // Se manda llamar la acción ObtenerTodos en el Controller
        },
        columns: [ //Se obtienen las columnas del data en el metodo ObtenerTodos en el Controller
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "40%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {//Incrustamos el id en los botones mediante html
                    return `
                        <div class="text-center">
                            <a href="/Admin/Marca/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i> 
                            </a>
                            <a onclick=Delete("/Admin/Marca/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-x-octagon"></i>
                            </a>
                        </div>
                    `;
                },
                 "width": "20%"

            }
        ] //Fin asignación columnas

    });//Fin function 
}

function Delete(url) {
    swal({
        title: "Estas seguro de eliminar esta categoria?",
        text: "Este registro no podrá ser recuperado",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message) //libreria para poder mandar la notificación del resultado
                        //Al realizar el envio de la notificacion recargaremos el datatable que se inicio con la pagina
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
    
}