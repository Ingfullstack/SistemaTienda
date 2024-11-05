let datatable;

$(document).ready(() => {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Almacen/ObtenerTodos"
        },
        "columns": [
            { data: "nombre"},
            { data: "descripcion"},
            {
                data: "estado",
                "render": function (data) {
                    if (data) {
                        return `<span class="badge bg-success">Activo</span>`;
                    } else {
                        return `<span class="badge bg-danger">Inactivo</span>`;
                    }
                }
            },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/ALmacen/Editar/${data}" class="btn btn-success btn-sm bg-gradient"> <i class="bi bi-pencil-square"></i> </a>
                                <a onclick=Eliminar("/Admin/ALmacen/Eliminar/${data}") class="btn btn-danger btn-sm bg-gradient"> <i class="bi bi-trash"></i> </a>
                            </div>`;
                }
            },
        ]
    });
};

const Eliminar = (url) => {
    Swal.fire({
        title: "Estas Seguro?",
        text: "No podras revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si Eliminar!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

