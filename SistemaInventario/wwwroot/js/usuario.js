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
            "url": "/Admin/Usuario/ObtenerTodos" // Se manda llamar la acción ObtenerTodos en el Controller
        },
        columns: [ //Se obtienen las columnas del data en el metodo ObtenerTodos en el Controller
            { "data": "email" },
            { "data": "nombre" },
            { "data": "apellidos" },
            { "data": "phoneNumber" },
            { "data": "role" },
            {   //Para poder acceder a más de una propiedad en una sola columna en data deberemos abrir llaves e indicar los datos a utilizar
                "data": {
                    id: "id",
                    lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {//Incrustamos el id en los botones mediante html
                    /**
                     * LockoutEnd es el campo en la BD que determina si un usuario esta bloqueado, si tiene información es por que tiene
                     * una fecha de expiración, si esta NULL es por que el usuario esta activo.
                     */
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    console.log(hoy);
                    console.log(bloqueo);
                    if (bloqueo > hoy) {
                        //Usuario bloqueado
                        return `
                        <div class="text-center">
                            <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-danger text-white" style="cursor:pointer, width:150px">
                                <i class="bi bi-unlock-fill"></i>Desbloquear
                            </a>
                        </div>
                        `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a onclick=BloquearDesbloquear('${data.id}') class="btn btn-success text-white" style="cursor:pointer, width:150px">
                                <i class="bi bi-lock-fill"></i>Bloquear
                            </a>
                        </div>
                        `;
                    }


                }

            }
        ] //Fin asignación columnas

    });//Fin function 
}

function BloquearDesbloquear(id) {

    $.ajax({
        type: "POST",
        url: "/Admin/Usuario/BloquearDesbloquear",
        data: JSON.stringify(id),
        contentType: "application/json",
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


